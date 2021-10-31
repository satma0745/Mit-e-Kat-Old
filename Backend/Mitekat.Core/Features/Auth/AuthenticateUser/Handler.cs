namespace Mitekat.Core.Features.Auth.AuthenticateUser
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Mitekat.Core.Features.Shared.Handlers;
    using Mitekat.Core.Features.Shared.Responses;
    using Mitekat.Core.Helpers.AuthToken;
    using Mitekat.Core.Helpers.PasswordHashing;
    using Mitekat.Core.Persistence.Entities;
    using Mitekat.Core.Persistence.UnitOfWork;

    internal class AuthenticateUserHandler : RequestHandlerBase<AuthenticateUserRequest, AuthenticateUserResult>
    {
        private readonly IAuthTokenHelper _authTokenHelper;
        private readonly IMapper _mapper;
        private readonly IPasswordHashingHelper _passwordHashingHelper;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticateUserHandler(
            IUnitOfWork unitOfWork,
            IAuthTokenHelper authTokenHelper,
            IPasswordHashingHelper passwordHashingHelper,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _authTokenHelper = authTokenHelper;
            _passwordHashingHelper = passwordHashingHelper;
            _mapper = mapper;
        }

        protected override async Task<Response<AuthenticateUserResult>> HandleAsync(AuthenticateUserRequest request)
        {
            var user = await _unitOfWork.Users.FindAsync(request.Username);
            if (user is null)
            {
                // user with specified username was not found
                return Failure(Error.NotFound);
            }

            if (!_passwordHashingHelper.AreEqual(user.Password, request.Password))
            {
                // incorrect password provided
                return Failure(Error.Conflict);
            }

            var tokenPairInfo = _authTokenHelper.IssueTokenPair(user.Id, user.Role);
            var refreshTokenInfo = tokenPairInfo.RefreshToken;

            var refreshToken = new RefreshTokenEntity(refreshTokenInfo.TokenId, refreshTokenInfo.ExpirationTime);
            _unitOfWork.RefreshTokens.Add(refreshToken);
            await _unitOfWork.SaveChangesAsync();

            return Success(_mapper.Map<AuthenticateUserResult>(tokenPairInfo));
        }
    }
}

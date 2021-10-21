namespace Mitekat.Core.Features.Auth.Handlers
{
    using System.Threading.Tasks;
    using Mitekat.Core.Features.Shared.Handlers;
    using Mitekat.Core.Features.Shared.Responses;
    using Mitekat.Core.Helpers.AuthToken;
    using Mitekat.Core.Helpers.PasswordHashing;
    using Mitekat.Core.Persistence.Entities;
    using Mitekat.Core.Persistence.UnitOfWork;

    internal class AuthenticateUserHandler : RequestHandlerBase<AuthenticateUserRequest, TokenPairResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthTokenHelper _authTokenHelper;
        private readonly IPasswordHashingHelper _passwordHashingHelper;

        public AuthenticateUserHandler(
            IUnitOfWork unitOfWork,
            IAuthTokenHelper authTokenHelper,
            IPasswordHashingHelper passwordHashingHelper)
        {
            _unitOfWork = unitOfWork;
            _authTokenHelper = authTokenHelper;
            _passwordHashingHelper = passwordHashingHelper;
        }
        
        protected override async Task<Response<TokenPairResult>> HandleAsync(AuthenticateUserRequest request)
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
            
            return Success(TokenPairResult.FromTokenPairInfo(tokenPairInfo));
        }
    }
}

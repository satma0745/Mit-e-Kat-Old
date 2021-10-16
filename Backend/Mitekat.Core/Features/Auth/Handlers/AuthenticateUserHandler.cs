namespace Mitekat.Core.Features.Auth.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Mitekat.Core.Helpers.AuthToken;
    using Mitekat.Core.Helpers.PasswordHashing;
    using Mitekat.Core.Persistence.Entities;
    using Mitekat.Core.Persistence.UnitOfWork;

    internal class AuthenticateUserHandler : IRequestHandler<AuthenticateUserRequest, TokenPairResponse>
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
        
        public async Task<TokenPairResponse> Handle(AuthenticateUserRequest request, CancellationToken _)
        {
            var user = await _unitOfWork.Users.FindAsync(request.Username);
            if (user is null)
            {
                // user with specified username was not found
                return null;
            }

            if (!_passwordHashingHelper.AreEqual(user.Password, request.Password))
            {
                // incorrect password provided
                return null;
            }
            
            var tokenPairInfo = _authTokenHelper.IssueTokenPair(user.Id, user.Role);
            var refreshTokenInfo = tokenPairInfo.RefreshToken;
            
            var refreshToken = new RefreshTokenEntity(refreshTokenInfo.TokenId, refreshTokenInfo.ExpirationTime);
            _unitOfWork.RefreshTokens.Add(refreshToken);
            await _unitOfWork.SaveChangesAsync();
            
            return TokenPairResponse.FromTokenPairInfo(tokenPairInfo);
        }
    }
}

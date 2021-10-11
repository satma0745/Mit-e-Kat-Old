namespace Mitekat.Core.Services
{
    using System.Threading.Tasks;
    using Mitekat.Core.Helpers.AuthToken;
    using Mitekat.Core.Helpers.PasswordHashing;
    using Mitekat.Core.Persistence.Entities;
    using Mitekat.Core.Persistence.UnitOfWork;

    internal class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthTokenHelper _authTokenHelper;
        private readonly IPasswordHashingHelper _passwordHashingHelper;

        public AuthService(
            IUnitOfWork unitOfWork,
            IAuthTokenHelper authTokenHelper,
            IPasswordHashingHelper passwordHashingHelper)
        {
            _unitOfWork = unitOfWork;
            _authTokenHelper = authTokenHelper;
            _passwordHashingHelper = passwordHashingHelper;
        }

        public async Task<UserEntity> GetTokenOwnerInfo(string encodedAccessToken)
        {
            var accessTokenInfo = _authTokenHelper.ParseAccessToken(encodedAccessToken);
            if (accessTokenInfo is null)
            {
                return null;
            }
            
            return await _unitOfWork.Users.FindAsync(accessTokenInfo.OwnerId);
        }

        public async Task RegisterNewUser(string username, string plainTextPassword)
        {
            var hashedPassword = _passwordHashingHelper.HashPassword(plainTextPassword);
            var user = new UserEntity(username, hashedPassword);
            
            _unitOfWork.Users.Add(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ITokenPair> AuthenticateUser(string username, string plainTextPassword)
        {
            var user = await _unitOfWork.Users.FindAsync(username);
            if (user is null)
            {
                // user with specified username was not found
                return null;
            }

            if (!_passwordHashingHelper.AreEqual(user.Password, plainTextPassword))
            {
                // incorrect password provided
                return null;
            }
            
            var tokenPairInfo = _authTokenHelper.IssueTokenPair(user.Id);
            var refreshTokenInfo = tokenPairInfo.RefreshToken;
            
            var refreshToken = new RefreshTokenEntity(refreshTokenInfo.TokenId, refreshTokenInfo.ExpirationTime);
            _unitOfWork.RefreshTokens.Add(refreshToken);
            await _unitOfWork.SaveChangesAsync();
            
            return new TokenPair(tokenPairInfo);
        }

        public async Task<ITokenPair> RefreshTokenPair(string encodedRefreshToken)
        {
            var oldRefreshTokenInfo = _authTokenHelper.ParseRefreshToken(encodedRefreshToken);
            if (oldRefreshTokenInfo is null)
            {
                // refresh token is not valid
                return null;
            }

            var oldRefreshToken = await _unitOfWork.RefreshTokens.FindAsync(oldRefreshTokenInfo.TokenId);
            if (oldRefreshToken is null)
            {
                // token is not registered
                return null;
            }

            var newTokenPairInfo = _authTokenHelper.IssueTokenPair(oldRefreshTokenInfo.OwnerId);
            var newRefreshTokenInfo = newTokenPairInfo.RefreshToken;
            
            var newRefreshToken = new RefreshTokenEntity(newRefreshTokenInfo.TokenId, newRefreshTokenInfo.ExpirationTime);
            _unitOfWork.RefreshTokens.Replace(oldRefreshToken, newRefreshToken);
            await _unitOfWork.SaveChangesAsync();

            return new TokenPair(newTokenPairInfo);
        }
    }
    
    internal class TokenPair : ITokenPair
    {
        public string AccessToken { get; }
        public string RefreshToken { get; }

        public TokenPair(TokenPairInfo tokenPairInfo)
        {
            AccessToken = tokenPairInfo.AccessToken.EncodedToken;
            RefreshToken = tokenPairInfo.RefreshToken.EncodedToken;
        }
    }
}

namespace Mitekat.RestApi.Services
{
    using System.Threading.Tasks;
    using Mitekat.Persistence.Entities;
    using Mitekat.Persistence.UnitOfWork;
    using Mitekat.RestApi.Helpers;

    public class AuthService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly AuthTokenHelper _authTokenHelper;
        private readonly PasswordHelper _passwordHelper;

        public AuthService(UnitOfWork unitOfWork, AuthTokenHelper authTokenHelper, PasswordHelper passwordHelper)
        {
            _unitOfWork = unitOfWork;
            _authTokenHelper = authTokenHelper;
            _passwordHelper = passwordHelper;
        }

        public async Task<User> GetTokenOwnerInfo(string encodedAccessToken)
        {
            var accessTokenInfo = _authTokenHelper.ParseAccessToken(encodedAccessToken);
            if (accessTokenInfo is null)
            {
                return null;
            }
            
            return await _unitOfWork.Users.FindUserAsync(accessTokenInfo.OwnerId);
        }

        public async Task RegisterNewUser(string username, string plainTextPassword)
        {
            var hashedPassword = _passwordHelper.HashPassword(plainTextPassword);
            var user = new User(username, hashedPassword);
            
            _unitOfWork.Users.AddUser(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<TokenPair> AuthenticateUser(string username, string plainTextPassword)
        {
            var user = await _unitOfWork.Users.FindUserAsync(username);
            if (user is null)
            {
                // user with specified username was not found
                return null;
            }

            if (!_passwordHelper.AreEqual(user.Password, plainTextPassword))
            {
                // incorrect password provided
                return null;
            }
            
            var tokenPair = _authTokenHelper.IssueTokenPair(user.Id);
            var refreshTokenInfo = tokenPair.RefreshToken;
            
            var refreshToken = new RefreshToken(refreshTokenInfo.TokenId, refreshTokenInfo.ExpirationTime);
            _unitOfWork.RefreshTokens.AddRefreshToken(refreshToken);
            await _unitOfWork.SaveChangesAsync();
            
            return tokenPair;
        }

        public async Task<TokenPair> RefreshTokenPair(string encodedRefreshToken)
        {
            var oldRefreshTokenInfo = _authTokenHelper.ParseRefreshToken(encodedRefreshToken);
            if (oldRefreshTokenInfo is null)
            {
                // refresh token is not valid
                return null;
            }

            var oldRefreshToken = await _unitOfWork.RefreshTokens.FindRefreshTokenAsync(oldRefreshTokenInfo.TokenId);
            if (oldRefreshToken is null)
            {
                // token is not registered
                return null;
            }

            var newTokenPair = _authTokenHelper.IssueTokenPair(oldRefreshTokenInfo.OwnerId);
            var newRefreshTokenInfo = newTokenPair.RefreshToken;
            
            var newRefreshToken = new RefreshToken(newRefreshTokenInfo.TokenId, newRefreshTokenInfo.ExpirationTime);
            _unitOfWork.RefreshTokens.ReplaceToken(oldRefreshToken, newRefreshToken);
            await _unitOfWork.SaveChangesAsync();

            return newTokenPair;
        }
    }
}

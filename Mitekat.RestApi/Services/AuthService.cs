namespace Mitekat.RestApi.Services
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Mitekat.RestApi.Entities;
    using Mitekat.RestApi.Helpers;

    public class AuthService
    {
        private readonly MitekatDbContext _context;
        private readonly AuthTokenHelper _authTokenHelper;
        private readonly PasswordHelper _passwordHelper;

        public AuthService(MitekatDbContext context, AuthTokenHelper authTokenHelper, PasswordHelper passwordHelper)
        {
            _context = context;
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
            
            return await _context.Users.FirstOrDefaultAsync(user => user.Id == accessTokenInfo.OwnerId);
        }

        public Task RegisterNewUser(string username, string plainTextPassword)
        {
            var hashedPassword = _passwordHelper.HashPassword(plainTextPassword);
            _context.Users.Add(new User(username, hashedPassword));
            return _context.SaveChangesAsync();
        }

        public async Task<TokenPair> AuthenticateUser(string username, string plainTextPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Username == username);
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
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
            
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

            var oldRefreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(token => token.TokenId == oldRefreshTokenInfo.TokenId);
            if (oldRefreshToken is null)
            {
                // token is not registered
                return null;
            }

            var newTokenPair = _authTokenHelper.IssueTokenPair(oldRefreshTokenInfo.OwnerId);
            var newRefreshTokenInfo = newTokenPair.RefreshToken;
            
            var newRefreshToken = new RefreshToken(newRefreshTokenInfo.TokenId, newRefreshTokenInfo.ExpirationTime);
            _context.RefreshTokens.Remove(oldRefreshToken);
            _context.RefreshTokens.Add(newRefreshToken);
            await _context.SaveChangesAsync();

            return newTokenPair;
        }
    }
}

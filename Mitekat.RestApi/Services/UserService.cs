namespace Mitekat.RestApi.Services
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Mitekat.RestApi.Entities;
    using Mitekat.RestApi.Helpers;

    public class UserService
    {
        private readonly MitekatDbContext _context;
        private readonly AuthTokenHelper _authTokenHelper;
        private readonly PasswordHelper _passwordHelper;

        public UserService(MitekatDbContext context, AuthTokenHelper authTokenHelper, PasswordHelper passwordHelper)
        {
            _context = context;
            _authTokenHelper = authTokenHelper;
            _passwordHelper = passwordHelper;
        }

        public Task<User> GetTokenOwnerInfo(string accessToken)
        {
            var ownerId = _authTokenHelper.ParseAccessToken(accessToken);
            return _context.Users.FirstOrDefaultAsync(user => user.Id == ownerId);
        }

        public Task RegisterNewUser(string username, string plainTextPassword)
        {
            var hashedPassword = _passwordHelper.HashPassword(plainTextPassword);
            _context.Users.Add(new User(username, hashedPassword));
            return _context.SaveChangesAsync();
        }

        public async Task<string> AuthenticateUser(string username, string plainTextPassword)
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
            
            var accessToken = _authTokenHelper.IssueAccessToken(user.Id);
            return accessToken;
        }
    }
}

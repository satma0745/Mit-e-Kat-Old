namespace Mitekat.RestApi.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Mitekat.RestApi.Helpers;

    public class UserService
    {
        private readonly ICollection<User> _users = new List<User>();
        private readonly AuthTokenHelper _authTokenHelper;
        private readonly PasswordHelper _passwordHelper;

        public UserService(AuthTokenHelper authTokenHelper, PasswordHelper passwordHelper)
        {
            _authTokenHelper = authTokenHelper;
            _passwordHelper = passwordHelper;
        }

        public User GetTokenOwnerInfo(string accessToken)
        {
            var ownerId = _authTokenHelper.ParseAccessToken(accessToken);
            return _users.FirstOrDefault(user => user.Id == ownerId);
        }

        public void RegisterNewUser(string username, string plainTextPassword)
        {
            var hashedPassword = _passwordHelper.HashPassword(plainTextPassword);
            _users.Add(new User(Guid.NewGuid(), username, hashedPassword));
        }

        public string AuthenticateUser(string username, string plainTextPassword)
        {
            var user = _users.FirstOrDefault(user => user.Username == username);
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

    public record User(Guid Id, string Username, UserPassword Password);

    public record UserPassword(string Hash, string Salt);
}

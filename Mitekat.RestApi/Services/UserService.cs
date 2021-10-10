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

        public UserService(AuthTokenHelper authTokenHelper) =>
            _authTokenHelper = authTokenHelper;

        public User GetTokenOwnerInfo(string accessToken)
        {
            var ownerId = _authTokenHelper.ParseAccessToken(accessToken);
            return _users.FirstOrDefault(user => user.Id == ownerId);
        }

        public void RegisterNewUser(string username, string password) =>
            _users.Add(new User(Guid.NewGuid(), username, password));

        public string AuthenticateUser(string username, string password)
        {
            var user = _users.FirstOrDefault(user => user.Username == username && user.Password == password);
            return user is null ? null : _authTokenHelper.IssueAccessToken(user.Id);
        }
            
    }

    public record User(Guid Id, string Username, string Password);
}

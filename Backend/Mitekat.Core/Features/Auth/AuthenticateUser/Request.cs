namespace Mitekat.Core.Features.Auth.AuthenticateUser
{
    using Mitekat.Core.Features.Shared.Requests;

    public class AuthenticateUserRequest : RequestBase<AuthenticateUserResult>
    {
        public string Username { get; }
        public string Password { get; }

        public AuthenticateUserRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
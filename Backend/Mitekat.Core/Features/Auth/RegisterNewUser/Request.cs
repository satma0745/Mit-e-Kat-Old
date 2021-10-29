namespace Mitekat.Core.Features.Auth.RegisterNewUser
{
    using Mitekat.Core.Features.Shared.Requests;
    using Mitekat.Core.Features.Shared.Responses;

    public class RegisterNewUserRequest : RequestBase<BlankResult>
    {
        public string Username { get; }
        public string Password { get; }

        public RegisterNewUserRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
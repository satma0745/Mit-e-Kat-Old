namespace Mitekat.Core.Features.Auth.AuthenticateUser
{
    using Mitekat.Core.Features.Shared.Requests;

    public class AuthenticateUserRequest : RequestBase<AuthenticateUserResult>
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string Username { get; private set; }
        
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string Password { get; private set; }
    }
}

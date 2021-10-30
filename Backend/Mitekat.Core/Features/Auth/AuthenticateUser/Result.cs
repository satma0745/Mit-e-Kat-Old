namespace Mitekat.Core.Features.Auth.AuthenticateUser
{
    public class AuthenticateUserResult
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string AccessToken { get; private set; }
        
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string RefreshToken { get; private set; }
    }
}

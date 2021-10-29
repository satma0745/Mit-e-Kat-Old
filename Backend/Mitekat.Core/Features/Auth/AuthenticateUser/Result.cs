namespace Mitekat.Core.Features.Auth.AuthenticateUser
{
    using Mitekat.Core.Helpers.AuthToken;

    public class AuthenticateUserResult
    {
        public static AuthenticateUserResult FromTokenPairInfo(ITokenPairInfo tokenPairInfo) =>
            new(tokenPairInfo.AccessToken.EncodedToken, tokenPairInfo.RefreshToken.EncodedToken);
        
        public string AccessToken { get; }
        public string RefreshToken { get; }

        private AuthenticateUserResult(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
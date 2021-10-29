namespace Mitekat.Core.Features.Auth.RefreshTokenPair
{
    using Mitekat.Core.Helpers.AuthToken;

    public class RefreshTokenPairResult
    {
        public static RefreshTokenPairResult FromTokenPairInfo(ITokenPairInfo tokenPairInfo) =>
            new(tokenPairInfo.AccessToken.EncodedToken, tokenPairInfo.RefreshToken.EncodedToken);
        
        public string AccessToken { get; }
        public string RefreshToken { get; }

        private RefreshTokenPairResult(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
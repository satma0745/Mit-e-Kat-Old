namespace Mitekat.RestApi.Features.Auth.Dtos
{
    using System.Text.Json.Serialization;
    using Mitekat.Core.Features.Auth.RefreshTokenPair;

    public class RefreshTokenPairResultDto
    {
        public static RefreshTokenPairResultDto FromResult(RefreshTokenPairResult result) =>
            new(result.AccessToken, result.RefreshToken);
        
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; }
        
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; }

        private RefreshTokenPairResultDto(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}

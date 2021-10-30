namespace Mitekat.RestApi.Features.Auth.Dtos
{
    using System.Text.Json.Serialization;

    public class RefreshTokenPairResultDto
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; private set; }
        
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; private set; }
    }
}

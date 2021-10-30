namespace Mitekat.RestApi.Features.Auth.Dtos
{
    using System.Text.Json.Serialization;

    public class AuthenticateUserResultDto
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; private set; }
        
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; private set; }
    }
}

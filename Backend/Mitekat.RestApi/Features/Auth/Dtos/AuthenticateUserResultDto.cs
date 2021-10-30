namespace Mitekat.RestApi.Features.Auth.Dtos
{
    using System.Text.Json.Serialization;
    using Mitekat.Core.Features.Auth.AuthenticateUser;

    public class AuthenticateUserResultDto
    {
        public static AuthenticateUserResultDto FromResult(AuthenticateUserResult result) =>
            new(result.AccessToken, result.RefreshToken);
        
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; }
        
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; }

        private AuthenticateUserResultDto(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}

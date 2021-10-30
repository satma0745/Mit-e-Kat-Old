namespace Mitekat.RestApi.Features.Auth.Dtos
{
    using System;
    using System.Text.Json.Serialization;
    using Mitekat.Core.Features.Auth.GetTokenOwnerInfo;

    public class GetTokenOwnerInfoResultDto
    {
        public static GetTokenOwnerInfoResultDto FromResult(GetTokenOwnerInfoResult result) =>
            new(result.Id, result.Username, result.Role.ToString());
        
        [JsonPropertyName("id")]
        public Guid Id { get; }
        
        [JsonPropertyName("username")]
        public string Username { get; }
        
        [JsonPropertyName("role")]
        public string Role { get; }

        private GetTokenOwnerInfoResultDto(Guid id, string username, string role)
        {
            Id = id;
            Username = username;
            Role = role;
        }
    }
}

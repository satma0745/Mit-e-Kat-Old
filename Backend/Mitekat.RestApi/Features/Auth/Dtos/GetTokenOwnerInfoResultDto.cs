namespace Mitekat.RestApi.Features.Auth.Dtos
{
    using System;
    using System.Text.Json.Serialization;

    public class GetTokenOwnerInfoResultDto
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [JsonPropertyName("id")] public Guid Id { get; private set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [JsonPropertyName("username")] public string Username { get; private set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [JsonPropertyName("role")] public string Role { get; private set; }
    }
}

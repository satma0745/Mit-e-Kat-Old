namespace Mitekat.RestApi.Features.Auth.Dtos
{
    using System.Text.Json.Serialization;
    using FluentValidation;

    public class RefreshTokenPairRequestDto
    {
        [JsonPropertyName("refreshToken")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string RefreshToken { get; set; }
    }
    
    internal class RefreshTokenRequestDtoValidator : AbstractValidator<RefreshTokenPairRequestDto>
    {
        public RefreshTokenRequestDtoValidator() =>
            RuleFor(dto => dto.RefreshToken).NotEmpty();
    }
}

namespace Mitekat.RestApi.Features.Auth.Dtos
{
    using System.Text.Json.Serialization;
    using FluentValidation;
    using Mitekat.Core.Features.Auth.RefreshTokenPair;

    public class RefreshTokenRequestDto
    {
        [JsonPropertyName("refreshToken")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string RefreshToken { get; set; }
        
        public RefreshTokenPairRequest ToRequest() =>
            new(RefreshToken);
    }
    
    internal class RefreshTokenRequestDtoValidator : AbstractValidator<RefreshTokenRequestDto>
    {
        public RefreshTokenRequestDtoValidator() =>
            RuleFor(dto => dto.RefreshToken).NotEmpty();
    }
}

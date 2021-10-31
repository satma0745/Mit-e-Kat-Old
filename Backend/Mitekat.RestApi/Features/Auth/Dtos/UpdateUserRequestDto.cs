namespace Mitekat.RestApi.Features.Auth.Dtos
{
    using System.Text.Json.Serialization;
    using FluentValidation;

    public class UpdateUserRequestDto
    {
        [JsonPropertyName("username")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string Username { get; set; }

        [JsonPropertyName("password")]
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string Password { get; set; }
    }

    internal class UpdateUserRequestDtoValidator : AbstractValidator<UpdateUserRequestDto>
    {
        public UpdateUserRequestDtoValidator()
        {
            RuleFor(dto => dto.Username)
                .MinimumLength(6)
                .MaximumLength(20)
                .NotEmpty();

            RuleFor(dto => dto.Password)
                .MinimumLength(6)
                .MaximumLength(20)
                .NotEmpty();
        }
    }
}

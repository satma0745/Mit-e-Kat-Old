namespace Mitekat.RestApi.Features.Auth.Dtos
{
    using FluentValidation;

    public class AuthenticateUserRequestDto
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string Username { get; set; }
        
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string Password { get; set; }
    }

    internal class AuthenticateUserRequestDtoValidator : AbstractValidator<AuthenticateUserRequestDto>
    {
        public AuthenticateUserRequestDtoValidator()
        {
            RuleFor(dto => dto.Username).NotEmpty();
            RuleFor(dto => dto.Password).NotEmpty();
        }
    }
}

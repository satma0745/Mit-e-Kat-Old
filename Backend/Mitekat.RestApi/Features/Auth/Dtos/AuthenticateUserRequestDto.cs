namespace Mitekat.RestApi.Features.Auth.Dtos
{
    using FluentValidation;
    using Mitekat.Core.Features.Auth.AuthenticateUser;

    public class AuthenticateUserRequestDto
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string Username { get; set; }
        
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string Password { get; set; }
        
        public AuthenticateUserRequest ToRequest() =>
            new(Username, Password);
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

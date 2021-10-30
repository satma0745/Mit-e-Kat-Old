namespace Mitekat.Core.Features.Auth.RegisterNewUser
{
    using FluentValidation;

    internal class RegisterNewUserRequestValidator : AbstractValidator<RegisterNewUserRequest>
    {
        public RegisterNewUserRequestValidator()
        {
            RuleFor(request => request.Username)
                .MinimumLength(6)
                .MaximumLength(20)
                .NotEmpty();
            
            RuleFor(request => request.Password)
                .MinimumLength(6)
                .MaximumLength(20)
                .NotEmpty();
        }
    }
}

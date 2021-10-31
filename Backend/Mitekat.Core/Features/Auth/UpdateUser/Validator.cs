namespace Mitekat.Core.Features.Auth.UpdateUser
{
    using FluentValidation;

    internal class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(request => request.Id).NotEmpty();
            RuleFor(request => request.Requester).NotEmpty();

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

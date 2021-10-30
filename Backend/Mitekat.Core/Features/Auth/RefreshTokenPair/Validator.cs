namespace Mitekat.Core.Features.Auth.RefreshTokenPair
{
    using FluentValidation;

    internal class RefreshTokenPairRequestValidator : AbstractValidator<RefreshTokenPairRequest>
    {
        public RefreshTokenPairRequestValidator() =>
            RuleFor(request => request.RefreshToken).NotEmpty();
    }
}
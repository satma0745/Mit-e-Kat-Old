namespace Mitekat.Core.Features.Auth.GetTokenOwnerInfo
{
    using FluentValidation;

    internal class GetTokenOwnerInfoRequestValidator : AbstractValidator<GetTokenOwnerInfoRequest>
    {
        public GetTokenOwnerInfoRequestValidator() =>
            RuleFor(request => request.Requester).NotEmpty();
    }
}
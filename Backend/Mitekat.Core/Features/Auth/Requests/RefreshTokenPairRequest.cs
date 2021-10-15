namespace Mitekat.Core.Features.Auth.Requests
{
    using MediatR;
    using Mitekat.Core.Features.Auth.Responses;

    public class RefreshTokenPairRequest : IRequest<TokenPairResponse>
    {
        public string RefreshToken { get; set; }
    }
}

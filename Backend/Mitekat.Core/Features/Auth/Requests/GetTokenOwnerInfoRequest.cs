namespace Mitekat.Core.Features.Auth.Requests
{
    using MediatR;
    using Mitekat.Core.Features.Auth.Responses;

    public class GetTokenOwnerInfoRequest : IRequest<UserInfoResponse>
    {
        public string AccessToken { get; set; }
    }
}

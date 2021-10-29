namespace Mitekat.Core.Features.Auth.GetTokenOwnerInfo
{
    using Mitekat.Core.Features.Shared.Requests;

    public class GetTokenOwnerInfoRequest : RequestBase<GetTokenOwnerInfoResult>
    {
        public IRequester Requester { get; }

        public GetTokenOwnerInfoRequest(IRequester requester) =>
            Requester = requester;
    }
}
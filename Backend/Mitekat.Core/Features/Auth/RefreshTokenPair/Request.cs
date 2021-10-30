namespace Mitekat.Core.Features.Auth.RefreshTokenPair
{
    using Mitekat.Core.Features.Shared.Requests;

    public class RefreshTokenPairRequest : RequestBase<RefreshTokenPairResult>
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string RefreshToken { get; private set; }
    }
}

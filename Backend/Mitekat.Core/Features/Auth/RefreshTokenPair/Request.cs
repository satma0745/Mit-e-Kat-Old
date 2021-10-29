namespace Mitekat.Core.Features.Auth.RefreshTokenPair
{
    using Mitekat.Core.Features.Shared.Requests;

    public class RefreshTokenPairRequest : RequestBase<RefreshTokenPairResult>
    {
        public string RefreshToken { get; }

        public RefreshTokenPairRequest(string refreshToken) =>
            RefreshToken = refreshToken;
    }
}
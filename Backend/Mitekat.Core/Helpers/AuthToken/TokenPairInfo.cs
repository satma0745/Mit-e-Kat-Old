namespace Mitekat.Core.Helpers.AuthToken
{
    using System;

    internal class TokenPairInfo
    {
        public AccessTokenInfo AccessToken { get; init; }
        public RefreshTokenInfo RefreshToken { get; init; }
    }

    internal class AccessTokenInfo
    {
        public Guid OwnerId { get; init; }
        public string EncodedToken { get; init; }
    }

    internal class RefreshTokenInfo
    {
        public Guid TokenId { get; init; }
        public Guid OwnerId { get; init; }
        public DateTime ExpirationTime { get; init; }
        public string EncodedToken { get; init; }
    }
}

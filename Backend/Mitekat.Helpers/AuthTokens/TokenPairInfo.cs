namespace Mitekat.Helpers.AuthTokens
{
    using System;
    using Mitekat.Core.Helpers.AuthToken;
    using Mitekat.Core.Persistence.Entities;

    internal class TokenPairInfo : ITokenPairInfo
    {
        public IAccessTokenInfo AccessToken { get; init; }
        public IRefreshTokenInfo RefreshToken { get; init; }
    }

    internal class AccessTokenInfo : IAccessTokenInfo
    {
        public Guid OwnerId { get; init; }
        public UserRole OwnerRole { get; init; }
        public string EncodedToken { get; init; }
    }

    internal class RefreshTokenInfo : IRefreshTokenInfo
    {
        public Guid TokenId { get; init; }
        public Guid OwnerId { get; init; }
        public DateTime ExpirationTime { get; init; }
        public string EncodedToken { get; init; }
    }
}

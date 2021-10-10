namespace Mitekat.RestApi.Entities
{
    using System;

    public class RefreshToken
    {
        public readonly Guid TokenId;
        public readonly DateTime ExpirationTime;

        // For EF Core
        private RefreshToken()
        {
        }

        public RefreshToken(Guid tokenId, DateTime expirationTime)
        {
            TokenId = tokenId;
            ExpirationTime = expirationTime;
        }
    }
}

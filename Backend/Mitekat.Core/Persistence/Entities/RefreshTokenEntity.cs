namespace Mitekat.Core.Persistence.Entities
{
    using System;

    public class RefreshTokenEntity
    {
        public readonly Guid TokenId;
        public readonly DateTime ExpirationTime;

        // For EF Core
        // ReSharper disable once UnusedMember.Local
        private RefreshTokenEntity()
        {
        }

        public RefreshTokenEntity(Guid tokenId, DateTime expirationTime)
        {
            TokenId = tokenId;
            ExpirationTime = expirationTime;
        }
    }
}

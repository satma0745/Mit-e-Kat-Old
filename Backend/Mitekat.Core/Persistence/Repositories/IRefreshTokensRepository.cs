namespace Mitekat.Core.Persistence.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Mitekat.Core.Persistence.Entities;

    public interface IRefreshTokensRepository
    {
        Task<RefreshTokenEntity> FindAsync(Guid tokenId);

        void Replace(RefreshTokenEntity existingToken, RefreshTokenEntity newToken);

        void Add(RefreshTokenEntity token);
    }
}

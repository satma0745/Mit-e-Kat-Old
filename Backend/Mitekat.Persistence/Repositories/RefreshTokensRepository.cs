namespace Mitekat.Persistence.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Mitekat.Core.Persistence.Entities;
    using Mitekat.Core.Persistence.Repositories;
    using Mitekat.Persistence.Context;

    internal class RefreshTokensRepository : IRefreshTokensRepository
    {
        private readonly DatabaseContext _context;

        public RefreshTokensRepository(DatabaseContext context) =>
            _context = context;

        public Task<RefreshTokenEntity> FindAsync(Guid tokenId) =>
            _context.RefreshTokens.FirstOrDefaultAsync(token => token.TokenId == tokenId);

        public void Replace(RefreshTokenEntity existingRefreshToken, RefreshTokenEntity newRefreshToken)
        {
            _context.RefreshTokens.Remove(existingRefreshToken);
            _context.RefreshTokens.Add(newRefreshToken);
        }

        public void Add(RefreshTokenEntity refreshToken) =>
            _context.RefreshTokens.Add(refreshToken);
    }
}

namespace Mitekat.Persistence.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Mitekat.Persistence.Context;
    using Mitekat.Persistence.Entities;

    public class RefreshTokensRepository
    {
        private readonly DatabaseContext _context;

        public RefreshTokensRepository(DatabaseContext context) =>
            _context = context;

        public Task<RefreshToken> FindRefreshTokenAsync(Guid tokenId) =>
            _context.RefreshTokens.FirstOrDefaultAsync(token => token.TokenId == tokenId);

        public void ReplaceToken(RefreshToken existingRefreshToken, RefreshToken newRefreshToken)
        {
            _context.RefreshTokens.Remove(existingRefreshToken);
            _context.RefreshTokens.Add(newRefreshToken);
        }

        public void AddRefreshToken(RefreshToken refreshToken) =>
            _context.RefreshTokens.Add(refreshToken);
    }
}

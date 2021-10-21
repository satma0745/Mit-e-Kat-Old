namespace Mitekat.Core.Helpers.AuthToken
{
    using System;
    using Mitekat.Core.Persistence.Entities;

    public interface IAuthTokenHelper
    {
        ITokenPairInfo IssueTokenPair(Guid ownerId, UserRole ownerRole);
        IRefreshTokenInfo ParseRefreshToken(string refreshToken);
    }
}

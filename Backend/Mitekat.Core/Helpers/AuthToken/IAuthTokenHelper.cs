namespace Mitekat.Core.Helpers.AuthToken
{
    using System;
    using Mitekat.Core.Persistence.Entities;

    public interface IAuthTokenHelper
    {
        ITokenPairInfo IssueTokenPair(Guid ownerId, UserRole ownerRole);
        IAccessTokenInfo ParseAccessToken(string accessToken);
        IRefreshTokenInfo ParseRefreshToken(string refreshToken);
    }
}

namespace Mitekat.Core.Helpers.AuthToken
{
    using System;

    public interface IAuthTokenHelper
    {
        ITokenPairInfo IssueTokenPair(Guid ownerId);
        IAccessTokenInfo ParseAccessToken(string accessToken);
        IRefreshTokenInfo ParseRefreshToken(string refreshToken);
    }
}

namespace Mitekat.Core.Helpers.AuthToken
{
    using System;

    internal interface IAuthTokenHelper
    {
        public TokenPairInfo IssueTokenPair(Guid ownerId);
        
        public AccessTokenInfo ParseAccessToken(string accessToken);

        public RefreshTokenInfo ParseRefreshToken(string refreshToken);
    }
}

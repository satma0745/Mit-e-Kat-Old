namespace Mitekat.Helpers.AuthTokens
{
    using System;
    using System.Collections.Generic;
    using JWT.Algorithms;
    using JWT.Builder;
    using Microsoft.Extensions.Options;
    using Mitekat.Core.Helpers.AuthToken;
    using Mitekat.Core.Persistence.Entities;
    using Mitekat.Helpers.Configuration;
    using Mitekat.Helpers.Extensions;

    internal class AuthTokenHelper : IAuthTokenHelper
    {
        private JwtBuilder Token => JwtBuilder
            .Create()
            .WithAlgorithm(new HMACSHA512Algorithm())
            .WithSecret(_secretKey)
            .MustVerifySignature();

        private readonly string _secretKey;
        private readonly TimeSpan _accessTokenLifetime;
        private readonly TimeSpan _refreshTokenLifetime;

        public AuthTokenHelper(IOptions<AuthConfiguration> authConfigurationOptions)
        {
            var authConfiguration = authConfigurationOptions.Value;

            _secretKey = authConfiguration.SecretKey;
            _accessTokenLifetime = TimeSpan.FromMinutes(authConfiguration.AccessTokenLifetimeInMinutes);
            _refreshTokenLifetime = TimeSpan.FromDays(authConfiguration.RefreshTokenLifetimeInDays);
        }

        public IRefreshTokenInfo ParseRefreshToken(string refreshToken)
        {
            try
            {
                var payload = Token.Decode<IDictionary<string, object>>(refreshToken);

                var tokenId = Guid.Parse((string) payload!["jti"]);
                var ownerId = Guid.Parse((string) payload!["sub"]);

                var unixEpochStartTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                var tokenExpirationTimeInSeconds = (long) payload!["exp"];
                var expirationTime = unixEpochStartTime.AddSeconds(tokenExpirationTimeInSeconds);

                return new RefreshTokenInfo
                {
                    TokenId = tokenId,
                    OwnerId = ownerId,
                    ExpirationTime = expirationTime,
                    EncodedToken = refreshToken
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ITokenPairInfo IssueTokenPair(Guid ownerId, UserRole ownerRole) =>
            new TokenPairInfo
            {
                AccessToken = IssueAccessToken(ownerId, ownerRole),
                RefreshToken = IssueRefreshToken(ownerId)
            };

        private AccessTokenInfo IssueAccessToken(Guid ownerId, UserRole ownerRole)
        {
            var encodedAccessToken = Token
                .WithOwnerId(ownerId)
                .WithOwnerRole(ownerRole)
                .WithLifetime(_accessTokenLifetime)
                .Encode();

            return new AccessTokenInfo
            {
                EncodedToken = encodedAccessToken
            };
        }

        private RefreshTokenInfo IssueRefreshToken(Guid ownerId)
        {
            var refreshTokenId = Guid.NewGuid();
            var encodedRefreshToken = Token
                .WithOwnerId(ownerId)
                .WithTokenId(refreshTokenId)
                .WithLifetime(_refreshTokenLifetime)
                .Encode();

            var refreshTokenExpirationTime = DateTime.UtcNow.Add(_refreshTokenLifetime);
            return new RefreshTokenInfo
            {
                TokenId = refreshTokenId,
                OwnerId = ownerId,
                ExpirationTime = refreshTokenExpirationTime,
                EncodedToken = encodedRefreshToken
            };
        }
    }
}

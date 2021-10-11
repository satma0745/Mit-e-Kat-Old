﻿namespace Mitekat.Helpers.AuthTokens
{
    using System;
    using System.Collections.Generic;
    using JWT.Algorithms;
    using JWT.Builder;
    using Microsoft.Extensions.Options;
    using Mitekat.Core.Helpers.AuthToken;
    using Mitekat.Helpers.Configuration;
    using Mitekat.Helpers.Extensions;

    internal class AuthTokenHelper : IAuthTokenHelper
    {
        private JwtBuilder Token => JwtBuilder
            .Create()
            .WithAlgorithm(new HMACSHA512Algorithm())
            .WithSecret(_authConfiguration.SecretKey)
            .MustVerifySignature();
        
        private TimeSpan AccessTokenLifetime => TimeSpan.FromMinutes(_authConfiguration.AccessTokenLifetimeInMinutes);
        private TimeSpan RefreshTokenLifetime => TimeSpan.FromDays(_authConfiguration.RefreshTokenLifetimeInDays);

        private readonly AuthConfiguration _authConfiguration;

        public AuthTokenHelper(IOptions<AuthConfiguration> authConfigurationOptions) =>
            _authConfiguration = authConfigurationOptions.Value;

        public IAccessTokenInfo ParseAccessToken(string accessToken)
        {
            try
            {
                var payload = Token.Decode<IDictionary<string, object>>(accessToken);
                var ownerId = Guid.Parse((string) payload!["sub"]);

                return new AccessTokenInfo
                {
                    OwnerId = ownerId,
                    EncodedToken = accessToken
                };
            }
            catch (Exception)
            {
                return null;
            }
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

        public ITokenPairInfo IssueTokenPair(Guid ownerId) =>
            new TokenPairInfo
            {
                AccessToken = IssueAccessToken(ownerId),
                RefreshToken = IssueRefreshToken(ownerId)
            };

        private AccessTokenInfo IssueAccessToken(Guid ownerId)
        {
            var encodedAccessToken = Token
                .WithOwnerId(ownerId)
                .WithLifetime(AccessTokenLifetime)
                .Encode();

            return new AccessTokenInfo
            {
                OwnerId = ownerId,
                EncodedToken = encodedAccessToken
            };
        }

        private RefreshTokenInfo IssueRefreshToken(Guid ownerId)
        {
            var refreshTokenId = Guid.NewGuid();
            var encodedRefreshToken = Token
                .WithOwnerId(ownerId)
                .WithTokenId(refreshTokenId)
                .WithLifetime(RefreshTokenLifetime)
                .Encode();
            
            var refreshTokenExpirationTime = DateTime.UtcNow.Add(RefreshTokenLifetime);
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
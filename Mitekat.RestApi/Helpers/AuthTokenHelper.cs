namespace Mitekat.RestApi.Helpers
{
    using System;
    using System.Collections.Generic;
    using JWT.Algorithms;
    using JWT.Builder;
    using Microsoft.Extensions.Configuration;
    using Mitekat.RestApi.Extensions.Configuration;

    public class AuthTokenHelper
    {
        private JwtBuilder Token => JwtBuilder
            .Create()
            .WithAlgorithm(_algorithm)
            .WithSecret(_secret)
            .MustVerifySignature();
        
        private readonly string _secret;
        private readonly TimeSpan _accessTokenLifetime;
        private readonly TimeSpan _refreshTokenLifetime;
        private readonly IJwtAlgorithm _algorithm;

        public AuthTokenHelper(IConfiguration configuration)
        {
            _secret = configuration.GetAuthSecretKey();
            _accessTokenLifetime = TimeSpan.FromMinutes(configuration.GetAccessTokenLifetime());
            _refreshTokenLifetime = TimeSpan.FromDays(configuration.GetRefreshTokenLifetime());
            _algorithm = new HMACSHA512Algorithm();
        }

        public AccessTokenInfo ParseAccessToken(string accessToken)
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

        public RefreshTokenInfo ParseRefreshToken(string refreshToken)
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

        public TokenPair IssueTokenPair(Guid ownerId) =>
            new TokenPair
            {
                AccessToken = IssueAccessToken(ownerId),
                RefreshToken = IssueRefreshToken(ownerId)
            };

        private AccessTokenInfo IssueAccessToken(Guid ownerId)
        {
            var encodedAccessToken = Token
                .WithOwnerId(ownerId)
                .WithLifetime(_accessTokenLifetime)
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

    public class TokenPair
    {
        public AccessTokenInfo AccessToken { get; init; }
        public RefreshTokenInfo RefreshToken { get; init; }
    }

    public class AccessTokenInfo
    {
        public Guid OwnerId { get; init; }
        public string EncodedToken { get; init; }
    }

    public class RefreshTokenInfo
    {
        public Guid TokenId { get; init; }
        public Guid OwnerId { get; init; }
        public DateTime ExpirationTime { get; init; }
        public string EncodedToken { get; init; }
    }
}

namespace Mitekat.RestApi.Helpers
{
    using System;
    using JWT.Algorithms;
    using JWT.Builder;
    using Microsoft.Extensions.Configuration;
    using Mitekat.RestApi.Extensions.Configuration;

    public class AuthTokenHelper
    {
        private readonly string _authSecret;
        private readonly int _accessTokenLifetime;

        public AuthTokenHelper(IConfiguration configuration)
        {
            _authSecret = configuration.GetAuthSecretKey();
            _accessTokenLifetime = configuration.GetAccessTokenLifetime();
        }

        public string IssueAccessToken(Guid ownerId) =>
            JwtBuilder.Create()
                .SetTokenOwnerId(ownerId)
                .SetTokenLifetime(TimeSpan.FromMinutes(_accessTokenLifetime))
                .EncodeToken(new HMACSHA512Algorithm(), _authSecret);

        public Guid? ParseAccessToken(string token)
        {
            var payload = JwtBuilder.Create().DecodeToken(token, new HMACSHA512Algorithm(), _authSecret);
            return payload is null ? null : Guid.Parse((string)payload!["sub"]);
        }
    }
}

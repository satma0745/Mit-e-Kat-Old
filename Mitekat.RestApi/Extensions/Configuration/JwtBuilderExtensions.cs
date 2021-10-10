namespace Mitekat.RestApi.Extensions.Configuration
{
    using System;
    using System.Collections.Generic;
    using JWT.Algorithms;
    using JWT.Builder;

    internal static class JwtBuilderExtensions
    {
        public static JwtBuilder SetTokenOwnerId(this JwtBuilder jwtBuilder, Guid ownerId) =>
            jwtBuilder.AddClaim("sub", ownerId.ToString());
        
        public static JwtBuilder SetTokenLifetime(this JwtBuilder jwtBuilder, TimeSpan lifetime) =>
            jwtBuilder.AddClaim("exp", DateTimeOffset.UtcNow.Add(lifetime).ToUnixTimeSeconds());

        public static string EncodeToken(this JwtBuilder jwtBuilder, IJwtAlgorithm algorithm, string secret) =>
            jwtBuilder
                .WithAlgorithm(algorithm)
                .WithSecret(secret)
                .Encode();

        public static IDictionary<string, object> DecodeToken(
            this JwtBuilder jwtBuilder,
            string token,
            IJwtAlgorithm algorithm,
            string secret)
        {
            try
            {
                return jwtBuilder
                    .WithAlgorithm(algorithm)
                    .WithSecret(secret)
                    .MustVerifySignature()
                    .Decode<IDictionary<string, object>>(token);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

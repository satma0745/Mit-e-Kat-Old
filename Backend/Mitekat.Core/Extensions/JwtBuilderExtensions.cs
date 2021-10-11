namespace Mitekat.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using JWT.Algorithms;
    using JWT.Builder;

    internal static class JwtBuilderExtensions
    {
        public static JwtBuilder WithOwnerId(this JwtBuilder jwtBuilder, Guid ownerId) =>
            jwtBuilder.AddClaim("sub", ownerId.ToString());
        
        public static JwtBuilder WithTokenId(this JwtBuilder jwtBuilder, Guid tokenId) =>
            jwtBuilder.AddClaim("jti", tokenId.ToString());
        
        public static JwtBuilder WithLifetime(this JwtBuilder jwtBuilder, TimeSpan lifetime) =>
            jwtBuilder.AddClaim("exp", DateTimeOffset.UtcNow.Add(lifetime).ToUnixTimeSeconds());

        public static IDictionary<string, object> Decode(
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

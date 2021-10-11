namespace Mitekat.Helpers.Extensions
{
    using System;
    using JWT.Builder;

    internal static class JwtBuilderExtensions
    {
        public static JwtBuilder WithOwnerId(this JwtBuilder jwtBuilder, Guid ownerId) =>
            jwtBuilder.AddClaim("sub", ownerId.ToString());
        
        public static JwtBuilder WithTokenId(this JwtBuilder jwtBuilder, Guid tokenId) =>
            jwtBuilder.AddClaim("jti", tokenId.ToString());
        
        public static JwtBuilder WithLifetime(this JwtBuilder jwtBuilder, TimeSpan lifetime) =>
            jwtBuilder.AddClaim("exp", DateTimeOffset.UtcNow.Add(lifetime).ToUnixTimeSeconds());
    }
}

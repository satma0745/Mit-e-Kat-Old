namespace Mitekat.Helpers.Extensions
{
    using System;
    using JWT.Builder;
    using Mitekat.Core.Persistence.Entities;

    internal static class JwtBuilderExtensions
    {
        public static JwtBuilder WithOwnerId(this JwtBuilder jwtBuilder, Guid ownerId) =>
            jwtBuilder.AddClaim("sub", ownerId.ToString());

        public static JwtBuilder WithOwnerRole(this JwtBuilder jwtBuilder, UserRole ownerRole) =>
            jwtBuilder.AddClaim("role", ownerRole.ToString());
        
        public static JwtBuilder WithTokenId(this JwtBuilder jwtBuilder, Guid tokenId) =>
            jwtBuilder.AddClaim("jti", tokenId.ToString());
        
        public static JwtBuilder WithLifetime(this JwtBuilder jwtBuilder, TimeSpan lifetime) =>
            jwtBuilder.AddClaim("exp", DateTimeOffset.UtcNow.Add(lifetime).ToUnixTimeSeconds());
    }
}

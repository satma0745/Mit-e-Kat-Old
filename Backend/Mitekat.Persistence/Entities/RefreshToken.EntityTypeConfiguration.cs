namespace Mitekat.Persistence.Entities
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class RefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("refresh_tokens");
            builder.HasKey(token => token.TokenId);

            builder
                .Property(token => token.TokenId)
                .HasColumnName("token_id");

            builder
                .Property(token => token.ExpirationTime)
                .HasColumnName("expiration_time");
        }
    }
}

namespace Mitekat.Persistence.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Mitekat.Core.Persistence.Entities;

    internal class RefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
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

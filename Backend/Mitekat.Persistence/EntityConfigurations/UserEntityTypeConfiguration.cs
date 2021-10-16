namespace Mitekat.Persistence.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Mitekat.Core.Persistence.Entities;

    internal class UserEntityTypeConfiguration: IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> userBuilder)
        {
            userBuilder.ToTable("users");
            
            userBuilder.HasKey(user => user.Id);
            userBuilder.HasIndex(user => user.Username).IsUnique();

            userBuilder
                .Property(user => user.Id)
                .HasColumnName("id");

            userBuilder
                .Property(user => user.Username)
                .HasColumnName("username")
                .HasMaxLength(20)
                .IsRequired();

            userBuilder.OwnsOne(user => user.Password, passwordBuilder =>
            {
                passwordBuilder
                    .Property(password => password.Hash)
                    .HasColumnName("password_hash")
                    .HasMaxLength(60)
                    .IsFixedLength()
                    .IsRequired();

                passwordBuilder
                    .Property(password => password.Salt)
                    .HasColumnName("password_salt")
                    .HasMaxLength(29)
                    .IsFixedLength()
                    .IsRequired();
            });
            userBuilder.Navigation(user => user.Password).IsRequired();

            userBuilder
                .Property(user => user.Role)
                .HasColumnName("role")
                .HasConversion<string>()
                .HasMaxLength(9)
                .HasDefaultValue(UserRole.User);
        }
    }
}

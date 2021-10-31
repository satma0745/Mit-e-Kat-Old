namespace Mitekat.Persistence.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Mitekat.Persistence.Context;
    using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

    [DbContext(typeof(DatabaseContext))]
    internal class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Mitekat.Core.Persistence.Entities.RefreshTokenEntity", b =>
            {
                b.Property<Guid>("TokenId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid")
                    .HasColumnName("token_id");

                b.Property<DateTime>("ExpirationTime")
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("expiration_time");

                b.HasKey("TokenId");

                b.ToTable("refresh_tokens");
            });

            modelBuilder.Entity("Mitekat.Core.Persistence.Entities.UserEntity", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("uuid")
                    .HasColumnName("id");

                b.Property<string>("Role")
                    .IsRequired()
                    .ValueGeneratedOnAdd()
                    .HasMaxLength(9)
                    .HasColumnType("character varying(9)")
                    .HasDefaultValue("User")
                    .HasColumnName("role");

                b.Property<string>("Username")
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnType("character varying(20)")
                    .HasColumnName("username");

                b.HasKey("Id");

                b.HasIndex("Username")
                    .IsUnique();

                b.ToTable("users");
            });

            modelBuilder.Entity("Mitekat.Core.Persistence.Entities.UserEntity", b =>
            {
                b.OwnsOne("Mitekat.Core.Persistence.Entities.UserPassword", "Password", b1 =>
                {
                    b1.Property<Guid>("UserEntityId")
                        .HasColumnType("uuid");

                    b1.Property<string>("Hash")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character(60)")
                        .HasColumnName("password_hash")
                        .IsFixedLength();

                    b1.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(29)
                        .HasColumnType("character(29)")
                        .HasColumnName("password_salt")
                        .IsFixedLength();

                    b1.HasKey("UserEntityId");

                    b1.ToTable("users");

                    b1.WithOwner()
                        .HasForeignKey("UserEntityId");
                });

                b.Navigation("Password")
                    .IsRequired();
            });
#pragma warning restore 612, 618
        }
    }
}

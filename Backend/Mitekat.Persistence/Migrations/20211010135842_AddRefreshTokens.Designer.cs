namespace Mitekat.Persistence.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Migrations;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
    using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
    using Mitekat.Persistence.Context;

    [DbContext(typeof(DatabaseContext))]
    [Migration("20211010135842_AddRefreshTokens")]
    internal partial class AddRefreshTokens
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Mitekat.Persistence.Entities.RefreshToken", b =>
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

            modelBuilder.Entity("Mitekat.Persistence.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

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

            modelBuilder.Entity("Mitekat.Persistence.Entities.User", b =>
                {
                    b.OwnsOne("Mitekat.Persistence.Entities.UserPassword", "Password", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Hash")
                                .IsRequired()
                                .HasMaxLength(60)
                                .HasColumnType("character(60)")
                                .HasColumnName("password_hash")
                                .IsFixedLength(true);

                            b1.Property<string>("Salt")
                                .IsRequired()
                                .HasMaxLength(29)
                                .HasColumnType("character(29)")
                                .HasColumnName("password_salt")
                                .IsFixedLength(true);

                            b1.HasKey("UserId");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Password")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

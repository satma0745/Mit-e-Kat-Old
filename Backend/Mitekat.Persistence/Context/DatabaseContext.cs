namespace Mitekat.Persistence.Context
{
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using Mitekat.Core.Persistence.Entities;

    internal class DatabaseContext : DbContext
    {
        // Private setters are required by EF Core
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public DbSet<UserEntity> Users { get; private set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public DbSet<RefreshTokenEntity> RefreshTokens { get; private set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

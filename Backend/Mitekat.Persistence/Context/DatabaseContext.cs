namespace Mitekat.Persistence.Context
{
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using Mitekat.Persistence.Entities;

    public class DatabaseContext : DbContext
    {
        // Private setters are required by EF Core
        public DbSet<User> Users { get; private set; }
        public DbSet<RefreshToken> RefreshTokens { get; private set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

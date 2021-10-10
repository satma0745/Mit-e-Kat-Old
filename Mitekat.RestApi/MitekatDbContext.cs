namespace Mitekat.RestApi
{
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using Mitekat.RestApi.Entities;

    public class MitekatDbContext : DbContext
    {
        // Private setters are required by EF Core
        public DbSet<User> Users { get; private set; }

        public MitekatDbContext(DbContextOptions<MitekatDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

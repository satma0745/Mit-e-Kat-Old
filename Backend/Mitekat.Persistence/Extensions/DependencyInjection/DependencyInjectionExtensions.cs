namespace Mitekat.Persistence.Extensions.DependencyInjection
{
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Mitekat.Persistence.Configuration;
    using Mitekat.Persistence.Context;
    using Mitekat.Persistence.UnitOfWork;

    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration) =>
            services
                .AddScoped<UnitOfWork>()
                .AddDbContext(configuration);

        private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var dbConfig = configuration.GetSection("Database").Get<DatabaseConfiguration>();
            var connectionString = dbConfig.ConnectionString;

            var migrationsAssembly = Assembly.GetExecutingAssembly().FullName;
            
            services.AddDbContext<DatabaseContext>(contextOptions => 
                contextOptions.UseNpgsql(connectionString, options => options.MigrationsAssembly(migrationsAssembly)));

            return services;
        }
    }
}

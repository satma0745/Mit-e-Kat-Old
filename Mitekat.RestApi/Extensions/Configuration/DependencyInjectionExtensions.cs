namespace Mitekat.RestApi.Extensions.Configuration
{
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Mitekat.RestApi.Helpers;
    using Mitekat.RestApi.Services;

    internal static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services) =>
            services.AddScoped<AuthService>();

        public static IServiceCollection AddHelpers(this IServiceCollection services) =>
            services
                .AddScoped<AuthTokenHelper>()
                .AddScoped<PasswordHelper>();

        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetDbConnectionString();
            var migrationsAssembly = Assembly.GetExecutingAssembly().FullName;

            return services.AddDbContext<MitekatDbContext>(contextOptions =>
                contextOptions.UseNpgsql(connectionString, options => options.MigrationsAssembly(migrationsAssembly)));
        }
    }
}

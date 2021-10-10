namespace Mitekat.RestApi.Extensions.Configuration
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Mitekat.RestApi.Configuration;
    using Mitekat.RestApi.Helpers;
    using Mitekat.RestApi.Services;

    internal static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddConfigurationOptions(
            this IServiceCollection services,
            IConfiguration configuration) =>
            services.Configure<AuthConfiguration>(configuration.GetSection("Auth"));
        
        public static IServiceCollection AddServices(this IServiceCollection services) =>
            services.AddScoped<AuthService>();

        public static IServiceCollection AddHelpers(this IServiceCollection services) =>
            services
                .AddScoped<AuthTokenHelper>()
                .AddScoped<PasswordHelper>();
    }
}

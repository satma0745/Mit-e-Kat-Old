namespace Mitekat.RestApi.Extensions.Configuration
{
    using Microsoft.Extensions.DependencyInjection;
    using Mitekat.RestApi.Helpers;
    using Mitekat.RestApi.Services;

    internal static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services) =>
            services.AddSingleton<UserService>();

        public static IServiceCollection AddHelpers(this IServiceCollection services) =>
            services.AddSingleton<AuthTokenHelper>();
    }
}

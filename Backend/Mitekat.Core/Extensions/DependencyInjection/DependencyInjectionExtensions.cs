namespace Mitekat.Core.Extensions.DependencyInjection
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Mitekat.Core.Helpers.AuthToken;
    using Mitekat.Core.Helpers.PasswordHashing;
    using Mitekat.Core.Services;

    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddHelpers(configuration)
                .AddServices();

        private static IServiceCollection AddHelpers(this IServiceCollection services, IConfiguration configuration) =>
            services
                .Configure<AuthConfiguration>(configuration.GetSection("Auth"))
                .AddScoped<IAuthTokenHelper, AuthTokenHelper>()
                .AddScoped<IPasswordHashingHelper, PasswordHashingHelper>();

        private static IServiceCollection AddServices(this IServiceCollection services) =>
            services.AddScoped<IAuthService, AuthService>();
    }
}

namespace Mitekat.Core.Extensions.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using Mitekat.Core.Services;

    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services) =>
            services.AddScoped<IAuthService, AuthService>();
    }
}

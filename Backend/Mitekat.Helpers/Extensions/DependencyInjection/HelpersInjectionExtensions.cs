namespace Mitekat.Helpers.Extensions.DependencyInjection
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Mitekat.Core.Helpers.AuthToken;
    using Mitekat.Core.Helpers.PasswordHashing;
    using Mitekat.Helpers.AuthTokens;
    using Mitekat.Helpers.Configuration;
    using Mitekat.Helpers.PasswordHashing;

    public static class HelpersInjectionExtensions
    {
        public static IServiceCollection AddHelpers(this IServiceCollection services, IConfiguration configuration) =>
            services
                .Configure<AuthConfiguration>(configuration.GetSection("Auth"))
                .AddScoped<IAuthTokenHelper, AuthTokenHelper>()
                .AddScoped<IPasswordHashingHelper, PasswordHashingHelper>();
    }
}

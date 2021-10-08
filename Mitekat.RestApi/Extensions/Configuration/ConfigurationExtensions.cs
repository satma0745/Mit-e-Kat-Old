namespace Mitekat.RestApi.Extensions.Configuration
{
    using Microsoft.Extensions.Configuration;

    internal static class ConfigurationExtensions
    {
        public static string GetApplicationTitle(this IConfiguration configuration) =>
            configuration["Application:Title"];

        public static string GetApplicationVersion(this IConfiguration configuration) =>
            configuration["Application:Version"];
    }
}

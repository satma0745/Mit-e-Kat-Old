namespace Mitekat.RestApi.Extensions.Configuration
{
    using Microsoft.Extensions.Configuration;

    internal static class ConfigurationExtensions
    {
        public static string GetApplicationTitle(this IConfiguration configuration) =>
            configuration["Application:Title"];

        public static string GetApplicationVersion(this IConfiguration configuration) =>
            configuration["Application:Version"];

        public static string GetAuthSecretKey(this IConfiguration configuration) =>
            configuration["MITEKAT_AUTH_SECRET_KEY"];

        public static int GetAccessTokenLifetime(this IConfiguration configuration) =>
            int.Parse(configuration["MITEKAT_AUTH_ACCESS_TOKEN_LIFETIME"]);

        public static int GetRefreshTokenLifetime(this IConfiguration configuration) =>
            int.Parse(configuration["MITEKAT_AUTH_REFRESH_TOKEN_LIFETIME"]);

        public static string GetDbConnectionString(this IConfiguration configuration) =>
            configuration["MITEKAT_DB_CONNECTION_STRING"];
    }
}

namespace Mitekat.RestApi.Extensions
{
    using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using Mitekat.RestApi.Filters;
    using Swashbuckle.AspNetCore.SwaggerGen;

    internal static class SwaggerConfigurationExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddSwaggerGen(options =>
                {
                    options.AddApiInfo(configuration);
                    options.AddAuthentication();
                })
                .AddFluentValidationRulesToSwagger();

        public static IApplicationBuilder UseSwagger(
            this IApplicationBuilder application,
            IConfiguration configuration) =>
            application
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    var appConfig = configuration.GetSection("Application").Get<ApplicationConfiguration>();

                    var url = $"/swagger/{appConfig.Version}/swagger.json";
                    var name = $"{appConfig.Title} v{appConfig.Version}";

                    options.SwaggerEndpoint(url, name);
                });

        private static void AddApiInfo(this SwaggerGenOptions options, IConfiguration configuration)
        {
            var appConfig = configuration.GetSection("Application").Get<ApplicationConfiguration>();

            var openApiInfo = new OpenApiInfo
            {
                Title = appConfig.Title,
                Version = appConfig.Version
            };

            options.SwaggerDoc(openApiInfo.Version, openApiInfo);
        }

        private static void AddAuthentication(this SwaggerGenOptions options)
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Put Your access token here (drop **Bearer** prefix):",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            options.OperationFilter<SwaggerPadlockIndicatorFilter>();
        }

        private class ApplicationConfiguration
        {
            public string Title { get; set; }
            public string Version { get; set; }
        }
    }
}

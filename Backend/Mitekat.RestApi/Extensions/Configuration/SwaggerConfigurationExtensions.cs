namespace Mitekat.RestApi.Extensions.Configuration
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using Mitekat.RestApi.Configuration;

    internal static class SwaggerConfigurationExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration) =>
            services.AddSwaggerGen(options =>
            {
                var appConfig = configuration.GetSection("Application").Get<ApplicationConfiguration>();
                
                var openApiInfo = new OpenApiInfo
                {
                    Title = appConfig.Title,
                    Version = appConfig.Version
                };
                
                options.SwaggerDoc(openApiInfo.Version, openApiInfo);
            });

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
    }
}

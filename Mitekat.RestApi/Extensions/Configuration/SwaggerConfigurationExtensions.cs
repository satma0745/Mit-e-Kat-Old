namespace Mitekat.RestApi.Extensions.Configuration
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    internal static class SwaggerConfigurationExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration) =>
            services.AddSwaggerGen(options =>
            {
                var openApiInfo = new OpenApiInfo
                {
                    Title = configuration.GetApplicationTitle(),
                    Version = configuration.GetApplicationVersion()
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
                    var version = configuration.GetApplicationVersion();
                    var title = configuration.GetApplicationTitle();
                    
                    var url = $"/swagger/{version}/swagger.json";
                    var name = $"{title} v{version}";
                    
                    options.SwaggerEndpoint(url, name);
                });
    }
}

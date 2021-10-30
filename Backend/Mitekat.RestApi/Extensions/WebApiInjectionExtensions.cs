namespace Mitekat.RestApi.Extensions
{
    using System.Reflection;
    using FluentValidation.AspNetCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    internal static class WebApiInjectionExtensions
    {
        public static void AddWebApi(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddSwagger(configuration)
                .AddAuthentication(configuration)
                .AddControllers()
                .AddFluentValidation(options =>
                {
                    options.DisableDataAnnotationsValidation = true;
                    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);
                });
    }
}

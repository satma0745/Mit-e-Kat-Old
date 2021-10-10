namespace Mitekat.RestApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Mitekat.RestApi.Extensions.Configuration;

    internal class Startup
    {
        private readonly IConfiguration _configuration;
        
        public Startup(IConfiguration configuration) =>
            _configuration = configuration;

        public void ConfigureServices(IServiceCollection services) =>
            services
                .AddDbContext(_configuration)
                .AddHelpers()
                .AddServices()
                .AddSwagger(_configuration)
                .AddControllers();

        public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                application
                    .UseDeveloperExceptionPage()
                    .UseSwagger(_configuration);
            }

            application
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}

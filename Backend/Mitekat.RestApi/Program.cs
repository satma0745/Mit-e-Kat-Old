namespace Mitekat.RestApi
{
    using System;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    internal static class Program
    {
        private static int Main(string[] arguments)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(arguments).Build().Run();
            }
            catch (Exception exception)
            {
                Log.Fatal(exception, "Host terminated unexpectedly");
                return 1;
            }
            
            return 0;
        }

        private static IHostBuilder CreateHostBuilder(string[] arguments) =>
            Host.CreateDefaultBuilder(arguments)
                .UseSerilog()
                .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>());
    }
}

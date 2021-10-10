namespace Mitekat.BlazorUI
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Mitekat.BlazorUI.Extensions;

    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("http://localhost:5000/api");

            await builder.Build().RunAsync();
        }
    }
}

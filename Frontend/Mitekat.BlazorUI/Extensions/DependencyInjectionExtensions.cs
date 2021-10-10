namespace Mitekat.BlazorUI.Extensions
{
    using System;
    using System.Net.Http;
    using Microsoft.Extensions.DependencyInjection;
    
    internal static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddHttpClient(this IServiceCollection services, string baseAddress) =>
            services.AddScoped(_ => new HttpClient {BaseAddress = new Uri(baseAddress)});
    }
}

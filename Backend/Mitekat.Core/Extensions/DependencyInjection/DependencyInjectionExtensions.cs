namespace Mitekat.Core.Extensions.DependencyInjection
{
    using System.Reflection;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using Mitekat.Core.Features.Shared.PipelineBehaviors;

    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services) =>
            services
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
    }
}

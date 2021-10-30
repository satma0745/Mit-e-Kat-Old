namespace Mitekat.Core.Extensions.DependencyInjection
{
    using System.Reflection;
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using Mitekat.Core.Features.Shared.PipelineBehaviors;

    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services) =>
            services
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(ErrorHandlingPipelineBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
                .AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}

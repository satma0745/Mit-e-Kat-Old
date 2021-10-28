namespace Mitekat.Core.Features.Shared.PipelineBehaviors
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.Extensions.Logging;

    internal class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

        public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger) =>
            _logger = logger;

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken _,
            RequestHandlerDelegate<TResponse> next)
        {
            var requestId = Guid.NewGuid();
            
            _logger.LogInformation("Handling request #{Id} of type {Type}", requestId, request.GetType().Name);
            _logger.LogInformation("Request #{Id} payload: {@Payload}", requestId, request);

            var stopwatch = Stopwatch.StartNew();
            var response = await next();
            stopwatch.Stop();
            
            _logger.LogInformation("Request #{Id} handled in {Time}ms", requestId, stopwatch.ElapsedMilliseconds);
            
            return response;
        }
    }
}
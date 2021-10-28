namespace Mitekat.Core.Features.Shared.PipelineBehaviors
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Mitekat.Core.Features.Shared.Responses;

    internal class LoggingPipelineBehavior<TAnyRequest, TResponse> : PipelineBehaviorBase<TAnyRequest, TResponse>
    {
        private readonly ILogger<LoggingPipelineBehavior<TAnyRequest, TResponse>> _logger;

        public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TAnyRequest, TResponse>> logger) =>
            _logger = logger;

        protected override async Task<Response<TResult>> HandleAsync<TRequest, TResult>(
            TRequest request,
            RequestHandlerDelegate<Response<TResult>> next)
        {
            _logger.LogInformation("Handling request #{RequestId}", request.RequestId);
            _logger.LogInformation("Request #{RequestId}: {@RequestPayload}", request.RequestId, request);

            var stopwatch = Stopwatch.StartNew();
            var response = await next();
            stopwatch.Stop();
            
            _logger.LogInformation(
                message: "Request #{RequestId} handled in {ElapsedTime}ms",
                args: new object[] { request.RequestId, stopwatch.ElapsedMilliseconds });
            
            return response;
        }
    }
}
namespace Mitekat.Core.Features.Shared.PipelineBehaviors
{
    using System;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Mitekat.Core.Features.Shared.Responses;

    internal class ErrorHandlingPipelineBehavior<TAnyRequest, TResponse> : PipelineBehaviorBase<TAnyRequest, TResponse>
    {
        private readonly ILogger<ErrorHandlingPipelineBehavior<TAnyRequest, TResponse>> _logger;

        public ErrorHandlingPipelineBehavior(ILogger<ErrorHandlingPipelineBehavior<TAnyRequest, TResponse>> logger) =>
            _logger = logger;

        protected override async Task<Response<TResult>> HandleAsync<TRequest, TResult>(
            TRequest request,
            RequestHandlerDelegate<Response<TResult>> next)
        {
            try
            {
                return await next();
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    exception: exception,
                    message: "Unhandled exception occurred while processing request #{RequestId}",
                    args: request.RequestId);

                return Response<TResult>.Failure(Error.Unhandled);
            }
        }
    }
}
namespace Mitekat.Core.Features.Shared.PipelineBehaviors
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Mitekat.Core.Features.Shared.Responses;

    internal class ValidationPipelineBehavior<TAnyRequest, TResponse> : PipelineBehaviorBase<TAnyRequest, TResponse>
        where TAnyRequest : class
    {
        private readonly ILogger<ValidationPipelineBehavior<TAnyRequest, TResponse>> _logger;
        private readonly IEnumerable<IValidator<TAnyRequest>> _requestValidators;

        public ValidationPipelineBehavior(
            IEnumerable<IValidator<TAnyRequest>> requestValidators,
            ILogger<ValidationPipelineBehavior<TAnyRequest, TResponse>> logger)
        {
            _requestValidators = requestValidators;
            _logger = logger;
        }

        protected override async Task<Response<TResult>> HandleAsync<TRequest, TResult>(
            TRequest request,
            RequestHandlerDelegate<Response<TResult>> next)
        {
            foreach (var requestValidator in _requestValidators)
            {
                var validationResult = await requestValidator.ValidateAsync(request as TAnyRequest);
                if (!validationResult.IsValid)
                {
                    var validationErrors = validationResult.Errors
                        .GroupBy(failure => failure.PropertyName, failure => failure.ErrorMessage)
                        .ToDictionary(group => group.Key, group => group.First());

                    _logger.LogError(
                        message: "Validation failed for request #{RequestId}: {@ValidationErrors}",
                        args: new object[] {request.RequestId, validationErrors});

                    return Response<TResult>.Failure(Error.Unhandled);
                }
            }

            return await next();
        }
    }
}

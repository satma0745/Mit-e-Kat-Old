namespace Mitekat.Core.Features.Shared.PipelineBehaviors
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Mitekat.Core.Extensions;
    using Mitekat.Core.Features.Shared.Requests;
    using Mitekat.Core.Features.Shared.Responses;

    internal abstract class PipelineBehaviorBase<TAnyRequest, TResponse> : IPipelineBehavior<TAnyRequest, TResponse>
    {
        public Task<TResponse> Handle(
            TAnyRequest request,
            CancellationToken _,
            RequestHandlerDelegate<TResponse> next)
        {
            var isSupportedResponseType = typeof(TResponse).DerivesFrom(typeof(Response<>));
            if (!isSupportedResponseType)
            {
                throw new Exception("Unsupported response type.");
            }

            var isSupportedRequestType = typeof(TAnyRequest).DerivesFrom(typeof(RequestBase<>));
            if (!isSupportedRequestType)
            {
                throw new Exception("Unsupported request type.");
            }

            const string methodName = nameof(HandleAsync);
            const BindingFlags methodType = BindingFlags.Instance | BindingFlags.NonPublic;

            var requestType = typeof(TAnyRequest);
            var resultType = typeof(TResponse).GenericTypeArguments.Single();

            var handleAsyncMethod = GetType()
                .GetMethod(methodName, methodType)!
                .MakeGenericMethod(requestType, resultType);

            return (Task<TResponse>) handleAsyncMethod.Invoke(this, new object[] {request, next});
        }

        protected abstract Task<Response<TResult>> HandleAsync<TRequest, TResult>(
            TRequest request,
            RequestHandlerDelegate<Response<TResult>> next)
            where TRequest : RequestBase<TResult>;
    }
}

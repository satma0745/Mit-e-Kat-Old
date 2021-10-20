namespace Mitekat.Core.Features.Shared
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Mitekat.Core.Features.Shared.Responses;
    
    // Handler for response with result payload
    internal abstract class RequestHandlerBase<TRequest, TResult> : IRequestHandler<TRequest, Response<TResult>>
        where TRequest : IRequest<Response<TResult>>
    {
        protected static Response<TResult> Success(TResult result) =>
            Response<TResult>.Success(result);

        protected static Response<TResult> Failure(Error error) =>
            Response<TResult>.Failure(error);

        public Task<Response<TResult>> Handle(TRequest request, CancellationToken _) =>
            HandleAsync(request);

        protected abstract Task<Response<TResult>> HandleAsync(TRequest request);
    }
    
    // Handler for response without result payload
    internal abstract class RequestHandlerBase<TRequest> : IRequestHandler<TRequest, Response>
        where TRequest : IRequest<Response>
    {
        protected static Response Success() =>
            Response.Success();

        protected static Response Failure(Error error) =>
            Response.Failure(error);

        public Task<Response> Handle(TRequest request, CancellationToken _) =>
            HandleAsync(request);

        protected abstract Task<Response> HandleAsync(TRequest request);
    }
}

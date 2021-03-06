namespace Mitekat.Core.Features.Shared.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Mitekat.Core.Features.Shared.Responses;

    internal abstract class RequestHandlerBase<TRequest, TResult> : IRequestHandler<TRequest, Response<TResult>>
        where TRequest : IRequest<Response<TResult>>
    {
        protected static Response<TResult> Success(TResult result) =>
            Response<TResult>.Success(result);

        protected static Response<BlankResult> Success() =>
            Response<BlankResult>.Success(new BlankResult());

        protected static Response<TResult> Failure(Error error) =>
            Response<TResult>.Failure(error);

        public Task<Response<TResult>> Handle(TRequest request, CancellationToken _) =>
            HandleAsync(request);

        protected abstract Task<Response<TResult>> HandleAsync(TRequest request);
    }
}

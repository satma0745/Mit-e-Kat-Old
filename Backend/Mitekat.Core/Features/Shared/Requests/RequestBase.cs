namespace Mitekat.Core.Features.Shared.Requests
{
    using MediatR;
    using Mitekat.Core.Features.Shared.Responses;

    // Request for response with result payload
    public abstract record RequestBase<TResult> : IRequest<Response<TResult>>;
    
    // Request for response without result payload
    public abstract record RequestBase : IRequest<Response>;
}

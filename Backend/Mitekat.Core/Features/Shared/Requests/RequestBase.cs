namespace Mitekat.Core.Features.Shared.Requests
{
    using System;
    using MediatR;
    using Mitekat.Core.Features.Shared.Responses;

    // Request for response with result payload
    public abstract record RequestBase<TResult>(Guid RequestId) : IRequest<Response<TResult>>
    {
        protected RequestBase()
            : this(RequestId: Guid.NewGuid())
        {
        }
    }
    
    // Request for response without result payload
    public abstract record RequestBase : RequestBase<BlankResult>;
}

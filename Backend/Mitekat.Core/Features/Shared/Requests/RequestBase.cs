namespace Mitekat.Core.Features.Shared.Requests
{
    using System;
    using MediatR;
    using Mitekat.Core.Features.Shared.Responses;

    // Request for response with result payload
    public abstract class RequestBase<TResult> : IRequest<Response<TResult>>
    {
        public Guid RequestId { get; }

        protected RequestBase() =>
            RequestId = Guid.NewGuid();
    }
    
    // Request for response without result payload
    public abstract class RequestBase : RequestBase<BlankResult>
    {
    }
}

namespace Mitekat.Core.Features.Shared.Requests
{
    using System;
    using MediatR;
    using Mitekat.Core.Features.Shared.Responses;

    public abstract class RequestBase<TResult> : IRequest<Response<TResult>>
    {
        public Guid RequestId { get; }

        protected RequestBase() =>
            RequestId = Guid.NewGuid();
    }
}

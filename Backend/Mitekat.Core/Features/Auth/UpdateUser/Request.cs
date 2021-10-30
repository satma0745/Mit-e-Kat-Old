namespace Mitekat.Core.Features.Auth.UpdateUser
{
    using System;
    using Mitekat.Core.Features.Shared.Requests;
    using Mitekat.Core.Features.Shared.Responses;

    public class UpdateUserRequest : RequestBase<BlankResult>
    {
        public Guid Id { get; }
        
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string Username { get; private set; }
        
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string Password { get; private set; }
        
        public IRequester Requester { get; }

        public UpdateUserRequest(Guid id, IRequester requester)
        {
            Id = id;
            Requester = requester;
        }
    }
}

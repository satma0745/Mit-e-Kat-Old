namespace Mitekat.Core.Features.Auth.UpdateUser
{
    using System;
    using Mitekat.Core.Features.Shared.Requests;
    using Mitekat.Core.Features.Shared.Responses;

    public class UpdateUserRequest : RequestBase<BlankResult>
    {
        public Guid Id { get; }
        public string Username { get; }
        public string Password { get; }
        public IRequester Requester { get; }

        public UpdateUserRequest(Guid id, string username, string password, IRequester requester)
        {
            Id = id;
            Username = username;
            Password = password;
            Requester = requester;
        }
    }
}
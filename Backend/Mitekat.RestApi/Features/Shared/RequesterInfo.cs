namespace Mitekat.RestApi.Features.Shared
{
    using System;
    using Mitekat.Core.Features.Shared.Requests;
    using Mitekat.Core.Persistence.Entities;

    internal class RequesterInfo : IRequester
    {
        public Guid Id { get; }
        public UserRole Role { get; }

        public RequesterInfo(Guid id, UserRole role)
        {
            Id = id;
            Role = role;
        }
    }
}

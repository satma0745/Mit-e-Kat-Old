namespace Mitekat.Core.Features.Shared.Requests
{
    using System;
    using Mitekat.Core.Persistence.Entities;

    public interface IRequester
    {
        Guid Id { get; }
        UserRole Role { get; }
    }
}

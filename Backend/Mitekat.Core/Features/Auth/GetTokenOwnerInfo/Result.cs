namespace Mitekat.Core.Features.Auth.GetTokenOwnerInfo
{
    using System;
    using Mitekat.Core.Persistence.Entities;

    public class GetTokenOwnerInfoResult
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public Guid Id { get; private set; }
        
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public string Username { get; private set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public UserRole Role { get; private set; }
    }
}

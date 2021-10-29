namespace Mitekat.Core.Features.Auth.GetTokenOwnerInfo
{
    using System;
    using Mitekat.Core.Persistence.Entities;

    public class GetTokenOwnerInfoResult
    {
        public static GetTokenOwnerInfoResult FromUserEntity(UserEntity user) =>
            new(user.Id, user.Username, user.Role);
        
        public Guid Id { get; }
        public string Username { get; }
        public UserRole Role { get; }

        private GetTokenOwnerInfoResult(Guid id, string username, UserRole role)
        {
            Id = id;
            Username = username;
            Role = role;
        }
    }
}
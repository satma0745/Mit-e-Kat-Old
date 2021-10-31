namespace Mitekat.Core.Persistence.Entities
{
    using System;

    public class UserEntity
    {
        public Guid Id { get; }
        public string Username { get; private set; }
        public UserPassword Password { get; private set; }
        public UserRole Role { get; }

        // For EF Core
        // ReSharper disable once UnusedMember.Local
        private UserEntity()
        {
        }

        public UserEntity(string username, UserPassword password, UserRole role = UserRole.User)
        {
            Id = Guid.NewGuid();
            Username = username;
            Password = password;
            Role = role;
        }

        public void Patch(string username, UserPassword password)
        {
            Username = username;
            Password = password;
        }
    }

    public class UserPassword
    {
        public readonly string Hash;
        public readonly string Salt;

        // For EF Core
        // ReSharper disable once UnusedMember.Local
        private UserPassword()
        {
        }

        public UserPassword(string hash, string salt)
        {
            Hash = hash;
            Salt = salt;
        }
    }

    public enum UserRole
    {
        User,
        Moderator,
        Admin
    }
}

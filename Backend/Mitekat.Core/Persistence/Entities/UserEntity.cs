namespace Mitekat.Core.Persistence.Entities
{
    using System;

    public class UserEntity
    {
        public readonly Guid Id;
        public readonly string Username;
        public readonly UserPassword Password;
        public readonly UserRole Role;

        // For EF Core
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
    }
    
    public class UserPassword
    {
        public readonly string Hash;
        public readonly string Salt;
        
        // For EF Core
        private UserPassword()
        {
        }

        public UserPassword(string hash, string salt)
        {
            Hash = hash;
            Salt = salt;
        }
    }

    public enum UserRole { User, Moderator, Admin }
}

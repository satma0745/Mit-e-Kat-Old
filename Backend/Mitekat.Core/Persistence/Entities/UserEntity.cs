﻿namespace Mitekat.Core.Persistence.Entities
{
    using System;

    public class UserEntity
    {
        public readonly Guid Id;
        public readonly string Username;
        public readonly UserPassword Password;

        // For EF Core
        private UserEntity()
        {
        }

        public UserEntity(string username, UserPassword password)
        {
            Id = Guid.NewGuid();
            Username = username;
            Password = password;
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
}
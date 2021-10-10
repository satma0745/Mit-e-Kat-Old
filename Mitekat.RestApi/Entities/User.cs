namespace Mitekat.RestApi.Entities
{
    using System;

    public class User
    {
        // Private setters are required by EF Core
        public Guid Id { get; private set; }
        public string Username  { get; private set; }
        public UserPassword Password  { get; private set; }

        // For EF Core
        private User()
        {
        }

        public User(string username, UserPassword password)
        {
            Id = Guid.NewGuid();
            Username = username;
            Password = password;
        }
    }
    
    public class UserPassword
    {
        // Private setters are required by EF Core
        public string Hash { get; private set; }
        public string Salt { get; private set; }
        
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

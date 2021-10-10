namespace Mitekat.RestApi.Helpers
{
    using BCrypt.Net;
    using Mitekat.RestApi.Entities;

    public class PasswordHelper
    {
        public UserPassword HashPassword(string plaintTextPassword) =>
            HashPassword(plaintTextPassword, BCrypt.GenerateSalt());
        
        public bool AreEqual(UserPassword hashedPassword, string plainTextPassword) =>
            HashPassword(plainTextPassword, hashedPassword.Salt).Hash == hashedPassword.Hash;
        
        private UserPassword HashPassword(string plaintTextPassword, string salt)
        {
            var hash = BCrypt.HashPassword(plaintTextPassword, salt);
            return new UserPassword(hash, salt);
        }
    }
}

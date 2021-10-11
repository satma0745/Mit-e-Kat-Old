namespace Mitekat.Core.Helpers.PasswordHashing
{
    using BCrypt.Net;
    using Mitekat.Core.Persistence.Entities;

    internal class PasswordHashingHelper : IPasswordHashingHelper
    {
        public UserPassword HashPassword(string plaintTextPassword) =>
            HashPassword(plaintTextPassword, BCrypt.GenerateSalt());
        
        public bool AreEqual(UserPassword hashedPassword, string plainTextPassword) =>
            HashPassword(plainTextPassword, hashedPassword.Salt).Hash == hashedPassword.Hash;
        
        private static UserPassword HashPassword(string plaintTextPassword, string salt)
        {
            var hash = BCrypt.HashPassword(plaintTextPassword, salt);
            return new UserPassword(hash, salt);
        }
    }
}

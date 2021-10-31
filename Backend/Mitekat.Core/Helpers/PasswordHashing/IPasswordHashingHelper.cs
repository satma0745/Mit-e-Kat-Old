namespace Mitekat.Core.Helpers.PasswordHashing
{
    using Mitekat.Core.Persistence.Entities;

    public interface IPasswordHashingHelper
    {
        UserPassword HashPassword(string plaintTextPassword);

        bool AreEqual(UserPassword hashedPassword, string plainTextPassword);
    }
}

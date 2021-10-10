namespace Mitekat.Persistence.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Mitekat.Persistence.Context;
    using Mitekat.Persistence.Entities;

    public class UsersRepository
    {
        private readonly DatabaseContext _context;

        public UsersRepository(DatabaseContext context) =>
            _context = context;

        public Task<User> FindUserAsync(Guid userId) =>
            _context.Users.FirstOrDefaultAsync(user => user.Id == userId);

        public Task<User> FindUserAsync(string username) =>
            _context.Users.FirstOrDefaultAsync(user => user.Username == username);

        public void AddUser(User user) =>
            _context.Users.Add(user);
    }
}

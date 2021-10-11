namespace Mitekat.Persistence.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Mitekat.Core.Persistence.Entities;
    using Mitekat.Core.Persistence.Repositories;
    using Mitekat.Persistence.Context;

    internal class UsersRepository : IUsersRepository
    {
        private readonly DatabaseContext _context;

        public UsersRepository(DatabaseContext context) =>
            _context = context;

        public Task<UserEntity> FindAsync(Guid userId) =>
            _context.Users.FirstOrDefaultAsync(user => user.Id == userId);

        public Task<UserEntity> FindAsync(string username) =>
            _context.Users.FirstOrDefaultAsync(user => user.Username == username);

        public void Add(UserEntity user) =>
            _context.Users.Add(user);
    }
}

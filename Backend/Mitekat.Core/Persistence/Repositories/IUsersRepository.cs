namespace Mitekat.Core.Persistence.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Mitekat.Core.Persistence.Entities;

    public interface IUsersRepository
    {
        Task<UserEntity> FindAsync(Guid userId);

        Task<UserEntity> FindAsync(string username);

        void Add(UserEntity user);
    }
}

namespace Mitekat.Core.Persistence.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Mitekat.Core.Persistence.Entities;

    public interface IUsersRepository
    {
        Task<UserEntity> FindAsync(Guid userId);

        Task<UserEntity> FindAsync(string username);

        Task<bool> UsernameTakenAsync(string username);

        Task<bool> UsernameTakenAsync(string username, Guid userIdToExclude);

        void Add(UserEntity user);
    }
}

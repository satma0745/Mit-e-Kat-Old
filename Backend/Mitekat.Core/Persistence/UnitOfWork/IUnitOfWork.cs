namespace Mitekat.Core.Persistence.UnitOfWork
{
    using System.Threading.Tasks;
    using Mitekat.Core.Persistence.Repositories;

    public interface IUnitOfWork
    {
        IUsersRepository Users { get; }
        IRefreshTokensRepository RefreshTokens { get; }

        Task SaveChangesAsync();
    }
}

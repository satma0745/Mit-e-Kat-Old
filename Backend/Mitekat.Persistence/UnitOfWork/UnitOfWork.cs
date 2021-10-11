namespace Mitekat.Persistence.UnitOfWork
{
    using System.Threading.Tasks;
    using Mitekat.Core.Persistence.Repositories;
    using Mitekat.Core.Persistence.UnitOfWork;
    using Mitekat.Persistence.Context;
    using Mitekat.Persistence.Repositories;

    internal class UnitOfWork : IUnitOfWork
    {
        public IUsersRepository Users { get; }
        public IRefreshTokensRepository RefreshTokens { get; }
        
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
            
            Users = new UsersRepository(context);
            RefreshTokens = new RefreshTokensRepository(context);
        }

        public Task SaveChangesAsync() =>
            _context.SaveChangesAsync();
    }
}

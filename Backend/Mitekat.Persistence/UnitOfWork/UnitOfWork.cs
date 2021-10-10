namespace Mitekat.Persistence.UnitOfWork
{
    using System.Threading.Tasks;
    using Mitekat.Persistence.Context;
    using Mitekat.Persistence.Repositories;

    public class UnitOfWork
    {
        public readonly UsersRepository Users;
        public readonly RefreshTokensRepository RefreshTokens;
        
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

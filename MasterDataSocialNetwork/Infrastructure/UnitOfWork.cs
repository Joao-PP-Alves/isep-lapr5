using System.Threading.Tasks;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DddNetCoreDbContext _context;

        public UnitOfWork(DddNetCoreDbContext context)
        {
            this._context = context;
        }

        public async Task<int> CommitAsync()
        {
            return await this._context.SaveChangesAsync();
        }
    }
}
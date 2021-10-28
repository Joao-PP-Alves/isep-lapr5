using DDDNetCore.Domain.Connections;
using DDDNetCore.Infrastructure.Shared;

namespace DDDNetCore.Infrastructure.Connections
{
    public class ConnectionRepository : BaseRepository<Connection, ConnectionId>,IConnectionRepository
    {
        public ConnectionRepository(DDDNetCoreDbContext context) : base(context.Connections)
        {
           
        }
    }
}
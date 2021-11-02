using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Connections
{
    public interface IConnectionRepository : IRepository<Connection,ConnectionId>
    {
        Task<List<Connection>> getPendentConnections(UserId id);
    }
}
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Domain.Connections;
using Microsoft.EntityFrameworkCore;
using DDDNetCore.Infrastructure.Shared;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Infrastructure.Connections
{
    public class ConnectionRepository : BaseRepository<Connection, ConnectionId>,IConnectionRepository
    {
        public ConnectionRepository(DDDNetCoreDbContext context) : base(context.Connections)
        {
           
        }

        public async Task<List<Connection>> getPendentConnections(UserId id){
            return await ((DbSet<Connection>)base.getContext()).Where(x => id.Equals(x.targetUser)).ToListAsync();
        }
    }
}
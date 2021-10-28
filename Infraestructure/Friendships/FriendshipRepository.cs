using DDDNetCore.Domain.Users;
using DDDNetCore.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace DDDNetCore.Infrastructure.Friendships {

    public class FriendshipRepository : BaseRepository<Friendship, FriendShipId>
    {
        public FriendshipRepository(DDDNetCoreDbContext context) : base(context.Friendships)
        {
            
        }
    }


}
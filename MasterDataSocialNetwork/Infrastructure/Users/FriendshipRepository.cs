using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Domain.Services.DTO;
using DDDNetCore.Domain.Users;
using DDDNetCore.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace DDDNetCore.Infrastructure.Users {

    public class FriendshipRepository : BaseRepository<Friendship, FriendshipId>, IFriendshipRepository
    {

        public FriendshipRepository(DDDNetCoreDbContext context) : base(context.Friendships)
        {
        }

    }
    }
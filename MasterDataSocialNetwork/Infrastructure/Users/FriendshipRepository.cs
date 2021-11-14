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
        private readonly DDDNetCoreDbContext _context;

        public FriendshipRepository(DDDNetCoreDbContext context) : base(context.Friendships)
        {
            _context = context;
        }

        public async Task<List<Friendship>> GetAllByUser(User user)
        {
            return await ((DbSet<Friendship>)base.getContext()).Where(x => (user.Id.Equals(x.requester) || user.Id.Equals(x.friend))).ToListAsync();
        }

    }
    }
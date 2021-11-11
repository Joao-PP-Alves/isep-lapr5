using DDDNetCore.Domain.Users;
using DDDNetCore.Infrastructure.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Domain.Services.DTO;
using Microsoft.EntityFrameworkCore;

namespace DDDNetCore.Infrastructure.Users {

    public class UserRepository : BaseRepository<User, UserId>, IUserRepository
    {
        public UserRepository(DDDNetCoreDbContext context) : base(context.Users)
        {
        }

     /*   public async Task<List<Friendship>> GetAllFriendshipsFromUser(UserId id)
            {
                return await ((DbSet<Friendship>)base.getContext()).Where()
            } */
                
        }
    }



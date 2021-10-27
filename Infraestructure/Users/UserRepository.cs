using DDDNetCore.Domain.Users;
using DDDNetCore.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace DDDNetCore.Infrastructure.Users {

    public class UserRepository : BaseRepository<User, UserId>
    {
        public UserRepository(DDDNetCoreDbContext context) : base(context.Users)
        {
            
        }
    }


}
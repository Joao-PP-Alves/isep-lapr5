using DDDNetCore.Domain.Users;
using DDDNetCore.Infrastructure.Shared;

namespace DDDNetCore.Infrastructure.Users {

    public class UserRepository : BaseRepository<User, UserId>, IUserRepository
    {
        public UserRepository(DDDNetCoreDbContext context) : base(context.Users)
        {
            
        }
    }


}
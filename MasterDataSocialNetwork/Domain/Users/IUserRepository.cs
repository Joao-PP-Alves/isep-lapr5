using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users{
    public interface IUserRepository : IRepository<User,UserId>{
        
    }
}
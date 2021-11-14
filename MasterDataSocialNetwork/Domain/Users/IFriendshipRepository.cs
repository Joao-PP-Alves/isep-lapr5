using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users{
    public interface IFriendshipRepository : IRepository<Friendship,FriendshipId>{
        Task<List<Friendship>> GetAllByUser(User current);
    }
}
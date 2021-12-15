using System.Threading.Tasks;
using System.Collections.Generic;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;
using System;

namespace DDDNetCore.Domain.Users{

    public interface IFriendshipService{

        Task createFriends(UserId requesterId, UserId friendId, string friendTag);

        Task<List<FriendshipWithFriendDto>> GetByUserIdWithFriend(UserId userId);

        Task<FriendshipDto> UpdateFriendshipConnectionStrength(Guid userId, Guid friendshipId, String connection_strength);

        Task<FriendshipDto> UpdateFriendshipTag(Guid userId, Guid friendshipId, string tag);











    }
}
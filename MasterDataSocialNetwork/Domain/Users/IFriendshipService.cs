using System.Threading.Tasks;
using System.Collections.Generic;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;
using System;

namespace DDDNetCore.Domain.Users{

    public interface IFriendshipService{
        public Task<List<FriendshipDto>> GetByUserId(UserId userId);
        public Task<Dictionary<int, List<UserDto>>> createMap(int level);
        public Task<FriendshipDto> ConvertToDto(Friendship friendship);
        public void UpdateFriendsList(FriendshipDto dto, Guid id);

        Task createFriends(UserId requesterId, UserId friendId, TagId friendTag);

        Task<List<FriendshipWithFriendDto>> GetByUserIdWithFriend(UserId userId);

        Task<FriendshipDto> UpdateFriendshipConnectionStrength(Guid userId, Guid friendshipId, String connection_strength);

        Task<FriendshipDto> UpdateFriendshipTag(Guid userId, Guid friendshipId, Guid tag);











    }
}
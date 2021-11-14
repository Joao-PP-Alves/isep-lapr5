using System.Threading.Tasks;
using System.Collections.Generic;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;
using System;

namespace DDDNetCore.Domain.Users{

    public interface IFriendshipService{
        public Task<List<FriendshipDto>> GetAllAsync();
        public Task<FriendshipDto> GetByIdAsync(FriendshipId id);
        public Task<FriendshipDto> AddAsync(CreatingFriendshipDto dto);
        public Task<FriendshipDto> UpdateAsync(FriendshipDto dto);
        public Task<FriendshipDto> InactivateAsync(FriendshipId id);
        public Task<FriendshipDto> DeleteAsync(FriendshipId id);
        public Task<List<FriendshipDto>> GetByUserId(UserId userId);
        public Task<Dictionary<int, List<UserDto>>> createMap(int level);
        public Task<FriendshipDto> ConvertToDto(Friendship friendship);
        public void UpdateFriendsList(FriendshipDto dto, Guid id);











    }
}
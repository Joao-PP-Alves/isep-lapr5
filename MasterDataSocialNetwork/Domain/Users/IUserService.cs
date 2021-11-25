using System.Threading.Tasks;
using System.Collections.Generic;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;
using System;
using DDDNetCore.Network;
using Microsoft.AspNetCore.Mvc;


namespace DDDNetCore.Domain.Users{
    public interface IUserService {

        public Task<List<UserDto>> GetAllAsync();
        public Task<UserDto> GetByIdAsync(UserId id);
        public Task<List<UserDto>> GetByEmail(string email);
        public Task<List<UserDto>> GetByName(string name);
        public Task<List<UserDto>> GetByTags(string tags);
        public Task<UserDto> AddAsync(CreatingUserDto dto);
        public Task<UserDto> UpdateProfileAsync(UserDto dto);
        public Task<UserDto> InactivateAsync(UserId id);
        public Task<UserDto> DeleteAsync(UserId id);
        public Task<UserDto> UpdateEmotionalStateAsync(UserDto dto);
        public Task<UserDto> ConvertToDto(User user);
        public Task<List<UserId>> GetFriendsSuggestionForNewUsers(UserId id);
        public Task<List<UserDto>> GetPossibleIntroductionTargets(UserId myId, UserId friendId);
        public Task<Network<UserDto, FriendshipDto>> GetMyFriends(UserId id, Network<UserDto, FriendshipDto> friendsNet, int level);
        Task<FriendshipDto> NewFriendship(CreatingFriendshipDto dto);
    }
}
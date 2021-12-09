using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;

namespace DDDNetCore.Domain.Users {
    public class FriendshipService : IFriendshipService {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _repoUser;

        public FriendshipService(IUnitOfWork unitOfWork, IUserRepository _repoUser)
        {
            this._unitOfWork = unitOfWork;
            this._repoUser = _repoUser;
        }

        public async Task<List<FriendshipDto>> GetByUserId(UserId userId)
        {
            List<FriendshipDto> friendships = new List<FriendshipDto>();
            friendships =  _repoUser.GetByIdAsync(userId).Result.friendsList.ConvertAll<FriendshipDto>(friendship =>
                new FriendshipDto(friendship.Id.AsGuid(), friendship.connection_strength, friendship.relationship_strength, friendship.friend, friendship.requester, friendship.friendshipTag));
            
            return friendships;
        }

        public async Task<List<FriendshipWithFriendDto>> GetByUserIdWithFriend(UserId userId)
        {
            List<FriendshipWithFriendDto> friendships = new List<FriendshipWithFriendDto>();
            friendships =  _repoUser.GetByIdAsync(userId).Result.friendsList.ConvertAll<FriendshipWithFriendDto>(friendship =>
                new FriendshipWithFriendDto(friendship.Id.AsGuid(), friendship.connection_strength, friendship.relationship_strength, friendship.friend, 
                friendship.requester, friendship.friendshipTag,_repoUser.GetByIdAsync(friendship.friend).Result));
            
            return friendships;
        }

        public async Task<Dictionary<int, List<UserDto>>> createMap(int level)
        {
            Dictionary<int, List<UserDto>> web = new Dictionary<int, List<UserDto>>();
            //inicializa o mapa, põe n keys (níveis)
            for (int i = 0; i < level; i++)
            {
                web[i] = new List<UserDto>();
            }

            return web;
        }

        public async Task<FriendshipDto> ConvertToDto(Friendship friendship)
        {
            return new FriendshipDto(friendship.Id.AsGuid(), friendship.connection_strength, friendship.relationship_strength,
                friendship.friend, friendship.requester, friendship.friendshipTag);
        }

        public async Task<FriendshipDto> UpdateFriendshipConnectionStrength(Guid userId, Guid friendshipId, String connection_strength){
            var friendship = _repoUser.GetFriendshipAsync(new UserId(userId), new FriendshipId(friendshipId));
            friendship.ChangeConnectionStrenght(new ConnectionStrength(connection_strength));

            await this._unitOfWork.CommitAsync();
            return new FriendshipDto(friendship.Id.AsGuid(),friendship.connection_strength,friendship.relationship_strength,friendship.friend,friendship.requester,friendship.friendshipTag);

        }

        public async Task<FriendshipDto> UpdateFriendshipTag(Guid userId, Guid friendshipId, Guid tag){
            var friendship = _repoUser.GetFriendshipAsync(new UserId(userId), new FriendshipId(friendshipId));
            friendship.ChangeFriendshipTag(new TagId(tag));

            await this._unitOfWork.CommitAsync();
            return new FriendshipDto(friendship.Id.AsGuid(),friendship.connection_strength,friendship.relationship_strength,friendship.friend,friendship.requester,friendship.friendshipTag);

        }

        public void UpdateFriendsList(FriendshipDto dto, Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task createFriends(UserId requesterId, UserId friendId, TagId friendTag){
            var requester = await _repoUser.GetByIdAsync(requesterId);
            var friend = await _repoUser.GetByIdAsync(friendId);

            Friendship friendship1 = new Friendship(friendId,requesterId,friendTag);
            Friendship friendship2 = new Friendship(requesterId,friendId,friendTag);

            requester.AddFriendship(friendship1);
            friend.AddFriendship(friendship2);

            await this._unitOfWork.CommitAsync();
        }
    }
}
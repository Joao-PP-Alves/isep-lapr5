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
        private readonly IFriendshipRepository _repo;
        private readonly IUserRepository _repoUser;

        public FriendshipService(IUnitOfWork unitOfWork, IFriendshipRepository repo, IUserRepository _repoUser)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._repoUser = _repoUser;
        }

        public async Task<List<FriendshipDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();
            
            List<FriendshipDto> listDto = list.ConvertAll<FriendshipDto>(f => new FriendshipDto(f.Id.AsGuid(), f.connection_strength, f.relationship_strength, f.friend, f.requester, f.friendshipTag));

            return listDto;
        }

        public async Task<FriendshipDto> GetByIdAsync(FriendshipId id)
        {
            var friendship = await this._repo.GetByIdAsync(id);
            
            if(friendship == null)
                return null;

            return new FriendshipDto(friendship.Id.AsGuid(), friendship.connection_strength, friendship.relationship_strength, friendship.friend, friendship.requester, friendship.friendshipTag);
        }

         public async Task<FriendshipDto> AddAsync(CreatingFriendshipDto dto)
        {
            
            var friendship = new Friendship(dto.friend, dto.requester, dto.connection_strength,dto.relationship_strength,dto.friendshipTag);
            
            await _repo.AddAsync(friendship);

            await _unitOfWork.CommitAsync();

            return new FriendshipDto(friendship.Id.AsGuid(), friendship.connection_strength, friendship.relationship_strength, dto.friend, dto.requester, friendship.friendshipTag);
        } 
        

        public async Task<FriendshipDto> UpdateAsync(FriendshipDto dto)
        {
            var friendship = await this._repo.GetByIdAsync(new FriendshipId(dto.Id)); 

            if (friendship == null){
                return null;   
            }

            friendship.ChangeConnectionStrenght(dto.connection_strength);
            friendship.ChangeFriendshipTag(dto.friendshipTag);
            
            await this._unitOfWork.CommitAsync();

            return new FriendshipDto(friendship.Id.AsGuid(), friendship.connection_strength, friendship.relationship_strength, friendship.friend, friendship.requester, friendship.friendshipTag);
        }

        public async Task<FriendshipDto> InactivateAsync(FriendshipId id)
        {
            var friendship = await this._repo.GetByIdAsync(id); 

            if (friendship == null)
                return null;   

            friendship.deactivate();
            
            await this._unitOfWork.CommitAsync();

            return new FriendshipDto(friendship.Id.AsGuid(), friendship.connection_strength, friendship.relationship_strength, friendship.friend, friendship.requester, friendship.friendshipTag);
        }

        public async Task<FriendshipDto> DeleteAsync(FriendshipId id)
        {
            var friendship = await this._repo.GetByIdAsync(id); 

            if (friendship == null)
                return null;   

            if (friendship.Active){
                throw new BusinessRuleValidationException("It is not possible to delete an active product.");
            }
            this._repo.Remove(friendship);
            await this._unitOfWork.CommitAsync();

            return new FriendshipDto(friendship.Id.AsGuid(), friendship.connection_strength, friendship.relationship_strength, friendship.friend, friendship.requester, friendship.friendshipTag);
        }

        public async Task<List<FriendshipDto>> GetByUserId(UserId userId)
        {
            List<FriendshipDto> friendships = new List<FriendshipDto>();
            friendships =  _repoUser.GetByIdAsync(userId).Result.friendsList.ConvertAll<FriendshipDto>(friendship =>
                new FriendshipDto(friendship.Id.AsGuid(), friendship.connection_strength, friendship.relationship_strength, friendship.friend, friendship.requester, friendship.friendshipTag));
            
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

        public void UpdateFriendsList(FriendshipDto dto, Guid id)
        {
            throw new NotImplementedException();
        }

        /* public async void UpdateFriendsList(FriendshipDto dto, Guid id)
        {
            var user = _repoUser.GetByIdAsync(new UserId(id)).Result;
            user.friendsList.Add(new Friendship(dto.friend, dto.requester, dto.connection_strength,dto.relationship_strength,dto.friendshipTag));
        } */
        
       // public async Task<Dictionary<int, List<UserDto>>> friendShipLevelMap(int level, Dictionary<int, >)
    }
}
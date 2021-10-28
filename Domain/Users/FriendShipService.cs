using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users {
    public class FriendshipService {
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
            
            List<FriendshipDto> listDto = list.ConvertAll<FriendshipDto>(f => new FriendshipDto(f.Id, f.connection_strenght, f.relationship_strenght, f.user1.Id, f.user2.Id, f.friendshipTag));

            return listDto;
        }

        public async Task<FriendshipDto> GetByIdAsync(FriendshipId id)
        {
            var friendship = await this._repo.GetByIdAsync(id);
            
            if(friendship == null)
                return null;

            return new FriendshipDto(friendship.Id, friendship.connection_strenght, friendship.relationship_strenght, friendship.user1.Id, friendship.user2.Id, friendship.friendshipTag);
        }

        public async Task<FriendshipDto> AddAsync(CreatingFriendshipDto dto)
        {
            var u1 = await _repoUser.GetByIdAsync(dto.user1);
            var u2 = await _repoUser.GetByIdAsync(dto.user2);
            
            var friendship = new Friendship(u1,u2,dto.connection_strenght,dto.relationship_strenght,dto.friendshipTag);

            await this._repo.AddAsync(friendship);

            await this._unitOfWork.CommitAsync();

            return new FriendshipDto(friendship.Id, friendship.connection_strenght, friendship.relationship_strenght, u1.Id, u2.Id, friendship.friendshipTag);
        }

        public async Task<FriendshipDto> UpdateAsync(FriendshipDto dto)
        {
            var friendship = await this._repo.GetByIdAsync(dto.Id); 

            if (friendship == null){
                return null;   
            }

            friendship.ChangeConnectionStrenght(dto.connection_strenght);
            friendship.ChangeFriendshipTag(dto.friendshipTag);
            
            await this._unitOfWork.CommitAsync();

            return new FriendshipDto(friendship.Id, friendship.connection_strenght, friendship.relationship_strenght, friendship.user1.Id, friendship.user2.Id, friendship.friendshipTag);
        }

        public async Task<FriendshipDto> InactivateAsync(FriendshipId id)
        {
            var friendship = await this._repo.GetByIdAsync(id); 

            if (friendship == null)
                return null;   

            friendship.deactivate();
            
            await this._unitOfWork.CommitAsync();

            return new FriendshipDto(friendship.Id, friendship.connection_strenght, friendship.relationship_strenght, friendship.user1.Id, friendship.user2.Id, friendship.friendshipTag);
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

            return new FriendshipDto(friendship.Id, friendship.connection_strenght, friendship.relationship_strenght, friendship.user1.Id, friendship.user2.Id, friendship.friendshipTag);
        }

        private async Task checkUserIdAsync(UserId userId)
        {
           var category = await _repoUser.GetByIdAsync(userId);
           if (category == null)
                throw new BusinessRuleValidationException("Invalid Category Id.");
        }
    }
}
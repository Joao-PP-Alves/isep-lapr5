using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;
using System.Linq;

namespace DDDNetCore.Domain.Users
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _repo;

        private readonly FriendshipService _serviceFrienships;

        public UserService(IUnitOfWork unitOfWork, IUserRepository repo, FriendshipService serviceFriendships)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._serviceFrienships = serviceFriendships;
        }

        public async Task<List<UserDto>> GetMyFriends(UserId id){
            var list = await this._serviceFrienships.GetByUserId(id);
            var returnableList = new List<UserDto>();
            foreach (var dto in list)
            {
                var user = await this._repo.GetByIdAsync(dto.friend);
                returnableList.Add(new UserDto(user.Id.AsGuid(),user.Name,user.Email,user.PhoneNumber,user.tags,user.emotionalState,user.EmotionTime,user.LastEmotionalChange));
            }

            return returnableList; 
        }

        public async Task<List<UserDto>> GetPossibleIntroductionTargets(UserId myId, UserId friendId){
            var myUserProfile = await this.GetByIdAsync(myId);
            var myFriends = await this.GetMyFriends(myId);
            var friendFriends = await this.GetMyFriends(friendId);
            var myIds = new List<Guid>();
            var friendIds = new List<Guid>();
            foreach (var dto in myFriends)
            {
                if (!dto.Id.Equals(friendId.AsGuid())){
                    myIds.Add(dto.Id);
                }
            }
            foreach (var dto in friendFriends)
            {
                if (!dto.Id.Equals(myId.AsGuid())){
                    friendIds.Add(dto.Id);
                }
            }
            var finalList = new List<UserDto>();
            var finalIdsList = friendIds.Except(myIds);
            foreach (var dto in friendFriends)
            {
                if (finalIdsList.Contains(dto.Id)){
                    finalList.Add(dto);
                }
            }
            return finalList;
        }
        public async Task<List<UserDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();

            foreach (var notDto in list)
            {
                notDto.updateEmotionTime();
            }

            List<UserDto> listDto = list.ConvertAll<UserDto>(user =>
                new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.PhoneNumber, user.tags, user.emotionalState, user.EmotionTime,user.LastEmotionalChange));

            
            return listDto;
        } 

        public async Task<UserDto> GetByIdAsync(UserId id)
        {
            var user = await this._repo.GetByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            user.updateEmotionTime();
            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.PhoneNumber, user.tags, user.emotionalState, user.EmotionTime,user.LastEmotionalChange);
        }

        public async Task<UserDto> AddAsync(CreatingUserDto dto)
        {
            var user = new User(dto.name, dto.email, dto.password, dto.phoneNumber, dto.tags, dto.emotionalState);
            await this._repo.AddAsync(user);
            await this._unitOfWork.CommitAsync();
            user.updateEmotionTime();
            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.PhoneNumber, user.tags, user.emotionalState, user.EmotionTime,user.LastEmotionalChange);
        }

        /**
        * método para dar atualização no perfil
        **/
        public async Task<UserDto> UpdateProfileAsync(UserDto dto)
        {
            var user = await this._repo.GetByIdAsync(new UserId(dto.Id));

            if (user == null)
            {
                return null;
            }

            //change all field
            user.ChangeName(dto.name);
            user.ChangeTags(dto.tags);
            user.ChangePhoneNumber(dto.phoneNumber);
            user.ChangeEmotionalState(dto.emotionalState);
            user.ChangeEmail(dto.email);
            user.updateEmotionTime();
            await this._unitOfWork.CommitAsync();
            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.PhoneNumber, user.tags, user.emotionalState, user.EmotionTime,user.LastEmotionalChange);
        }

        public async Task<UserDto> InactivateAsync(UserId id)
        {
            var user = await this._repo.GetByIdAsync(id); 

            if (user == null)
                return null;   

            user.MarkAsInative();
            
            await this._unitOfWork.CommitAsync();

            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.PhoneNumber, user.tags, user.emotionalState, user.EmotionTime,user.LastEmotionalChange);
        }

        public async Task<UserDto> DeleteAsync(UserId id)
        {
            var user = await this._repo.GetByIdAsync(id);

            if (user == null)
                return null;

            // Mark as inactive. An exception may be thrown in the User class
            user.MarkAsInative();

            this._repo.Remove(user);
            await this._unitOfWork.CommitAsync();

            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.PhoneNumber, user.tags, user.emotionalState, user.EmotionTime,user.LastEmotionalChange);
        }

         public async Task<UserDto> UpdateEmotionalStateAsync(UserDto dto)
        {
            var user = await this._repo.GetByIdAsync(new UserId(dto.Id));

            if (user == null)
            {
                return null;
            }

            //change all field
            user.ChangeEmotionalState(dto.emotionalState);
            user.updateEmotionTime();
            await this._unitOfWork.CommitAsync();
            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.PhoneNumber, user.tags, user.emotionalState, user.EmotionTime,user.LastEmotionalChange);
        }

         // public async Task<List<UserDto>> friendsSuggestion(UserDto dto)
         // {
         //     // var user = await this._repo.GetByIdAsync(new UserId(dto.Id));
         //     //
         //     // if (user == null)
         //     // {
         //     //     return null;
         //     // }
         // }
    }
}

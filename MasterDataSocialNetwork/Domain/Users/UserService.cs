using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;
using System.Linq;
using DDDNetCore.Network;

namespace DDDNetCore.Domain.Users
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _repo;

        private readonly FriendshipService _serviceFriendships;

        public UserService(IUnitOfWork unitOfWork, IUserRepository repo, FriendshipService serviceFriendships)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._serviceFriendships = serviceFriendships;
        }

        public async Task<Network<UserDto, FriendshipDto>> GetMyFriends(UserId id, Network<UserDto, FriendshipDto> friendsNet, int level)
        {
           /* // Base case
            if (current == level)
            {
                // Convert all users to UserDTO and FriendShips To friendShipsDTO
                return null;
            }
            // Starts by inserting the central user vertex
            if (current == 0)
            {
                friendsNet.InsertVertex(this._repo.GetByIdAsync(id).Result);
            } */

           // Starts by inserting the central user vertex
           friendsNet.InsertVertex(await ConvertToDto(this._repo.GetByIdAsync(id).Result));

           for (int i = 0; i < level; i++)
           {
               // Goes trough all users in the network 
               foreach (var user in friendsNet.Vertices())
               {
                   // Gets their friends
                   var friends = user.friendsList;
                   foreach (var user_friendship in friends)
                   {
                       // Adds each of the friends to the network
                        
                       friendsNet.InsertVertex(await ConvertToDto(user_friendship.friend));
                       friendsNet.InsertEdge(await ConvertToDto(user_friendship.friend), user, await _serviceFriendships.ConvertToDto(user_friendship), 0);
                   }
               }
           }

           // Recursive call to add friends of friends
           /*current++;
           GetMyFriends(id, friendsNet, level, current); */

           return friendsNet;
        }

        public async Task<List<UserDto>> GetPossibleIntroductionTargets(UserId myId, UserId friendId)
        {
            var myUserProfile = await this.GetByIdAsync(myId);
            // Estas linhas têm de ser corrigidas
    //        var myFriends = await this.GetMyFriends(myId,new Network<User, Friendship>(false), 1, 0);
    //        var friendFriends = await this.GetMyFriends(friendId, new Network<User, Friendship>(false), 1, 0);
            // var myIds = new List<Guid>();
            // var friendIds = new List<Guid>();
            // foreach (var dto in myFriends)
            // {
            //     if (!dto.Id.Equals(friendId.AsGuid()))
            //     {
            //         myIds.Add(dto.Id);
            //     }
            // }
            //
            // foreach (var dto in friendFriends)
            // {
            //     if (!dto.Id.Equals(myId.AsGuid()))
            //     {
            //         friendIds.Add(dto.Id);
            //     }
            // }
            //
            // var finalList = new List<UserDto>();
            // var finalIdsList = friendIds.Except(myIds);
            // foreach (var dto in friendFriends)
            // {
            //     if (finalIdsList.Contains(dto.Id))
            //     {
            //         finalList.Add(dto);
            //     }
            // }

            //return finalList;
            return null;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();

            foreach (var notDto in list)
            {
               //notDto.updateEmotionTime(new EmotionTime(notDto.EmotionTime.LastEmotionalUpdate));
            }

            List<UserDto> listDto = list.ConvertAll<UserDto>(user =>
                new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.PhoneNumber, user.tags, user.emotionalState/*,
                    user.EmotionTime*/));


            return listDto;
        }

        public async Task<UserDto> GetByIdAsync(UserId id)
        {
            var user = await this._repo.GetByIdAsync(id);

            if (user == null)
            {
                return null;
            }

          //  user.updateEmotionTime(new EmotionTime(user.EmotionTime.LastEmotionalUpdate));
            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.PhoneNumber, user.tags,
                user.emotionalState /*, user.EmotionTime*/);
        }

        public async Task<UserDto> AddAsync(CreatingUserDto dto)
        {
            var user = new User(dto.name, dto.email, dto.password, dto.phoneNumber, dto.tags, dto.emotionalState
                /*dto.EmotionTime*/);
            await this._repo.AddAsync(user);
            await this._unitOfWork.CommitAsync();
          //  user.updateEmotionTime(new EmotionTime(user.EmotionTime.LastEmotionalUpdate));
            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.PhoneNumber, user.tags,
                user.emotionalState/*, user.EmotionTime*/);
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
       //     user.updateEmotionTime(new EmotionTime(user.EmotionTime.LastEmotionalUpdate));
            await this._unitOfWork.CommitAsync();
            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.PhoneNumber, user.tags,
                user.emotionalState/*, user.EmotionTime*/);
        }

        public async Task<UserDto> InactivateAsync(UserId id)
        {
            var user = await this._repo.GetByIdAsync(id);

            if (user == null)
                return null;

            user.MarkAsInative();

            await this._unitOfWork.CommitAsync();

            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.PhoneNumber, user.tags,
                user.emotionalState/*, user.EmotionTime*/);
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

            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.PhoneNumber, user.tags,
                user.emotionalState /*, user.EmotionTime*/);
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
            //user.updateEmotionTime(new EmotionTime(user.EmotionTime.LastEmotionalUpdate));
            await this._unitOfWork.CommitAsync();
            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.PhoneNumber, user.tags,
                user.emotionalState/*, user.EmotionTime*/);
        }

        public async Task<UserDto> ConvertToDto(User user)
        {
            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.friendsList, user.PhoneNumber, user.tags,
                user.emotionalState/*, user.EmotionTime*/);
        }

        public async Task<List<UserId>> GetFriendsSuggestionForNewUsers(UserId id)
        {
            var user = await this._repo.GetByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            var friends = this._repo.friendsSuggestion(user.Id);
            return friends;
        }
    }
}
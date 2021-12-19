using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain.Services;
using DDDNetCore.Domain.Services.CreatingDTO;
using DDDNetCore.Domain.Services.DTO;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Network;
using System.Text;
using Microsoft.AspNetCore.Http;
using Flurl.Http;
using System.Drawing;
using System.Linq;

namespace DDDNetCore.Domain.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _repo;
        private readonly IFriendshipService _friendshipService;
        public UserService(IUnitOfWork unitOfWork, IUserRepository repo, IFriendshipService friendshipService)
        {
            _unitOfWork = unitOfWork;
            _repo = repo;
            _friendshipService = friendshipService;
        }

        public async Task<UserLoginDTO> Login(LoginDTO dto)
        {
            
            var user = await _repo.checkCredentials(dto.email.EmailAddress, dto.password.Value);

            if (user == null)
            {
                user = await _repo.GetByEmail(dto.email.EmailAddress);
                if (user != null)
                {
                    // Email is correct, password is wrong
                    return null;
                }

                // User does not exist
                throw new Exception();
            }
            
            return new UserLoginDTO(user.Id.AsGuid(), user.Name, user.Email, user.PhoneNumber,
                user.BirthDate, user.tags);
        }


        public async Task<Network<UserDto, FriendshipDto>> GetMyFriends(UserId id,
            Network<UserDto, FriendshipDto> friendsNet, int level)
        {
            friendsNet = new Network<UserDto, FriendshipDto>(false);

            // Starts by inserting the central user vertex
            var current = _repo.GetByIdAsync(id).Result;
            var dto = await ConvertToDto(current);
            friendsNet.InsertVertex(dto);
            List<Friendship> friendships = new List<Friendship>();

            for (int i = 0; i < level; i++)
            {
                // Goes trough all users in the network 
                foreach (var user in friendsNet.Vertices())
                {
                    // Gets their friends
                    //friendships = await 


                    //var friends = user.friendsList;
                    foreach (var user_friendship in friendships)
                    {
                        // Adds each of the friends to the network
                        current = _repo.GetByIdAsync(id).Result;
                        friendsNet.InsertVertex(await ConvertToDto(current));
                        //friendsNet.InsertEdge(await ConvertToDto(_repo.GetByIdAsync(user_friendship.friend).Result), user, await _serviceFriendships.ConvertToDto(user_friendship), 0);
                    }
                }
            }

            return friendsNet;
        }

        public async Task<List<UserDto>> GetPossibleIntroductionTargets(UserId myId, UserId friendId)
        {
            var myUserProfile = await GetByIdAsync(myId);
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
            var list = await _repo.GetAllAsync();

            foreach (var notDto in list)
            {
                notDto.updateEmotionTime(new EmotionTime(notDto.EmotionTime.LastEmotionalUpdate));
            }

            var listDto = list.ConvertAll(user =>
                new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.friendsList, user.PhoneNumber, user.BirthDate,
                    user.tags,
                    user.emotionalState,
                    user.EmotionTime));

            return listDto;
        }

        public async Task<UserDto> GetByIdAsync(UserId id)
        {
            var user = await _repo.GetByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            user.updateEmotionTime(new EmotionTime(user.EmotionTime.LastEmotionalUpdate));
            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.friendsList, user.PhoneNumber,
                user.BirthDate, user.tags,
                user.emotionalState, user.EmotionTime);
        }

        public async Task<UserDto> GetByEmail(string email)
        {
            try
            {
                var emailConfirmation = new Email(email);
            }
            catch (BusinessRuleValidationException)
            {
                throw new Exception("The provided email is invalid");
            }

            var user = await _repo.GetByEmail(email);

            if (user == null)
            {
                return null;
            }

            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.friendsList, user.PhoneNumber,
                user.BirthDate,
                user.tags,
                user.emotionalState,
                user.EmotionTime);
        }

        public async Task<List<UserDto>> GetByName(string name)
        {
            try
            {
                var nameConfirmation = new Name(name);
            }
            catch (BusinessRuleValidationException b)
            {
                throw new Exception("The provided name is invalid");
            }

            var list = await _repo.GetByName(name);

            if (list == null)
            {
                return null;
            }

            foreach (var user in list)
            {
                user.updateEmotionTime(new EmotionTime(user.EmotionTime.LastEmotionalUpdate));
            }

            List<UserDto> listDto = list.ConvertAll(user =>
                new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.friendsList, user.PhoneNumber, user.BirthDate,
                    user.tags,
                    user.emotionalState,
                    user.EmotionTime));

            return listDto;
        }

        public async Task<List<UserDto>> GetByTags(string tags)
        {
            string[] listStrings = tags.Split("&");
            List<Tag> listTags = new List<Tag>();
            try
            {
                foreach (string item in listStrings)
                {
                    Tag tagConfirmation = new Tag(item);
                    listTags.Add(tagConfirmation);
                }
            }
            catch (BusinessRuleValidationException b)
            {
                throw new Exception("One or more tags are invalid!");
            }

            var list = await _repo.GetByTags(listTags);

            if (list == null)
            {
                return null;
            }

            foreach (var user in list)
            {
                user.updateEmotionTime(new EmotionTime(user.EmotionTime.LastEmotionalUpdate));
            }

            List<UserDto> listDto = list.ConvertAll(user =>
                new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.friendsList, user.PhoneNumber, user.BirthDate,
                    user.tags,
                    user.emotionalState,
                    user.EmotionTime));

            return listDto;
        }

        public async Task<UserDto> AddAsync(CreatingUserDto dto)
        {
            var user = new User(dto.name, dto.email, dto.password, dto.phoneNumber, dto.birthDate, dto.tags);
            await _repo.AddAsync(user);
            await _unitOfWork.CommitAsync();
            user.updateEmotionTime(new EmotionTime(user.EmotionTime.LastEmotionalUpdate));
            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.friendsList, user.PhoneNumber,
                user.BirthDate, user.tags,
                user.emotionalState, user.EmotionTime);
        }

        /**
        * método para dar atualização no perfil
        **/
        public async Task<UserDto> UpdateProfileAsync(UpdateUserDto dto)
        {
            var user = await _repo.GetByIdAsync(new UserId(dto.Id));

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
            user.updateEmotionTime(new EmotionTime(user.EmotionTime.LastEmotionalUpdate));
            await _unitOfWork.CommitAsync();
            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.friendsList, user.PhoneNumber,
                user.BirthDate, user.tags,
                user.emotionalState, user.EmotionTime);
        }

        /// <summary>
        /// Marks a user as inactive to the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDto> InactivateAsync(UserId id)
        {
            var user = await _repo.GetByIdAsync(id);

            if (user == null)
                return null;

            user.MarkAsInative();

            await _unitOfWork.CommitAsync();

            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.friendsList, user.PhoneNumber,
                user.BirthDate, user.tags,
                user.emotionalState, user.EmotionTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDto> DeleteAsync(UserId id)
        {
            var user = await _repo.GetByIdAsync(id);

            if (user == null)
                return null;

            // Mark as inactive. An exception may be thrown in the User class
            user.MarkAsInative();

            _repo.Remove(user);
            await _unitOfWork.CommitAsync();

            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.friendsList, user.PhoneNumber,
                user.BirthDate, user.tags,
                user.emotionalState, user.EmotionTime);
        }

        /// <summary>
        /// Updates the emotional state of an user
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<UserDto> UpdateEmotionalStateAsync(UserDto dto)
        {
            var user = await _repo.GetByIdAsync(new UserId(dto.Id));

            if (user == null)
            {
                return null;
            }

            //change all field
            user.ChangeEmotionalState(dto.emotionalState);
            user.updateEmotionTime(new EmotionTime(user.EmotionTime.LastEmotionalUpdate));
            await _unitOfWork.CommitAsync();
            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.friendsList, user.PhoneNumber,
                user.BirthDate, user.tags,
                user.emotionalState, user.EmotionTime);
        }

        /// <summary>
        /// Converts a user to a dto, for data policy purposes
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<UserDto> ConvertToDto(User user)
        {
            return new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.friendsList, user.PhoneNumber,
                user.BirthDate, user.tags,
                user.emotionalState, user.EmotionTime);
        }

        public async Task<List<UserDto>> GetFriendsSuggestionForNewUsers(UserId id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            var friends = _repo.ReturnFriendsSuggestionList(user.Id);
            List<UserDto> listDto = friends.ConvertAll(user =>
                new UserDto(user.Id.AsGuid(), user.Name, user.Email, user.friendsList, user.PhoneNumber, user.BirthDate,
                    user.tags,
                    user.emotionalState,
                    user.EmotionTime)); ;

            return listDto;
        }

        /// <summary>
        /// Creates a new friendship between two friends
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<FriendshipDto> NewFriendship(CreatingFriendshipDto dto)
        {
            // Checks if both users exist
            await checkUserIdAsync(dto.friend);
            await checkUserIdAsync(dto.requester);

            // Returns them from the database
            var friend = await (_repo.GetByIdAsync(dto.friend));
            var requester = await _repo.GetByIdAsync(dto.requester);

            // Creates a new friendship between them
            var friendship = new Friendship(friend.Id, requester.Id, dto.connection_strength, dto.relationship_strength,
                dto.friendshipTag);

            // Adds the friendship to the databse
            await _repo.NewFriendship(new FriendshipDto(friendship.Id.AsGuid(), dto.connection_strength,
                dto.relationship_strength, dto.friend, dto.requester, dto.friendshipTag));

            // Returns a dto
            return new FriendshipDto(friendship.Id.AsGuid(), friendship.connection_strength,
                friendship.relationship_strength, friend.Id, requester.Id, friendship.friendshipTag);
        }

        public async Task<List<UserPerspectiveDto>> MyPerspective(UserId userId, int param)
        {
            List<UserPerspectiveDto> toReturn = new List<UserPerspectiveDto>();
            var user = await _repo.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("The provider user does not exist");
            }
            else
            {
                toReturn.Add(new UserPerspectiveDto(user.Id.Value, user.Name.text, null, null, null));
                //toReturn.Add(new UserPerspectiveDto(user.Id.Value, user.Name.text, null, "1", "1"));
                await transforma(param, new List<UserPerspectiveDto>(), toReturn,user.Id.ToString());
                return toReturn;
            }
        }

        private async Task<List<UserPerspectiveDto>> transforma(int param, List<UserPerspectiveDto> auxList,
            List<UserPerspectiveDto> toReturn, String rootId)
        {
            if (param == 0)
            {
                return toReturn;
            }
            else
            {
                foreach (var user in toReturn)
                {
                    var friendsList = await _friendshipService.GetByUserIdWithFriend(new UserId(user.userId));
                        foreach (var friend in friendsList)
                        {
                            var newUser = new UserPerspectiveDto(friend.friend.Value, friend.friendObject.Name.text,
                                user.userId, friend.connection_strength.value,
                                friend.relationship_strength.value);
                            auxList.Add(newUser);
                        }
                }
                foreach (var user in auxList){
                    var counter = 0;
                    foreach (var item in toReturn)
                    {
                        if ((user.userId == item.userId && user.parentId == item.parentId) || (user.userId == item.parentId && user.parentId == item.userId) || (user.userId == rootId)){
                            counter++;
                        }
                    }
                    if (counter == 0){
                        toReturn.Add(user);
                    }
                }
                await transforma(param - 1, new List<UserPerspectiveDto>(), toReturn, rootId);
            }
            return null;
        }

        /// <summary>
        /// Checks if a user exists
        /// </summary>
        /// <param name="userId"></param>
        /// <exception cref="Exception"></exception>
        private async Task checkUserIdAsync(UserId userId)
        {
            var user = await _repo.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("The provided user does not exist");
        }

        public async Task<bool> checkIfTwoUsersAreFriends(UserId user1, UserId user2)
        {
            if (_repo.checkIfFriends(user1, user2))
            {
                return true;
            }
            return false;
        }

        public Task<List<Tag>> checkToAddTag(List<string> tags)
        {
            foreach (var tag in tags)
            {
                
            }

            return null;
        }
        public async Task<NSizeResponseDTO> GetNetworkSize(UserId userId, int level){

            var user = await GetByIdAsync(userId);

            var dto = new NetworkNSizeDTO(user.email.EmailAddress,level);

            if (dto.N > 3 || dto.N < 0){
                throw new BusinessRuleValidationException("Level must be between 0 and 3.");
            }

            var address = new StringBuilder(Constants.URL_Prolog);
            address.Append("/networksize?");
            address.Append("user=").Append(dto.UserEmail);
            address.Append("&level=").Append(dto.N);
            var response = await address.ToString().GetStringAsync();
            var array = response.ToCharArray(0,response.Length);
            address.Clear();
            foreach (var chare in array)
            {
                if (chare >= 48 && chare <= 57){
                    address.Append(chare);
                }
            }
            NSizeResponseDTO dtoR = new NSizeResponseDTO(Int32.Parse(address.ToString()));
            
            return dtoR;
        }

        /* Para já, este método está a retornar o NetworkSize -1. Alterar no futuro.*/
        public async Task<NSizeResponseDTO> GetNetworkDimensionSize(UserId userId, int level){
            
             if (level > 2 ||level < 0){
                throw new BusinessRuleValidationException("Level must be between 0 and 2.");
             }
             var responseDto = await this.GetNetworkSize(userId, level);
           
             if (responseDto.Size > 0)
                 responseDto.Size = responseDto.Size -1;

             return responseDto;
        }
        
        public async Task<List<LeaderboardUserNetworkSizeDto>> GetLeaderBoardNetworkSize(int N){
            var listUsers = _repo.GetAllAsync().Result;

            List<LeaderboardUserNetworkSizeDto> listDto = new List<LeaderboardUserNetworkSizeDto>();

            foreach(User user in listUsers){
                var networkSizeUser = GetNetworkSize(user.Id,N).Result;
                listDto.Add(new LeaderboardUserNetworkSizeDto(user.Name.text,networkSizeUser.Size));
            }

            List<LeaderboardUserNetworkSizeDto> listDtoOrdered = listDto.OrderBy(o => o.Size).ToList();

            return listDtoOrdered;
        }

        public async Task<List<TagCloudDto>> GetMyTagCloud(UserId id)
        {
            var tagsList = _repo.GetMyTagList(id).Result;
            var sortedList = _repo.GetSortedTagsList(tagsList).Result;
            return sortedList.Select(tag => new TagCloudDto(tag.name, 1)).ToList();
        }
        
        public async Task<List<TagCloudDto>> GetAllUsersTagCloud()
        {
            var tagsList = _repo.GetAllUsersTags().Result;
            var sortedList = _repo.GetSortedTagsList(tagsList).Result;
            return GetTagCloudDtoList(sortedList);
        }

        public async Task<List<TagCloudDto>> GetAllFriendshipsTagCloud()
        {
            var friendships = _repo.GetAllFriendships().Result;
            var tagsList = _repo.GetAllFriendshipTags(friendships).Result;
            var sortedList = _repo.GetSortedTagsList(tagsList).Result;
            return GetTagCloudDtoList(sortedList);
        }

        public async Task<List<TagCloudDto>> GetMyFriendshipsTagCloud(UserId id)
        {
            var friendships = _repo.GetMyFriendships(id).Result;
            var tagsList = _repo.GetAllFriendshipTags(friendships).Result;
            var sortedList = _repo.GetSortedTagsList(tagsList).Result;
            return GetTagCloudDtoList(sortedList);
        }

        public List<TagCloudDto> GetTagCloudDtoList(List<Tag> sortedList)
        {
            var auxDictionary = new Dictionary<string, int>();
            foreach (var tag in sortedList)
            {
                if (!auxDictionary.ContainsKey(tag.name))
                {
                    auxDictionary.Add(tag.name, 1);
                }
                else
                {
                    auxDictionary[tag.name] += 1;
                }
            }
            return auxDictionary.Select(newTag => new TagCloudDto(newTag.Key, newTag.Value)).ToList();
        }
        
        public async Task<List<String>> GetShortestPath(UserId userId1, UserId userId2){
            var list = new List<String>();
            var user1 = await GetByIdAsync(userId1);
            var user2 = await GetByIdAsync(userId2);

            var orig = user1.email.EmailAddress;
            var dest = user2.email.EmailAddress;

            var address = new StringBuilder(Constants.URL_Prolog);
            address.Append("/shortpath?");
            address.Append("orig=").Append(orig);
            address.Append("&dest=").Append(dest);

            var response = await address.ToString().GetStringAsync();
            var array = response.Split("\"");
            foreach (var item in array)
            {
                if (item.Contains("@")){
                    var user = await this.GetByEmail(item);
                    list.Add(user.Id.ToString());
                }
            }
            address.Clear();
            return list;
        }

        public async Task<List<String>> GetStrongestPath(UserId userId1, UserId userId2){
            var list = new List<String>();
            var user1 = await GetByIdAsync(userId1);
            var user2 = await GetByIdAsync(userId2);

            var orig = user1.email.EmailAddress;
            var dest = user2.email.EmailAddress;

            var address = new StringBuilder(Constants.URL_Prolog);
            address.Append("/strongpath?");
            address.Append("orig=").Append(orig);
            address.Append("&dest=").Append(dest);

            var response = await address.ToString().GetStringAsync();
            var array = response.Split("\"");
            foreach (var item in array)
            {
                if (item.Contains("@")){
                    var user = await this.GetByEmail(item);
                    list.Add(user.Id.ToString());
                }
            }
            address.Clear();
            return list;
        }

        public async Task<List<String>> GetSafestPath(UserId userId1, UserId userId2, int minimum){
            var list = new List<String>();
            var user1 = await GetByIdAsync(userId1);
            var user2 = await GetByIdAsync(userId2);

            var orig = user1.email.EmailAddress;
            var dest = user2.email.EmailAddress;

            var address = new StringBuilder(Constants.URL_Prolog);
            address.Append("/safepath?");
            address.Append("orig=").Append(orig);
            address.Append("&dest=").Append(dest);
            address.Append("&min=").Append(minimum);

            var response = await address.ToString().GetStringAsync();
            var array = response.Split("\"");
            foreach (var item in array)
            {
                if (item.Contains("@")){
                    var user = await this.GetByEmail(item);
                    list.Add(user.Id.ToString());
                }
            }
            address.Clear();
            return list;
        }
    }
}
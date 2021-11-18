using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Domain.Services.DTO;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;
using DDDNetCore.Infrastructure.Shared;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DDDNetCore.Infrastructure.Users
{

    public class UserRepository : BaseRepository<User, UserId>, IUserRepository
    {
        private readonly DDDNetCoreDbContext _context;

        public UserRepository(DDDNetCoreDbContext context) : base(context.Users)
        {
            _context = context;
            
        }

        public async Task<List<Tag>> GetTagList(UserId id)
        {
            return _context.Users.Find(id).tags;
        }

        public async Task<List<User>> GetUsersWithTheirTags()
       {
           return _context.Users.Include(u => u.tags).ToList();
       }

        /// <summary>
        /// From a new userId registered, his tags are compared with other user's tags
        /// to get the first one some friends suggestions
        /// TODO: FALTA MUDAR DE USERID PARA DTO
        /// </summary>
        /// <param name="userId"></param>
        /// <returns> UserId list </returns>
        public List<UserId> ReturnFriendsSuggestionList(UserId userId)
        {
            List<UserId> friendsList = new List<UserId>();
            var tag = GetTagList(userId).Result;
            var possibleFriends = GetUsersWithTheirTags().Result;
            foreach (User u in possibleFriends)
            {
                foreach (Tag ut in tag)
                {
                    foreach (Tag usertag in u.tags)
                    {
                        if (ut.name.Equals(usertag.name) && !(friendsList.Contains(u.Id)))
                        {
                            friendsList.Add(u.Id);
                        }
                    }
                }
            }
            return friendsList;
        }
        
        public Boolean checkIfFriends(UserId id, UserId id2){
            var friendships = _context.Users.Find(id).friendsList;
            if (friendships == null){
                return false;
            }
            
            return friendships.Any(friendship => friendship.friend == id2);
        }

        public Boolean checkIfNotFriends(UserId id, UserId id2){
            return (!checkIfFriends(id,id2));
        }

        public async Task<List<User>> GetByEmail(string email){
            return await ((DbSet<User>)base.getContext()).Where(x => email.Equals(x.Email.EmailAddress)).ToListAsync();
        }

        public async Task<List<User>> GetByName(string name){
            return await ((DbSet<User>)base.getContext()).Where(x => name.Equals(x.Name.text)).ToListAsync();
        }

        public async void NewFriendship(FriendshipDto dto)
        { 
            //var friend  = GetByIdAsync(new UserId(dto.friend.AsGuid())).Result;
            var requester = GetByIdAsync(new UserId(dto.requester.AsString())).Result;
            var friend = _context.Users.Include(f => f.friendsList).FirstOrDefault(u => u.Id == dto.friend);
            if (friend == null || requester == null)
            {
                throw new Exception("Invalid User Id.");
            }
            
            /**
             * TODO (próximo sprint): relationship strength é calculada, connection strength + tag pode ser diferente
             **/

            requester.AddFriendship(new Friendship(dto.friend, dto.requester, dto.connection_strength,dto.relationship_strength,dto.friendshipTag));
            friend.AddFriendship(new Friendship(dto.requester, dto.friend, dto.connection_strength, dto.relationship_strength, dto.friendshipTag));

            await _context.SaveChangesAsync();
        }

        public new async Task<List<User>> GetAllAsync()
        {
            return _context.Users.Include(f => f.friendsList).ToList();
            
        }

        public Task<User> GetByIdAsync(UserId id)
        {
            return _context.Users.Include(f => f.friendsList).Where(user => user.Id == id).FirstOrDefaultAsync();
        }
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Domain.Services.DTO;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Tags;
using DDDNetCore.Domain.Users;
using DDDNetCore.Infrastructure.Shared;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DDDNetCore.Infrastructure.Users
{

    public class UserRepository : BaseRepository<User, UserId>, IUserRepository
    {
        private readonly DddNetCoreDbContext _context;

        public UserRepository(DddNetCoreDbContext context) : base(context.Users)
        {
            _context = context;
            
        }

        public async Task<ICollection<Tag>> GetTagList(UserId id)
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
        public List<User> ReturnFriendsSuggestionList(UserId userId)
        {
            List<User> friendsList = new List<User>();
            var tag = GetTagList(userId).Result;
            var possibleFriends = GetUsersWithTheirTags().Result;
            foreach (User u in possibleFriends)
            {
                if (u.Id != userId)
                {
                    foreach (Tag ut in tag)
                    {
                        foreach (Tag usertag in u.tags)
                        {
                            if (ut.name.Equals(usertag.name) && !(friendsList.Contains(u)))
                            {
                                friendsList.Add(u);
                            }
                        }
                    }
                }
            }
            return friendsList;
        }
        
        public Boolean checkIfFriends(UserId id, UserId id2){
            var friendships = _context.Users.Where(x => id.Equals(x.Id)).FirstOrDefaultAsync().Result.friendsList;
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
        
        public async Task<List<User>> GetByTags(List<Tag> list){
            List<User> listUsers = new List<User>();
            var listPossibleUsers = GetUsersWithTheirTags().Result;
               
                foreach (var itemThis in listPossibleUsers)
                {
                    var c = list.Intersect(itemThis.tags);
                    if(c.Count() > 0){
                        listUsers.Add(itemThis);
                    }
                }
             
            return listUsers;
        }

        public async Task<int> NewFriendship(FriendshipDto dto)
        { 
            //var friend  = GetByIdAsync(new UserId(dto.friend.AsGuid())).Result;
            var requester = GetByIdAsync(new UserId(dto.requester.AsString())).Result;
            var friend = _context.Users.Include(f => f.friendsList).Include(b=>b.BirthDate).FirstOrDefault(u => u.Id == dto.friend);
            if (friend == null || requester == null)
            {
                throw new Exception("Invalid User Id.");
            }
            
            /**
             * TODO (próximo sprint): relationship strength é calculada, connection strength + tag pode ser diferente
             **/

            requester.AddFriendship(new Friendship(dto.friend, dto.requester, dto.connection_strength,dto.relationship_strength,dto.friendshipTag));
            friend.AddFriendship(new Friendship(dto.requester, dto.friend, dto.connection_strength, dto.relationship_strength, dto.friendshipTag));

            return await _context.SaveChangesAsync();
        }

        public new async Task<List<User>> GetAllAsync()
        {
            return _context.Users.Include(b=>b.BirthDate).Include(f => f.friendsList).ToList();
            
        }

        public Task<User> GetByIdAsync(UserId id)
        {
            return _context.Users.Include(f => f.friendsList).Include(b=>b.BirthDate).Where(user => user.Id == id).FirstOrDefaultAsync();
        }

        public Friendship GetFriendshipAsync(UserId id, FriendshipId friendshipId)
        {
            Task<User> user = GetByIdAsync(id);

            return user.Result.friendsList.Find(x => x.Id == friendshipId);
        }
        
    }
}



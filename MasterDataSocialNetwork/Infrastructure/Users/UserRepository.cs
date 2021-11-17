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

       /* public async Task<List<User>> GetUserSuggestion(Tag usertag)
        {
            //return await ((DbSet<User>) base.getContext()).Where(user => user.tags.Contains(usertag)).ToListAsync();
            return await _context.Users.Where(u => u.tags.All(t => t.name.Equals(usertag))).ToListAsync();
        }

        public List<UserId> friendsSuggestion(UserId id)
        {
            List<UserId> friend = new List<UserId>();
            var tag = GetTagList(id).Result;
            foreach (Tag usertag in tag)
            {
                var userSuggestions = GetUserSuggestion(usertag).Result;
                foreach (User u in userSuggestions)
                {
                    if (!friend.Contains(u.Id))
                    {
                        friend.Add(u.Id);
                    }
                }
            }
            return friend;
        }*/

        public List<UserId> GetFriendsSuggestion(UserId userId)
        {
            List<UserId> friend = new List<UserId>();
            var tag = GetTagList(userId).Result;
            foreach (Tag usertag in tag)
            {
                using (SqlConnection connection =
                    new SqlConnection(
                        "Server=vs366.dei.isep.ipp.pt;Database=master;User id=sa;Password=rOfhiwMtvA==Xa5;"))
                {
                    SqlCommand command = new SqlCommand("",
                        connection);
                    command.Parameters.AddWithValue("@usertag", usertag.name);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            UserId uid = new UserId(reader["UserId"].ToString());
                            if (!(userId.AsString().Equals(uid.AsString()) || friend.Contains(uid)))
                            {
                                friend.Add(uid);
                            }
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
            }

            return friend;
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
    }
}



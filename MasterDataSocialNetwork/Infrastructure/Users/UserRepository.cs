using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Domain.Users;
using DDDNetCore.Infrastructure.Shared;
using Microsoft.AspNetCore.Server.IIS.Core;
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

        public async Task<List<User>> GetUserSuggestion(Tag usertag)
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
        }

        public Boolean checkIfFriends(UserId id, UserId id2){
            var friendships = _context.Users.Find(id).friendsList;
            if (friendships == null){
                return false;
            }
            foreach (var friendship in friendships)
            {
                if (friendship.friend == GetByIdAsync(id2).Result){
                    return true;
                }
            }
            return false;
        }

        public Boolean checkIfNotFriends(UserId id, UserId id2){
            return (!checkIfFriends(id,id2));
        }


    }
}



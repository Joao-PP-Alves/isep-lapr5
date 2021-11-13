using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<UserId> friendsSuggestion(UserId id)
        {
            List<UserId> friend = new List<UserId>();
            var tags = _context.Users.FirstOrDefault().tags;
            Console.WriteLine(tags);
            foreach (Tag usertag in tags)
            {
                using (_context.Database.OpenConnectionAsync())
                {
                    SqlCommand command = new SqlCommand("SELECT UserId FROM Users_tags ut WHERE ut.name = @userTag");
                    //command.CreateParameter("@usertag", usertag)
                    command.Parameters.AddWithValue("@userTag", usertag);
                    SqlDataReader reader = command.ExecuteReader();

                    try
                    {
                        while (reader.Read())
                        {
                            friend.Add(new UserId(reader["UserId"].ToString()));
                        }
                    }
                    finally
                    {
                        reader.Close();
                        _context.Database.CloseConnectionAsync();
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



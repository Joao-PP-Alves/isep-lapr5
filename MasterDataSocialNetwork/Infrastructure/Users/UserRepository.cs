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
    }
}



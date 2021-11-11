using System;
using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Domain.Services.DTO;
using DDDNetCore.Domain.Users;
using DDDNetCore.Infrastructure.Shared;
using Microsoft.Data.SqlClient;

namespace DDDNetCore.Infrastructure.Users
{

    public class UserRepository : BaseRepository<User, UserId>, IUserRepository
    {
        private readonly DDDNetCoreDbContext _context;

        public UserRepository(DDDNetCoreDbContext context) : base(context.Users)
        {
        }

        public List<User> friendsSuggestion(UserId id)
        {
            List<string> friends = new List<String>();
            using (SqlConnection connection = new SqlConnection("DefaultConnection"))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM USER", connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        friends.Add(reader.ToString());
                    }
                }
            }

            // return friends;
            return null;
        }
    }
}



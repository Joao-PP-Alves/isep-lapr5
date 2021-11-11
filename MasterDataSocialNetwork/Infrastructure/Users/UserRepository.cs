using System;
using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Domain.Services.DTO;
using DDDNetCore.Domain.Users;
using DDDNetCore.Infrastructure.Shared;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DDDNetCore.Infrastructure.Users {

    public class UserRepository : BaseRepository<User, UserId>, IUserRepository
    {
        private readonly DDDNetCoreDbContext _context;
        public UserRepository(DDDNetCoreDbContext context) : base(context.Users)
        {
            _context = context;
        }

        public List<User> friendsSuggestion(UserId id)
        {
            List<User> friends = new List<User>();
            var tags = _context.Users.SelectMany(user => user.tags).ToList();

            return friends;
        }
    }


}
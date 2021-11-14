using System;
using System.Collections.Generic;
using DDDNetCore.Domain.Services.DTO;
using DDDNetCore.Domain.Shared;
using System.Threading.Tasks;


namespace DDDNetCore.Domain.Users{
    public interface IUserRepository : IRepository<User,UserId>
    {
        public List<UserId> GetFriendsSuggestion(UserId id);

        public Boolean checkIfFriends(UserId id, UserId id2);

        public Boolean checkIfNotFriends(UserId id, UserId id2);

        public Task<List<User>> GetByEmail(string email);

        public Task<List<User>> GetByName(string name);

    }
}
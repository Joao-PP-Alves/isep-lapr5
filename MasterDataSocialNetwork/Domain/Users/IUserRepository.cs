using System.Collections.Generic;
using DDDNetCore.Domain.Services.DTO;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users{
    public interface IUserRepository : IRepository<User,UserId>
    {
        public List<string> friendsSuggestion(UserId id);

    }
}
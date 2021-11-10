using System.Security.Cryptography;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Connections
{
    public class CreatingConnectionDto
    {

        public UserId requester {get; set; } 

        public UserId targetUser {get; set; } 
        public Description description {get; set; }

        public Decision decision {get; set; }


        public CreatingConnectionDto(Description description, UserId requester, UserId targetUser)
        {
            this.description = description;
            this.requester = requester;
            this.targetUser = targetUser;
            this.decision = Decision.PENDING;
        }
    }
}
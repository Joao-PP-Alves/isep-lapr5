using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Services.CreatingDTO
{
    public class CreatingConnectionDto
    {

        public UserId requester {get; set; } 

        public UserId targetUser {get; set; } 
        public string description {get; set; }

        public Decision decision {get; set; }


        public CreatingConnectionDto(string description, UserId requester, UserId targetUser)
        {
            this.description = description;
            this.requester = requester;
            this.targetUser = targetUser;
            this.decision = Decision.PENDING;
        }
    }
}
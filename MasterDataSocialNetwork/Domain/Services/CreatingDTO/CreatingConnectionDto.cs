using System.Security.Cryptography;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Missions;

namespace DDDNetCore.Domain.Services.CreatingDTO
{
    public class CreatingConnectionDto
    {

        public UserId requester {get; set; } 

        public UserId targetUser {get; set; } 
        public Description description {get; set; }
        public MissionId missionId {get; set; }


        public CreatingConnectionDto(Description description, UserId requester, UserId targetUser)
        {
            this.description = description;
            this.requester = requester;
            this.targetUser = targetUser;
        }

        public CreatingConnectionDto(Description description, UserId requester, UserId targetUser,MissionId missionId)
        {
            this.description = description;
            this.requester = requester;
            this.targetUser = targetUser;
            this.missionId = missionId;
        }
    }
}
using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Introductions
{
    public class CreatingIntroductionDto
    {
        public Decision Decision {get;set;}
        public string Description {get;set;}
        public MissionId MissionId {get;set;}
        public UserId Requester {get;set;}
        public UserId Enabler {get;set;}
        public UserId TargetUser {get;set;}

        public CreatingIntroductionDto(string description, MissionId missionId, UserId requester, UserId enabler, UserId targetUser){
            this.Description = description;
            this.Requester = requester;
            this.Enabler = enabler;
            this.TargetUser = targetUser;
            this.MissionId = missionId;
            this.Decision = Decision.PENDING;
        }

    }
}
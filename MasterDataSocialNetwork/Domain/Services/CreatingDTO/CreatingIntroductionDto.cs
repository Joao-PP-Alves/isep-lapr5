using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Services.CreatingDTO
{
    public class CreatingIntroductionDto
    {
        public Decision Decision {get;set;}
        public Description MessageToIntermediate{get;set;}
        public Description MessageToTargetUser {get;set;}
        public Description MessageFromIntermediateToTargetUser {get;set;}
        public MissionId MissionId {get;set;}
        public UserId Requester {get;set;}
        public UserId Enabler {get;set;}
        public UserId TargetUser {get;set;}

        public CreatingIntroductionDto(Description messageToIntermediateUser, Description messageToTargetUser, MissionId missionId, UserId requester, UserId enabler, UserId targetUser){
            this.MessageToTargetUser = messageToTargetUser;
            this.MessageToIntermediate = messageToIntermediateUser;
            this.Requester = requester;
            this.Enabler = enabler;
            this.TargetUser = targetUser;
            this.MissionId = missionId;
            this.Decision = Decision.PENDING;
        }

    }
}
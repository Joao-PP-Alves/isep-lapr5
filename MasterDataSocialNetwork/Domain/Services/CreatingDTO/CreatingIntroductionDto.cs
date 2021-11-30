using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Services.CreatingDTO
{
    public class CreatingIntroductionDto
    {
        public Description MessageToIntermediate{get;set;}
        public Description MessageToTargetUser {get;set;}
        public Description MessageFromIntermediateToTargetUser {get;set;}
        //public MissionId MissionId {get;set;}
        public UserId Requester {get;set;}
        public UserId Enabler {get;set;}
        public UserId TargetUser {get;set;}

        public CreatingIntroductionDto(Description messageToIntermediateUser, Description messageToTargetUser, UserId requester, UserId enabler, UserId targetUser){
            MessageToTargetUser = messageToTargetUser;
            MessageToIntermediate = messageToIntermediateUser;
            Requester = requester;
            Enabler = enabler;
            TargetUser = targetUser;
           
        }

    }
}
using System;
using DDDNetCore.Domain.Introductions;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Services.DTO
{
    public class IntroductionDto{

        public Guid Id {get;set;}
        public IntroductionStatus decisionStatus {get;set;}
        //public MissionId MissionId {get;set;}
        public Description MessageToIntermediate{get;set;}
        public Description MessageToTargetUser {get;set;}
        public Description MessageFromIntermediateToTargetUser {get;set;}
        public UserId TargetUser {get;set;}
        public UserId Requester {get;set;}
        public UserId Enabler {get;set;}

        public IntroductionDto(Guid Id,  IntroductionStatus decision,Description messageToIntermediateUser, Description messageToTargetUser, Description messageFromIntermediateToTargetUser,UserId Requester, UserId Enabler, UserId TargetUser){
            this.Id = Id;
            MessageToTargetUser = messageToTargetUser;
            MessageToIntermediate = messageToIntermediateUser;
            MessageFromIntermediateToTargetUser = messageFromIntermediateToTargetUser;
            this.TargetUser = TargetUser;
            this.Requester = Requester;
            this.Enabler = Enabler;
            decisionStatus = decision;
        }

    }
}
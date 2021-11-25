using System;
using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Connections;
using DDDNetCore.Domain.Introductions;

namespace DDDNetCore.Domain.Services.DTO
{
    public class IntroductionDto{

        public Guid Id {get;set;}
        public IntroductionStatus decisionStatus {get;set;}
        public MissionId MissionId {get;set;}
        public Description MessageToIntermediate{get;set;}
        public Description MessageToTargetUser {get;set;}
        public Description MessageFromIntermediateToTargetUser {get;set;}
        public UserId TargetUser {get;set;}
        public UserId Requester {get;set;}
        public UserId Enabler {get;set;}

        public IntroductionDto(Guid Id, MissionId missionId, IntroductionStatus decision,Description messageToIntermediateUser, Description messageToTargetUser, Description messageFromIntermediateToTargetUser,UserId Requester, UserId Enabler, UserId TargetUser){
            this.Id = Id;
            this.MessageToTargetUser = messageToTargetUser;
            this.MessageToIntermediate = messageToIntermediateUser;
            this.MessageFromIntermediateToTargetUser = messageFromIntermediateToTargetUser;
            this.TargetUser = TargetUser;
            this.Requester = Requester;
            this.Enabler = Enabler;
            this.MissionId = missionId;
            this.decisionStatus = decision;
        }

    }
}
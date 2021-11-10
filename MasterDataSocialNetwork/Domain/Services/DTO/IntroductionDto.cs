using System;
using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Connections;

namespace DDDNetCore.Domain.Services.DTO
{
    public class IntroductionDto{

        public Guid Id {get;set;}
        public DecisionState Decision {get;set;}
        public MissionId MissionId {get;set;}
        public Description Description {get;set;}
        public UserId TargetUser {get;set;}
        public UserId Requester {get;set;}
        public UserId Enabler {get;set;}
        public ConnectionId ConnectionId {get;set;}

        public IntroductionDto(Guid Id, MissionId missionId, DecisionState decision, Description description, UserId Requester, UserId Enabler, UserId TargetUser){
            this.Id = Id;
            this.Description = Description;
            this.TargetUser = TargetUser;
            this.Requester = Requester;
            this.Enabler = Enabler;
            this.MissionId = missionId;
            this.Decision = decision;
        }
    }
}
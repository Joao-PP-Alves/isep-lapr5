using System;
using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Introductions
{
    public class IntroductionDto{

        public Guid Id {get;set;}
        public Decision Decision {get;set;}
        public MissionId MissionId {get;set;}
        public string Description {get;set;}
        public UserId TargetUser {get;set;}
        public UserId Requester {get;set;}
        public UserId Enabler {get;set;}

        public IntroductionDto(Guid Id, MissionId missionId, Decision decision, string Description, UserId Requester, UserId Enabler, UserId TargetUser){
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
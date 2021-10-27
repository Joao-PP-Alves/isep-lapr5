using System;
using System.ComponentModel;
using System.Configuration;
using System.Dynamic;
using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;

namespace DDDNetCore.Domain.Introductions
{
    public class Introduction : Entity<IntroductionId>, IAggregateRoot
    {
        public Decision Decision {get; private set;}
        public MissionId MissionId {get; private set;}
        public string Description {get; private set;}
        public UserId Requester {get; private set;}
        public UserId Enabler {get;private set;}
        public UserId TargetUser {get; private set;} 
        public bool Active {get; private set;}

        private Introduction(){
            this.Active = true;
        }

        public Introduction(string Description,MissionId missionId,UserId Requester, UserId Enabler, UserId TargetUser){
            this.Id = new IntroductionId(Guid.NewGuid());
            this.Decision = Decision.PENDING;
            this.Description = Description;
            this.TargetUser = TargetUser;
            this.Enabler = Enabler;
            this.Requester = Requester;
            this.MissionId = missionId;
            this.Active = true;
        }

        public Introduction(string Description,MissionId missionId,Decision decision, UserId Requester, UserId Enabler, UserId TargetUser){
            this.Id = new IntroductionId(Guid.NewGuid());
            this.Decision = decision;
            this.Description = Description;
            this.TargetUser = TargetUser;
            this.Enabler = Enabler;
            this.Requester = Requester;
            this.MissionId = missionId;
            this.Active = true;
        }

        public void AcceptedIntroduction(){
            this.Decision = Decision.ACCEPTED;
        }

        public void DeclinedIntroduction(){
            this.Decision = Decision.DECLINED;
        }

        public void MarkAsInative(){
            this.Active = false;
        }

        public void MakeDecision(Decision newDecision){
            if(!this.Active)
                throw new BusinessRuleValidationException("It is not possible to change the decision to an inactive introduction.");
            this.Decision = newDecision;    
        }

        public void ChangeDescription(string newDescription){
            if(!this.Active)
                throw new BusinessRuleValidationException("It is not possible to change the description to an inactive introduction.");
            this.Description = newDescription;
        }

        public void ChangeTargetUser(UserId userId){
            if(!this.Active)
                throw new BusinessRuleValidationException("It is not possible to change the target user to an inactive introduction.");
            if (userId == null)
                throw new BusinessRuleValidationException("Every Introducion requires a valid target user.");
            this.TargetUser = userId;    
        }

        public void ChangeRequester(UserId userId){
            if(!this.Active)
                throw new BusinessRuleValidationException("It is not possible to change the requester to an inactive introduction.");
            if (userId == null)
                throw new BusinessRuleValidationException("Every Introducion requires a valid requester.");
            this.Requester = userId;    
        }

        public void ChangeEnabler(UserId userId){
            if(!this.Active)
                throw new BusinessRuleValidationException("It is not possible to change the enabler to an inactive introduction.");
            if (userId == null)
                throw new BusinessRuleValidationException("Every Introducion requires a valid enabler.");
            this.Enabler = userId;    
        }

    }
}
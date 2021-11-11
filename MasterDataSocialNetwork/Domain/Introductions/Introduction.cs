using System;
using System.ComponentModel;
using System.Configuration;
using System.Dynamic;
using DDDNetCore.Domain.Connections;
using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Introductions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;

namespace DDDNetCore.Domain.Introductions
{
    public class Introduction : Entity<IntroductionId>, IAggregateRoot
    {
        public IntroductionStatus decisionStatus {get; private set;}
        public MissionId MissionId {get; private set;}
        public Description MessageToIntermediate{get;private set;}
        public Description MessageToTargetUser {get;private set;}
        public Description MessageFromIntermediateToTargetUser {get;private set;}
        public UserId Requester {get; private set;}
        public UserId Enabler {get;private set;}
        public UserId TargetUser {get; private set;}
        public bool Active {get; private set;}

        private Introduction(){
            this.Active = true;
        }

        public Introduction(Description messageToTargetUser,Description messageToIntermediate,MissionId missionId,UserId Requester, UserId Enabler, UserId TargetUser){
            this.Id = new IntroductionId(Guid.NewGuid());
            this.decisionStatus = IntroductionStatus.PENDING_APPROVAL;
            this.MessageToTargetUser = messageToTargetUser;
            this.MessageToIntermediate = messageToIntermediate;
            this.TargetUser = TargetUser;
            this.Enabler = Enabler;
            this.Requester = Requester;
            this.MissionId = missionId;
            this.Active = true;
        }

        public Introduction(Description messageToTargetUser,Description messageToIntermediate,MissionId missionId,Decision decision, UserId Requester, UserId Enabler, UserId TargetUser){
            this.Id = new IntroductionId(Guid.NewGuid());
            this.decisionStatus = IntroductionStatus.PENDING_APPROVAL;
            this.MessageToTargetUser = messageToTargetUser;
            this.MessageToIntermediate = messageToIntermediate;
            this.TargetUser = TargetUser;
            this.Enabler = Enabler;
            this.Requester = Requester;
            this.MissionId = missionId;
            this.Active = true;
        }

        public void AcceptedIntroduction(){
            this.decisionStatus = IntroductionStatus.ACCEPTED;
        }

        public void DeclinedIntroduction(){
            this.decisionStatus = IntroductionStatus.DECLINED;
        }

        public void MarkAsInative(){
            this.Active = false;
        }

        public void changeMessageToTargetUser(Description message){
            this.MessageToTargetUser = message;
        }

        public void changeMessageToIntermediate(Description message){
            this.MessageToIntermediate = message;
        }

        public void changeIntermediateToTargetUserDescription(Description message){
            this.MessageFromIntermediateToTargetUser = message;
        }

        public void approveIntermediate(){
            this.decisionStatus = IntroductionStatus.APPROVAL_ACCEPTED;
        }

        public void declineIntermediate(){
            this.decisionStatus = IntroductionStatus.APPROVAL_DECLINED;
        }

        public void makeDecision(IntroductionStatus decision){
            this.decisionStatus = decision;
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
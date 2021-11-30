using System;
using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Introductions
{
    public class Introduction : Entity<IntroductionId>, IAggregateRoot
    {
        public IntroductionStatus decisionStatus {get; private set;}
        //public MissionId MissionId {get; private set;}
        public Description MessageToIntermediate{get;private set;}
        public Description MessageToTargetUser {get;private set;}
        public Description MessageFromIntermediateToTargetUser {get;private set;}
        public UserId Requester {get; private set;}
        public UserId Enabler {get;private set;}
        public UserId TargetUser {get; private set;}
        public bool Active {get; private set;}

        private Introduction(){
            Active = true;
        }

        // public Introduction(Description messageToTargetUser,Description messageToIntermediate,MissionId missionId,UserId Requester, UserId Enabler, UserId TargetUser){
        //     Id = new IntroductionId(Guid.NewGuid());
        //     decisionStatus = IntroductionStatus.PENDING_APPROVAL;
        //     MessageToTargetUser = messageToTargetUser;
        //     MessageToIntermediate = messageToIntermediate;
        //     this.TargetUser = TargetUser;
        //     this.Enabler = Enabler;
        //     this.Requester = Requester;
        //     this.MissionId = missionId;
        //     Active = true;
        // }

        // public Introduction(Description messageToTargetUser,Description messageToIntermediate,MissionId missionId,Decision decision, UserId Requester, UserId Enabler, UserId TargetUser){
        //     this.Id = new IntroductionId(Guid.NewGuid());
        //     this.decisionStatus = IntroductionStatus.PENDING_APPROVAL;
        //     this.MessageToTargetUser = messageToTargetUser;
        //     this.MessageToIntermediate = messageToIntermediate;
        //     this.TargetUser = TargetUser;
        //     this.Enabler = Enabler;
        //     this.Requester = Requester;
        //     this.MissionId = missionId;
        //     this.Active = true;
        // }
        
        public Introduction(Description messageToTargetUser,Description messageToIntermediate,UserId Requester, UserId Enabler, UserId TargetUser){
            Id = new IntroductionId(Guid.NewGuid());
            decisionStatus = IntroductionStatus.PENDING_APPROVAL;
            MessageToTargetUser = messageToTargetUser;
            MessageToIntermediate = messageToIntermediate;
            this.TargetUser = TargetUser;
            this.Enabler = Enabler;
            this.Requester = Requester;
            Active = true;
        }
        
        
        public Introduction(Description messageToTargetUser,Description messageToIntermediate,Decision decision, UserId Requester, UserId Enabler, UserId TargetUser){
            Id = new IntroductionId(Guid.NewGuid());
            decisionStatus = IntroductionStatus.PENDING_APPROVAL;
            MessageToTargetUser = messageToTargetUser;
            MessageToIntermediate = messageToIntermediate;
            this.TargetUser = TargetUser;
            this.Enabler = Enabler;
            this.Requester = Requester;
            Active = true;
        }

        public void AcceptedIntroduction(){
            if(decisionStatus != IntroductionStatus.PENDING_APPROVAL){
                throw new BusinessRuleValidationException("You cannot approve an introduction that is not pending.");
            }
            decisionStatus = IntroductionStatus.ACCEPTED;
        }

        public void DeclinedIntroduction(){
            if(decisionStatus != IntroductionStatus.PENDING_APPROVAL){
                throw new BusinessRuleValidationException("You cannot approve an introduction that is not pending.");
            }
            decisionStatus = IntroductionStatus.DECLINED;
        }

        public void MarkAsInative(){
            Active = false;
        }

        public void changeMessageToTargetUser(Description message){
            MessageToTargetUser = message;
        }

        public void changeMessageToIntermediate(Description message){
            MessageToIntermediate = message;
        }

        public void changeIntermediateToTargetUserDescription(Description message){
            MessageFromIntermediateToTargetUser = message;
        }

        public void approveIntermediate(){
            if(decisionStatus != IntroductionStatus.PENDING_APPROVAL){
                throw new BusinessRuleValidationException("You cannot approve an introduction that is not pending.");
            }
            decisionStatus = IntroductionStatus.APPROVAL_ACCEPTED;
        }

        public void declineIntermediate(){
            if(decisionStatus != IntroductionStatus.PENDING_APPROVAL){
                throw new BusinessRuleValidationException("You cannot approve an introduction that is not pending.");
            }
            decisionStatus = IntroductionStatus.APPROVAL_DECLINED;
        }

        public void makeDecision(IntroductionStatus decision){
            decisionStatus = decision;
        }

        public void ChangeTargetUser(UserId userId){
            if(!Active)
                throw new BusinessRuleValidationException("It is not possible to change the target user to an inactive introduction.");
            if (userId == null)
                throw new BusinessRuleValidationException("Every Introducion requires a valid target user.");
            TargetUser = userId;    
        }

        public void ChangeRequester(UserId userId){
            if(!Active)
                throw new BusinessRuleValidationException("It is not possible to change the requester to an inactive introduction.");
            if (userId == null)
                throw new BusinessRuleValidationException("Every Introducion requires a valid requester.");
            Requester = userId;    
        }

        public void ChangeEnabler(UserId userId){
            if(!Active)
                throw new BusinessRuleValidationException("It is not possible to change the enabler to an inactive introduction.");
            if (userId == null)
                throw new BusinessRuleValidationException("Every Introducion requires a valid enabler.");
            Enabler = userId;    
        }

    }
}
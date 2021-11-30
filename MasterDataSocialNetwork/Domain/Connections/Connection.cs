using System;
using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Connections{

    public class Connection : Entity<ConnectionId> , IAggregateRoot {

        public UserId requester { get;  private set; }

        public UserId targetUser { get;  private set; }

        public Description description { get;  private set; }

        public Decision decision { get; private set; }

        // in case the connection is created in order to finish the mission
        //public MissionId missionId { get; private set;}

        public bool active{get; private set; }

        private Connection(){

        }

        // public Connection(UserId requester, UserId targetUser, Description description){
        //     Id = new ConnectionId(Guid.NewGuid());
        //     this.requester = requester;
        //     this.targetUser = targetUser;
        //     this.description = description;
        //     decision = Decision.PENDING;
        //     
        //     active = true;
        // }

        public Connection(UserId requester, UserId targetUser, Description description){
            Id = new ConnectionId(Guid.NewGuid());
            this.requester = requester;
            this.targetUser = targetUser;
            this.description = description;
            decision = Decision.PENDING;
            active = true;
        }

        public void acceptConnection(){
            if(decision != Decision.PENDING){
                throw new BusinessRuleValidationException("You cannot accept connections that are not pending.");
            }
            decision = Decision.ACCEPTED;
        }

        public void declineConnection(){
            if(decision != Decision.PENDING){
                throw new BusinessRuleValidationException("You cannot decline connections that are not pending.");
            }
            decision = Decision.DECLINED;
        }

        public void MarkAsInative()
        {
            active = false;
        }

        public void MakeDecision(Decision newDecision){
            if(!active)
                throw new BusinessRuleValidationException("It is not possible to change the decision to an inactive introduction.");
            decision = newDecision;    
        }

        public void ChangeDescription(Description newDescription){
            if(!active)
                throw new BusinessRuleValidationException("It is not possible to change the description to an inactive introduction.");
            description = newDescription;
        }

        public void ChangeTargetUser(UserId userId){
            if(!active)
                throw new BusinessRuleValidationException("It is not possible to change the target user to an inactive introduction.");
            if (userId == null)
                throw new BusinessRuleValidationException("Every Introducion requires a valid target user.");
            targetUser = userId;    
        }

        public void ChangeRequester(UserId userId){
            if(!active)
                throw new BusinessRuleValidationException("It is not possible to change the requester to an inactive introduction.");
            if (userId == null)
                throw new BusinessRuleValidationException("Every Introducion requires a valid requester.");
            requester = userId;    
        }

    }
}
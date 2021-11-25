using DDDNetCore.Domain.Users;
using System;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Missions;

namespace DDDNetCore.Domain.Connections{

    public class Connection : Entity<ConnectionId> , IAggregateRoot {

        public UserId requester { get;  private set; }

        public UserId targetUser { get;  private set; }

        public Description description { get;  private set; }

        public Decision decision { get; private set; }

        // in case the connection is created in order to finish the mission
        public MissionId missionId { get; private set;}

        public bool active{get; private set; }

        private Connection(){

        }

        public Connection(UserId requester, UserId targetUser, Description description){
            this.Id = new ConnectionId(Guid.NewGuid());
            this.requester = requester;
            this.targetUser = targetUser;
            this.description = description;
            this.decision = Decision.PENDING;
            this.active = true;
        }

        public Connection(UserId requester, UserId targetUser, Description description, MissionId missionId){
            this.Id = new ConnectionId(Guid.NewGuid());
            this.requester = requester;
            this.targetUser = targetUser;
            this.description = description;
            this.missionId = missionId;
            this.decision = Decision.PENDING;
            this.active = true;
        }

        public void acceptConnection(){
            if(this.decision != Decision.PENDING){
                throw new BusinessRuleValidationException("You cannot accept connections that are not pending.");
            }
            this.decision = Decision.ACCEPTED;
        }

        public void declineConnection(){
            if(this.decision != Decision.PENDING){
                throw new BusinessRuleValidationException("You cannot decline connections that are not pending.");
            }
            this.decision = Decision.DECLINED;
        }

        public void MarkAsInative()
        {
            this.active = false;
        }

        public void MakeDecision(Decision newDecision){
            if(!this.active)
                throw new BusinessRuleValidationException("It is not possible to change the decision to an inactive introduction.");
            this.decision = newDecision;    
        }

        public void ChangeDescription(Description newDescription){
            if(!this.active)
                throw new BusinessRuleValidationException("It is not possible to change the description to an inactive introduction.");
            this.description = newDescription;
        }

        public void ChangeTargetUser(UserId userId){
            if(!this.active)
                throw new BusinessRuleValidationException("It is not possible to change the target user to an inactive introduction.");
            if (userId == null)
                throw new BusinessRuleValidationException("Every Introducion requires a valid target user.");
            this.targetUser = userId;    
        }

        public void ChangeRequester(UserId userId){
            if(!this.active)
                throw new BusinessRuleValidationException("It is not possible to change the requester to an inactive introduction.");
            if (userId == null)
                throw new BusinessRuleValidationException("Every Introducion requires a valid requester.");
            this.requester = userId;    
        }

    }
}
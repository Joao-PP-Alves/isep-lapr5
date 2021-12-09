using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class Friendship : Entity<FriendshipId>
    {

        public ConnectionStrength connection_strength {get; set;}

        public RelationshipStrength relationship_strength {get; set;}

        public UserId friend {get; private set;}
        
        public UserId requester { get; private set; }
        
        public TagId friendshipTag {get; set;}

        public bool Active{get; set;}

        public Friendship(){
            this.Active = true;
        }

        public Friendship(UserId friend, UserId requester,TagId friendTag) {
            this.Id = new FriendshipId(Guid.NewGuid());
            this.friend = friend;
            this.requester = requester;
            this.connection_strength = new ConnectionStrength("1");
            this.relationship_strength = new RelationshipStrength("1");
            this.friendshipTag = friendTag;
            this.Active = true;
        }

        public Friendship(UserId friend, UserId requester, ConnectionStrength connection_strength, RelationshipStrength relationship_strength, TagId friendshipTags) {
            this.Id = new FriendshipId(Guid.NewGuid());
            this.friend = friend;
            this.requester = requester;
            this.connection_strength = connection_strength;
            this.relationship_strength = relationship_strength;
            this.friendshipTag = friendshipTags;
            this.Active = true;
        }

        public void ChangeConnectionStrenght(ConnectionStrength connection_strength) {
            if(this.friend == null) {
                throw new BusinessRuleValidationException("The relationship is invalid. One or both users are null");
            }
            this.connection_strength = connection_strength;
        }

        public void ChangeFriendshipTag(TagId friendshipTag){
            if(this.friend == null){
                throw new BusinessRuleValidationException("The relationship is invalid. One or both users are null");
            }
            this.friendshipTag = friendshipTag;
        }

        public void deactivate() {
            if(this.Active == false) {
                throw new Exception("The FriendShip is already inactive");
            } else {
                this.Active = true;
            }
        }
        
    }
}
using System;
using System.Collections.Generic;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class Friendship : Entity<FriendshipId> {


        public ConnectionStrength connection_strenght {get; set;}

        public RelationshipStrength relationship_strenght {get; set;}

        public User friend {get; set;}
        
        public Tag friendshipTag {get; set;}

        public bool Active{get; set;}

        public Friendship(){
            this.Active = true;
        }

        public Friendship(User friend) {
            this.Id = new FriendshipId(Guid.NewGuid());
            this.friend = friend;
            this.connection_strenght = new ConnectionStrength(1);
            this.relationship_strenght = new RelationshipStrength(1);
            this.friendshipTag = new Tag();
            this.Active = true;
        }

        public Friendship(User friend, ConnectionStrength connection_strenght, RelationshipStrength relationship_strenght, Tag friendshipTags) {
            this.Id = new FriendshipId(Guid.NewGuid());
            this.friend = friend;
            this.connection_strenght = connection_strenght;
            this.relationship_strenght = relationship_strenght;
            this.friendshipTag = friendshipTags;
            this.Active = true;
        }

        public void ChangeConnectionStrenght(ConnectionStrength connection_strenght) {
            if(this.friend == null) {
                throw new BusinessRuleValidationException("The relationship is invalid. One or both users are null");
            }
            this.connection_strenght = connection_strenght;
        }

        public void ChangeFriendshipTag(Tag friendshipTag){
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
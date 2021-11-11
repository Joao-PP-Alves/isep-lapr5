using System;
using System.Collections.Generic;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class Friendship : Entity<FriendshipId> {


        public float connection_strenght {get; set;}

        public float relationship_strenght {get; set;}

        public User friend {get; set;}
        
        public Tag friendshipTag {get; set;}

        public bool Active{get; set;}

        public Friendship(){
            this.Active = true;
        }

        public Friendship(User friend) {
            this.Id = new FriendshipId(Guid.NewGuid());
            this.friend = friend;
            this.connection_strenght = 0;
            this.relationship_strenght = 0;
            this.friendshipTag = new Tag();
            this.Active = true;
        }

        public Friendship(User friend, float connection_strenght, float relationship_strenght, Tag friendshipTags) {
            this.Id = new FriendshipId(Guid.NewGuid());
            this.friend = friend;
            this.connection_strenght = connection_strenght;
            this.relationship_strenght = relationship_strenght;
            this.friendshipTag = friendshipTag;
            this.Active = true;
        }

        public void ChangeConnectionStrenght(float connection_strenght) {
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
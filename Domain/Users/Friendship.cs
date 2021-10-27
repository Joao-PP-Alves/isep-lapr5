using System;
using System.Collections.Generic;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class FriendShip : Entity<FriendShipId> {


        private float connection_strenght {get; set;}

        private float relationship_strenght {get; set;}

        private User user1 {get; set;}

        private User user2 {get; set;}

        private List<Tag> friendshipTags {get; set;}

        private bool Active{get; set;}

        public FriendShip(){
            this.Active = true;
        }

        public FriendShip(User user1, User user2) {
            this.Id = new FriendShipId(Guid.NewGuid());
            this.user1 = user1;
            this.user2 = user2;
            this.connection_strenght = 0;
            this.relationship_strenght = 0;
            this.friendshipTags = new List<Tag>();
            this.Active = true;
        }

        public FriendShip(User user1, User user2, float connection_strenght, float relationship_strenght, List<Tag> friendshipTags) {
            this.Id = new FriendShipId(Guid.NewGuid());
            this.user1 = user1;
            this.user2 = user2;
            this.connection_strenght = connection_strenght;
            this.relationship_strenght = relationship_strenght;
            this.friendshipTags = friendshipTags;
            this.Active = true;
        }

        public void ChangeConnectionStrenght(float connection_strenght) {
            if(this.user1 == null || this.user2 == null) {
                throw new BusinessRuleValidationException("The relationship is invalid. One or both users are null");
            }
            this.connection_strenght = connection_strenght;
        }

        protected void deactivate() {
            if(this.Active == false) {
                throw new Exception("The FriendShip is already inactive");
            } else {
                this.Active = true;
            }
        }
        
    }
}
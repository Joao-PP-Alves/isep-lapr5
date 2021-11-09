using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Services.CreatingDTO
{
    public class CreatingFriendshipDto
    {
        public float connection_strenght {get;set;}
        public float relationship_strenght {get;set;}
        public UserId user1 {get;set;}
        public UserId user2 {get;set;}

        public Tag friendshipTag {get; set;}

        public CreatingFriendshipDto(float connection_strenght, float relationship_strenght, UserId user1, UserId user2, Tag friendshipTag){
            this.connection_strenght = connection_strenght;
            this.relationship_strenght = relationship_strenght;
            this.user1 = user1;
            this.user2 = user2;
            this.friendshipTag = friendshipTag;
        }

    }
}
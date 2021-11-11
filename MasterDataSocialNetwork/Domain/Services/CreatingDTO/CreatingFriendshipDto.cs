using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Services.CreatingDTO
{
    public class CreatingFriendshipDto
    {
        public float connection_strenght {get;set;}
        public float relationship_strenght {get;set;}
        public UserId friend {get;set;}

        public Tag friendshipTag {get; set;}

        public CreatingFriendshipDto(float connection_strenght, float relationship_strenght, UserId friend, Tag friendshipTag){
            this.connection_strenght = connection_strenght;
            this.relationship_strenght = relationship_strenght;
            this.friend = friend;
            this.friendshipTag = friendshipTag;
        }

    }
}
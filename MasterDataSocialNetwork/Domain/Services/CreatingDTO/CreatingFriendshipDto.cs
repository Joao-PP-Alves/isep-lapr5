using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Services.CreatingDTO
{
    public class CreatingFriendshipDto
    {
        public ConnectionStrength connection_strenght {get;set;}
        public RelationshipStrength relationship_strenght {get;set;}
        public User friend {get;set;}
        
        public User requester { get; set; }

        public Tag friendshipTag {get; set;}

        public CreatingFriendshipDto(ConnectionStrength connection_strenght, RelationshipStrength relationship_strenght, User friend, User requester, Tag friendshipTag){
            this.connection_strenght = connection_strenght;
            this.relationship_strenght = relationship_strenght;
            this.friend = friend;
            this.requester = requester;
            this.friendshipTag = friendshipTag;
        }

    }
}
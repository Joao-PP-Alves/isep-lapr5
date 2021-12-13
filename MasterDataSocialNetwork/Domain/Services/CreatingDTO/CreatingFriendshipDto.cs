using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Services.CreatingDTO
{
    public class CreatingFriendshipDto
    {
        public ConnectionStrength connection_strength {get;set;}
        public RelationshipStrength relationship_strength {get;set;}
        public UserId friend {get;set;}
        
        public UserId requester { get; set; }

        public TagId friendshipTag {get; set;}

        public CreatingFriendshipDto(ConnectionStrength connection_strength, RelationshipStrength relationship_strength, UserId friend, UserId requester, TagId friendshipTag){
            this.connection_strength = connection_strength;
            this.relationship_strength = relationship_strength;
            this.friend = friend;
            this.requester = requester;
            this.friendshipTag = friendshipTag;
        }

    }
}
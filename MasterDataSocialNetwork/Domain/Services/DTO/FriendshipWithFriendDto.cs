using System;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Services.DTO
{
    public class FriendshipWithFriendDto
    {
        public Guid Id { get; set; }
        public ConnectionStrength connection_strength { get; set; }

        public RelationshipStrength relationship_strength { get; set; }

        public UserId friend { get; set; }
        
        public UserId requester { get; set; }

        public Tag friendshipTag { get; set; }

        // change to value objects from user
        public User friendObject {get; set;}

        public FriendshipWithFriendDto(Guid Id, ConnectionStrength connection_strength, RelationshipStrength relationship_strength, UserId friend, UserId requester, Tag friendshipTag, User friendObject)
        {
            this.Id = Id;
            this.connection_strength = connection_strength;
            this.relationship_strength = relationship_strength;
            this.friend = friend;
            this.requester = requester;
            this.friendshipTag = friendshipTag;
            this.friendObject = friendObject;
        }
    }
}
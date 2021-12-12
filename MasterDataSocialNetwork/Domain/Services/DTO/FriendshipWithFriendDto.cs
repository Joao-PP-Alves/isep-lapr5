using System;
using System.Diagnostics;
using DDDNetCore.Domain.Users;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;

namespace DDDNetCore.Domain.Services.DTO
{
    public class FriendshipWithFriendDto
    {
        public Guid Id { get; set; }
        public ConnectionStrength connection_strength { get; set; }

        public RelationshipStrength relationship_strength { get; set; }

        public UserId friend { get; set; }
        
        public UserId requester { get; set; }

        public TagId friendshipTag { get; set; }

        // change to value objects from user
        public User friendObject {get; set;}

        public FriendshipWithFriendDto(Guid Id, ConnectionStrength connection_strength, RelationshipStrength relationship_strength, UserId friend, UserId requester, TagId friendshipTag, User friendObject)
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
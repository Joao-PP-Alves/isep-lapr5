using System;
using System.Diagnostics;
using DDDNetCore.Domain.Users;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;

namespace DDDNetCore.Domain.Services.DTO
{
    public class FriendshipDto
    {
        public Guid Id { get; set; }
        public ConnectionStrength connection_strength { get; set; }

        public RelationshipStrength relationship_strength { get; set; }

        public UserId friend { get; set; }
        
        public UserId requester { get; set; }

        public Tag friendshipTag { get; set; }

        public FriendshipDto(Guid Id, ConnectionStrength connection_strength, RelationshipStrength relationship_strength, UserId friend, UserId requester, Tag friendshipTag)
        {
            this.Id = Id;
            this.connection_strength = connection_strength;
            this.relationship_strength = relationship_strength;
            this.friend = friend;
            this.requester = requester;
            this.friendshipTag = friendshipTag;
        }
    }
}
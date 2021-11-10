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
        public FriendshipId Id { get; set; }
        public float connection_strenght { get; set; }

        public float relationship_strenght { get; set; }

        public UserId user1 { get; set; }

        public UserId user2 { get; set; }

        public Tag friendshipTag { get; set; }

        public FriendshipDto(FriendshipId Id, float connection_strenght, float relationship_strenght, UserId user1, UserId user2, Tag friendshipTag)
        {
            this.Id = Id;
            this.connection_strenght = connection_strenght;
            this.relationship_strenght = relationship_strenght;
            this.user1 = user1;
            this.user2 = user2;
            this.friendshipTag = friendshipTag;
        }
    }
}
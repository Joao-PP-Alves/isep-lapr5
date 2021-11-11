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

        public UserId friend { get; set; }

        public Tag friendshipTag { get; set; }

        public FriendshipDto(FriendshipId Id, float connection_strenght, float relationship_strenght, UserId friend, Tag friendshipTag)
        {
            this.Id = Id;
            this.connection_strenght = connection_strenght;
            this.relationship_strenght = relationship_strenght;
            this.friend = friend;
            this.friendshipTag = friendshipTag;
        }
    }
}
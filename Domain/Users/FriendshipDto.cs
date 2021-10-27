using System;
using System.Diagnostics;
using DDDNetCore.Domain.Users;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;

namespace DDDNetCore.Domain.Users
{
    public class FriendshipDto
    {
        private Guid Id { get; set; }
        private float connection_strenght { get; set; }

        private float relationship_strenght { get; set; }

        private UserId user1 { get; set; }

        private UserId user2 { get; set; }

        private List<Tag> friendshipTags { get; set; }


        public FriendshipDto(Guid Id, float connection_strenght, float relationship_strenght, UserId user1, UserId user2, List<Tag> friendshipTags)
        {
            this.Id = Id;
            this.connection_strenght = connection_strenght;
            this.relationship_strenght = relationship_strenght;
            this.user1 = user1;
            this.user2 = user2;
            this.friendshipTags = friendshipTags;
        }
    }
}
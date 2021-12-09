using System;
using System.Diagnostics;
using DDDNetCore.Domain.Users;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;

namespace DDDNetCore.Domain.Services.DTO
{
    public class UpdateFriendshipTagDto
    {
        public Guid Id { get; set; }
        public Guid tag { get; set; }

        public UpdateFriendshipTagDto(Guid Id, Guid tag)
        {
            this.Id = Id;
            this.tag = tag;
        }
    }
}
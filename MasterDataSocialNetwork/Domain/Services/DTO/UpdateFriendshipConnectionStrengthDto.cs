using System;
using System.Diagnostics;
using DDDNetCore.Domain.Users;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;

namespace DDDNetCore.Domain.Services.DTO
{
    public class UpdateFriendshipConnectionStrengthDto
    {
        public Guid Id { get; set; }
        public String connection_strength { get; set; }

        public UpdateFriendshipConnectionStrengthDto(Guid Id, String connection_strength)
        {
            this.Id = Id;
            this.connection_strength = connection_strength;
        }
    }
}
using System;
using DDDNetCore.Domain.Users;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace DDDNetCore.Domain.Services.DTO
{
    public class UserPerspectiveDto
    {
        public Guid userId {get;set;}

        public Name userName {get;set;}
        
        public Guid parentId { get; set; }
        
        public ConnectionStrength connectionStrength { get; set; }
        
        public RelationshipStrength relationshipStrength { get; set; }

        public UserPerspectiveDto(Guid userId, Name userName, Guid parentId, ConnectionStrength connectionStrength,
            RelationshipStrength relationshipStrength)
        {
            this.userId = userId;
            this.userName = userName;
            this.parentId = parentId;
            this.connectionStrength = connectionStrength;
            this.relationshipStrength = relationshipStrength;
        }
    }
}
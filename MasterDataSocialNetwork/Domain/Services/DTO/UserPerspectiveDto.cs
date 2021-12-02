using System;
using DDDNetCore.Domain.Users;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace DDDNetCore.Domain.Services.DTO
{
    public class UserPerspectiveDto
    {
        public String userId {get;set;}

        public Name userName {get;set;}
        
        public String parentId { get; set; }
        
        public String connectionStrength { get; set; }
        
        public String relationshipStrength { get; set; }

        public UserPerspectiveDto(String userId, Name userName, String parentId, String connectionStrength,
            String relationshipStrength)
        {
            this.userId = userId;
            this.userName = userName;
            this.parentId = parentId;
            this.connectionStrength = connectionStrength;
            this.relationshipStrength = relationshipStrength;
        }
    }
}
using System;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Services.DTO
{
    public class TagCloudDto
    {
        public string tagName { get; set; }
        
        public int tagQuantity { get; set; }

        public TagCloudDto(string tagName, int tagQuantity)
        {
            this.tagName = tagName;
            this.tagQuantity = tagQuantity;
        }
    }
}
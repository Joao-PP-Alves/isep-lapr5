using System;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Services.DTO
{
    public class TagCloudDto
    {
        public String tagName { get; set; }
        
        public int tagQuantity { get; set; }

        public TagCloudDto(String tagName, int tagQuantity)
        {
            this.tagName = tagName;
            this.tagQuantity = tagQuantity;
        }
    }
}
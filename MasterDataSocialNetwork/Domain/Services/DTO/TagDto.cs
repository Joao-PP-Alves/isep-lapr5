using System;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Services.DTO
{
    public class TagDto
    {
        public Guid Id { get; set; }
        
        public Name name { get; set; }

        public TagDto(Guid Id, Name name)
        {
            this.Id = Id;
            this.name = name;
        }
    }
}
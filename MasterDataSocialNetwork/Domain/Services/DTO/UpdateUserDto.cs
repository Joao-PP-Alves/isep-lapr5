using System;
using System.Collections.Generic;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Services.DTO
{
    public class UpdateUserDto
    {
        public Guid Id {get;set;}

        public Name name {get;set;}
        public Email email {get;}
        public ICollection<Tag> tags {get;set;}

        public LifeDate birthDate { get; set; }

        public PhoneNumber phoneNumber {get;set;}

        public EmotionalState emotionalState {get;set;}

        public EmotionTime EmotionTime {get;set;}

        public UpdateUserDto(Guid Id, Name name, Email email, PhoneNumber phoneNumber, LifeDate birthDate, ICollection<Tag> tags, EmotionalState emotionalState){
            this.Id = Id;
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.birthDate = birthDate;
            this.email = email;
            this.tags = tags;
            this.emotionalState = emotionalState;
        }
    }
}
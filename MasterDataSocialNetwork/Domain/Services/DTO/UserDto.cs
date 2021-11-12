using System;
using System.Collections.Generic;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Services.DTO{
    public class UserDto {
        public Guid Id {get;set;}

        public Name name {get;set;}
        public Email email {get;}
        public List<Tag> tags {get;set;}

        public PhoneNumber phoneNumber {get;set;}

        public EmotionalState emotionalState {get;set;}

        public EmotionTime EmotionTime {get;set;}

        public UserDto(Guid Id, Name name, Email email, PhoneNumber phoneNumber, List<Tag> tags, EmotionalState emotionalState,EmotionTime EmotionTime){
            this.Id = Id;
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.tags = tags;
            this.emotionalState = emotionalState;
            this.EmotionTime = EmotionTime;
        }
    }
}
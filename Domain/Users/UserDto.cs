using System;
using System.Collections.Generic;

namespace DDDNetCore.Domain.Users{
    public class UserDto {
        public Guid Id {get;set;}

        public string name {get;set;}
        public Email email {get;}
        public List<Tag> tags {get;set;}

        public PhoneNumber phoneNumber {get;set;}

        public EmotionalState emotionalState {get;set;}

        public UserDto(Guid Id, string name, Email email, PhoneNumber phoneNumber, List<Tag> tags, EmotionalState emotionalState){
            this.Id = Id;
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.tags = tags;
            this.emotionalState = emotionalState;
        }
    }
}
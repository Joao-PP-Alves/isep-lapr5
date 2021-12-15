using System;
using System.Collections.Generic;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Services.DTO{
    public class UserDto {
        public Guid Id {get;set;}

        public Name name {get;set;}
        public Email email {get;}
        public ICollection<Tag> tags {get;set;}
        public List<Friendship> friendsList { get; set; }

        public LifeDate birthDate { get; set; }

        public PhoneNumber phoneNumber {get;set;}

        public EmotionalState emotionalState {get;set;}

        public EmotionTime EmotionTime {get;set;}

        public UserDto(Guid Id, Name name, Email email, List<Friendship> friendsList, PhoneNumber phoneNumber, LifeDate birthDate, ICollection<Tag> tags, EmotionalState emotionalState,EmotionTime EmotionTime){
            this.Id = Id;
            this.name = name;
            this.friendsList = friendsList;
            this.phoneNumber = phoneNumber;
            this.birthDate = birthDate;
            this.email = email;
            this.tags = tags;
            this.emotionalState = emotionalState;
           this.EmotionTime = EmotionTime;
        }
        
    }
}
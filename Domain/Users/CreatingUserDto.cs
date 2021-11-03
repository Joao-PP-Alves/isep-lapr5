using System.Collections.Generic;

namespace DDDNetCore.Domain.Users{
    public class CreatingUserDto{
        public string name {get;set;}
        public Email email {get;}

        public PhoneNumber phoneNumber {get; set;}
        public List<Tag> tags {get; set;}

        public EmotionalState emotionalState {get;set;}

        public CreatingUserDto(string name, Email email, PhoneNumber phoneNumber, List<Tag> tags, EmotionalState emotionalState){
            this.name = name;
            this.email=email;
            this.phoneNumber = phoneNumber;
            this.tags=tags;
            this.emotionalState = emotionalState;
        }
    }
}
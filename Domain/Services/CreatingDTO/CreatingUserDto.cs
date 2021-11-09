using System.Collections.Generic;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Services.CreatingDTO{
    public class CreatingUserDto{
        public string name {get;set;}
        public Email email {get;}
        public Password password {get;}
        public PhoneNumber phoneNumber {get; set;}
        public List<Tag> tags {get; set;}

        public EmotionalState emotionalState {get;set;}

        public CreatingUserDto(string name, Email email, Password password, PhoneNumber phoneNumber, List<Tag> tags, EmotionalState emotionalState){
            this.name = name;
            this.email=email;
            this.password = new Password(password.Value);
            //this.password = password;
            this.phoneNumber = phoneNumber;
            this.tags=tags;
            this.emotionalState = emotionalState;
        }
    }
}
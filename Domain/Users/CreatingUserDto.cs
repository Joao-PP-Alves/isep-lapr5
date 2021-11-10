using System.Collections.Generic;

namespace DDDNetCore.Domain.Users{
    public class CreatingUserDto{
        public Name name {get;set;}
        public Email email {get;}
        public Password password {get;}
        public PhoneNumber phoneNumber {get; set;}
        public List<Tag> tags {get; set;}

        public EmotionalState emotionalState {get;set;}

        public CreatingUserDto(Name name, Email email, Password password, PhoneNumber phoneNumber, List<Tag> tags, EmotionalState emotionalState){
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
using System;
using System.Collections.Generic;
using DDDNetCore.Domain.Tags;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Services.CreatingDTO{
    public class CreatingUserDto{
        public Name name {get;set;}
        public Email email {get;}
        public Password password {get;}
        public PhoneNumber phoneNumber {get; set;}
        public ICollection<Tag> tags {get; set;}
        
        public LifeDate birthDate { get; set; }

        public CreatingUserDto(Name name, Email email, Password password, PhoneNumber phoneNumber, ICollection<Tag> tags, LifeDate birthDate){
            this.name = name;
            this.email=email;
            this.password = new Password(password.Value);
            this.phoneNumber = phoneNumber;
            this.tags=tags;
            this.birthDate = birthDate;
        }
    }
}
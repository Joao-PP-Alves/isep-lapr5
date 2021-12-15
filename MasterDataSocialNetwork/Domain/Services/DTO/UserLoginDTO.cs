using System;
using System.Collections.Generic;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Services.DTO{
    public class UserLoginDTO {
        public Guid Id {get;set;}
        public Name name {get;set;}
        public Email email {get;}
        public ICollection<Tag> tags {get;set;}

        public LifeDate birthDate { get; set; }

        public PhoneNumber phoneNumber {get;set;}

        public UserLoginDTO(Guid Id, Name name, Email email,  PhoneNumber phoneNumber, LifeDate birthDate, ICollection<Tag> tags){
            this.Id = Id;
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.birthDate = birthDate;
            this.email = email;
            this.tags = tags;
        }
        
    }
}
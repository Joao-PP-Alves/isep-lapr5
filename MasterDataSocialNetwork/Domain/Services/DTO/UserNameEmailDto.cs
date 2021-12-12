using System;
using System.Collections.Generic;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Services.DTO{
    public class UserNameEmailDto {
        public Name name {get;set;}
        public Email email {get;}

        public UserNameEmailDto(Name name, Email email){
            this.name = name;
            this.email = email;
        }
        
    }
}
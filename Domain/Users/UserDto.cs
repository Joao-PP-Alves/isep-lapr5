using System;

namespace DDDNetCore.Domain.Users{
    public class UserDto {
        public Guid Id {get;set;}

        public string name {get;set;}
        public Email email {get;}
    }
}
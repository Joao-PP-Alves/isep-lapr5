using System.Collections.Generic;

namespace DDDNetCore.Domain.Users{
    public class CreatingUserDto{
        public string name {get;set;}
        public Email email {get;}
        public List<Tag> tags {get; set;}

        public CreatingUserDto(string name, Email email, List<Tag> tags){
            this.name = name;
            this.email=email;
            this.tags=tags;
        }
    }
}
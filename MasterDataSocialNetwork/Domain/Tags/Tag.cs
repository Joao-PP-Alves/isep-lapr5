using System;
using System.Collections.Generic;
using System.Drawing;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Tags
{
    public class Tag : Entity<TagId>, IAggregateRoot
    {
        public Name name { get; private set; }
        
        public ICollection<User> usersList { get; set; }

        public bool Active { get; set; }

        public Tag()
        {
            this.Active = true;
        }

        public Tag(Name name)
        {
            if (name != null)
            {
                this.Id = new TagId(new Guid());
                this.name = name;
                this.usersList = new List<User>();
                this.Active = true;
            }else{
                throw new BusinessRuleValidationException("Tag cannot be null.");
            }
        }

        public override string ToString()
        {
            return name.text;
        }
        
        public override bool Equals(Object obj)
        {
            Tag tag = obj as Tag;
            if (tag == null)
                return false;
            else
                return base.Equals((Tag)obj) && name == tag.name;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public void deactivate()
        {
            
        }

        public void ChangeTagName(Name name)
        {
            if (!Active)
            {
                return; 
            }
            if (name == null)
            {
                return; //se o email for nulo, mantém o mesmo
            }
            this.name = name;;
        }
    }
}
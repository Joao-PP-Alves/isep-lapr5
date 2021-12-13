using System;
using System.Drawing;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class Tag : Entity<TagId>
    {
        public Name name { get; private set; }
        
        public bool Active { get; set; }

        public Tag()
        {
            this.Active = true;
        }

        public Tag(Name name)
        {
            if (name != null)
            {
                this.Id = new TagId(Guid.NewGuid());
                this.name = name;
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
    }
}
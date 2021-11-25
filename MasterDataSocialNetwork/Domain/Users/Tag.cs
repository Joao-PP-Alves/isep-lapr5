using System;
using System.Drawing;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class Tag : IValueObject
    {
        public string name { get; set; }

        public Tag()
        {
        }

        public Tag(string name)
        {
            if (name != null)
            {
                this.name = name;
            }
        }

        public override string ToString()
        {
            return name;
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
    }
}
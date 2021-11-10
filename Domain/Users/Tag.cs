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
    }
}
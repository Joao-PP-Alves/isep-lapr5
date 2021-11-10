using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class Name : IValueObject
    {
        public string text { get; set; }

        public Name()
        {
            this.text = "";
        }

        public Name(string text)
        {
            if (text != null)
            {
                this.text = text;
            }
        }

        public override string ToString()
        {
            return text;
        }
    }
}
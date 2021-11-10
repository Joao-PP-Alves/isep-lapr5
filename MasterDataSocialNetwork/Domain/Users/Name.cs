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
                this.text = validateName(text);
            }
        }

        private string validateName(string name){
             if (name != null){
                 string aux = name.Substring(0,1);
                 if (aux.Equals(" ")){
                    throw new BusinessRuleValidationException("The name cannot start with a space.");
                 }
             }
            return name;
        }

        public override string ToString()
        {
            return text;
        }
    }
}
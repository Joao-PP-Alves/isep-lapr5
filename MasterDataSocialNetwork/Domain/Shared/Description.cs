namespace DDDNetCore.Domain.Shared
{
    public class Description : IValueObject
    {
        public string text { get; set; }

        public Description()
        {
            this.text = "";
        }

        public Description(string text)
        {
            if (text != null)
            {
                if (text.Length > 10000){
                    throw new BusinessRuleValidationException("The description cannot be longer than 10000 characters.");
                }
                this.text = text;
            }
            
        }

        public override string ToString()
        {
            return text;
        }
    }
}
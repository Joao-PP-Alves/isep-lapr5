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
                this.text = text;
            }
        }

        public override string ToString()
        {
            return text;
        }
    }
}
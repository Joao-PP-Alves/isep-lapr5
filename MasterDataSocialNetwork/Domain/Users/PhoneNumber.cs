using System.Text.RegularExpressions;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class PhoneNumber : IValueObject
    {
        public string Number { get; set; }

        public PhoneNumber()
        {
        }

        public PhoneNumber(string number)
        {
            if (number == null)
            {
                throw new BusinessRuleValidationException("The number cannot be null or empty.");
            }

            Regex phoneNumberRegex = new Regex("^[0-9]{9}$"); //depois ver como é com indicativos
            Match match = phoneNumberRegex.Match(number);
            if (!match.Success)
            {
                throw new BusinessRuleValidationException("The inserted phone number is not valid.");
            }

            this.Number = number;
        }

        public string toString()
        {
            return Number;
        }
    }
}
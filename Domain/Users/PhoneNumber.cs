using System.Text.RegularExpressions;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class PhoneNumber : IValueObject{
        private string number {get;}

        public PhoneNumber(){}

        public PhoneNumber(string number){
           Regex phoneNumberRegex = new Regex("^[0-9]{9}$");    //depois ver como é com indicativos
           Match match = phoneNumberRegex.Match(number);
           if(!match.Success){
               throw new BusinessRuleValidationException("The inserted phone number is not valid.");
           }
           this.number = number;
        }
    }
}
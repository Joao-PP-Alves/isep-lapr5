using System;
using System.Net.Mail;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users{

    public class Email : IValueObject {
        public string EmailAddress {get; set;}
        public Email(){
        
        }

        public Email (string emailAddress){
            if(emailAddress == null){
                throw new BusinessRuleValidationException("The email address cannot be null or empty.");
            }
            try{
                MailAddress address = new MailAddress(emailAddress);
            } catch (FormatException) {
                throw new BusinessRuleValidationException("Invalid email address!");
            }
            
            this.EmailAddress = emailAddress;
        }

        public string toString(){
            return EmailAddress;
        }

    }
}
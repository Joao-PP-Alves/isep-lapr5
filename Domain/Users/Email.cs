using System;
using System.Net.Mail;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Users{

    public class Email : IValueObject {
        private string EmailAddress {get; set;}

        public Email(){
        
        }

        public Email (string emailAddress){
            if(emailAddress == null){
                throw new BusinessRuleValidationException("The email address cannot be null or empty.");
            }
            try{
                MailAddress address = new MailAddress(emailAddress);
            } catch (FormatException ex) {
                throw new BusinessRuleValidationException("Invalid email address!");
            }

            this.EmailAddress = emailAddress;
        }

    }
}
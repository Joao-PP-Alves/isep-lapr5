using System;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class LifeDate: IValueObject
    {
        public DateTime date { get; set; }
        public int LEGAL_AGE = 16;

        public LifeDate()
        {
            
        }

        public LifeDate(DateTime date)
        {
            if (date == null)
            {
                throw new BusinessRuleValidationException("The date cannot be null or empty.");
            }

            if (validateAge(date))
            {
                this.date = date;
            }
        }
        
        public bool validateAge(DateTime birth)
        {
            DateTime now = DateTime.Now;
            
            int Years = new DateTime(DateTime.Now.Subtract(birth).Ticks).Year - 1;
            if (Years < LEGAL_AGE)
            {
                throw new BusinessRuleValidationException("You must be over 16 years to use this social network.");
            }

            return true;

        }

        
    }
}
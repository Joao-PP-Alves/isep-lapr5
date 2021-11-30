using System;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class LifeDate: IValueObject
    {
        public DateTime date { get; set; }


        public LifeDate()
        {
            
        }

        public LifeDate(DateTime date)
        {
            if (date == null)
            {
                throw new BusinessRuleValidationException("The date cannot be null or empty.");
            }
            date = date;
        }

        
    }
}
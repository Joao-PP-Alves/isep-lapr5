using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class ConnectionStrength
    {
        public float value { get; set; }
        
        public ConnectionStrength(){}

        public ConnectionStrength(float value)
        {
            if (value <= 0.0)
            {
                throw new BusinessRuleValidationException("The relationship strength must be a positive number!");
            }

            this.value = value;
        }
    }
}
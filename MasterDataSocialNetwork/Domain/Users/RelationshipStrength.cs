using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class RelationshipStrength : IValueObject
    {
        public float value { get; set; }

        public RelationshipStrength()
        {
            
        }

        public RelationshipStrength(float value)
        {
            if (value <= 0.0)
            {
                throw new BusinessRuleValidationException("The relationship strength value must be a positive number.");
            }
            this.value = value;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}
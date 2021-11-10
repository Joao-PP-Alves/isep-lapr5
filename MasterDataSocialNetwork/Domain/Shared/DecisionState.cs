namespace DDDNetCore.Domain.Shared
{
    public class DecisionState
    {
        public Decision decision { get; set; }

        public DecisionState(Decision decision)
        {
            this.decision = decision;
        }
        
    }
    
    
}
namespace DDDNetCore.Domain.Shared
{
    public class DecisionState
    {
        public string decision { get; set; }

        public DecisionState(string decision)
        {
            this.decision = decision;
        }

        public override bool Equals(object obj)
        {
            return decision.Equals(obj.ToString());
        }

        public override string ToString(){
            return decision;
        }

        public override int GetHashCode()
        {
            throw new System.NotImplementedException();
        }
    }
    
    
}
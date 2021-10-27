using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Missions
{
    public class Mission : Entity<MissionId>, IAggregateRoot
    {
        //[Required]
        public int dificultyDegree {get; private set;}

        //[Required]
        public Status status {get; private set; }

        public bool Active {get; private set;}

        private Mission()
        {
            this.Active = true;
        }

        private Mission(int dificultyDegree, Status status)
        {
            this.dificultyDegree = dificultyDegree;
            this.status = status;
            this.Active = true;
        }

        private void ChangeDificultyDegree(int dificultyDegree){
            if(!this.Active){
                throw new BusinessRuleValidationException("It is not possible to make changes to an inactive product.");
            }
            this.dificultyDegree = dificultyDegree;
        }
    }
}
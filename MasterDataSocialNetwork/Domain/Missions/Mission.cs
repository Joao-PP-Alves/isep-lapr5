using System;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Missions
{
    public class Mission : Entity<MissionId>, IAggregateRoot
    {
        //[Required]
        public DificultyDegree dificultyDegree { get; private set; }

        public UserId requester { get; private set; }

        //[Required]
        public Status status { get; private set; }

        public bool Active { get; private set; }

        public Mission()
        {
            this.Active = true;
        }

        public Mission(UserId requester,DificultyDegree dificultyDegree)
        {
            this.Id = new MissionId(Guid.NewGuid());
            this.requester = requester;
            this.dificultyDegree = dificultyDegree;
            this.status = Status.IN_PROGRESS;
            this.Active = true;
        }

        public void ChangeDificultyDegree(DificultyDegree dificultyDegree)
        {
            if (!this.Active)
            {
                throw new BusinessRuleValidationException("It is not possible to make changes to an inactive product.");
            }
            this.dificultyDegree = dificultyDegree;
        }

        public void UnsucessMissionStatus(){
            if(this.status != Status.IN_PROGRESS){
                throw new BusinessRuleValidationException("Cannot complete unsucessfully a mission that isn't in progress.");
            }
            this.status = Status.UNSUCCESS;
        }

        public void SucessMissionStatus(){
            if(this.status != Status.IN_PROGRESS){
                throw new BusinessRuleValidationException("Cannot complete sucessfully a mission that isn't in progress.");
            }
            this.status = Status.SUCCESS;
        }

        public void deactivate()
        {
            if (this.Active == false)
            {
                throw new Exception("The Mission is already inactive");
            }
            else
            {
                this.Active = false;
            }
        }
    }
}
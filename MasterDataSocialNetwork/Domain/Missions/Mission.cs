using System;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Missions
{
    public class Mission : Entity<MissionId>, IAggregateRoot
    {
        //[Required]
        public DificultyDegree dificultyDegree { get; private set; }

        //[Required]
        public Status status { get; private set; }

        public bool Active { get; private set; }

        public Mission()
        {
            this.Active = true;
        }

        public Mission(DificultyDegree dificultyDegree, Status status)
        {
            this.Id = new MissionId(Guid.NewGuid());
            this.dificultyDegree = dificultyDegree;
            this.status = status;
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
            this.status = Status.INACTIVE;
        }

        public void deactivate()
        {
            if (this.Active == false)
            {
                throw new Exception("The FriendShip is already inactive");
            }
            else
            {
                this.Active = false;
            }
        }
    }
}
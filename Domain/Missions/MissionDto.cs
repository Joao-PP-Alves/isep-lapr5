using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Missions
{
    public class MissionDto{
        public int dificultyDegree {get; private set;}

        //[Required]
        public Status status {get; private set; }

        public MissionDto(int dificultyDegree, Status status){
            this.dificultyDegree = dificultyDegree;
            this.status = status;
        }
    }
}
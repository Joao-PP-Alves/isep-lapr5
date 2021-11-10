using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Missions
{
    public class CreatingMissionDto
    {
        public MissionId Id {get; set;}
        public DificultyDegree dificultyDegree {get; set;}

        public Status status {get; set;}
        public CreatingMissionDto(MissionId Id, DificultyDegree dificultyDegree, Status status){
            this.Id = Id;
            this.dificultyDegree = dificultyDegree;
            this.status = status;
        }

    }
}
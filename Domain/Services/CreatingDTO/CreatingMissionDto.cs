using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Services.CreatingDTO
{
    public class CreatingMissionDto
    {
        public MissionId Id {get; set;}
        public int dificultyDegree {get; set;}

        public Status status {get; set;}
        public CreatingMissionDto(MissionId Id, int dificultyDegree, Status status){
            this.Id = Id;
            this.dificultyDegree = dificultyDegree;
            this.status = status;
        }

    }
}
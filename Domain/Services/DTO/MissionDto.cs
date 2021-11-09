using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Missions;

namespace DDDNetCore.Domain.Services.DTO
{
    public class MissionDto{

        public MissionId Id {get; set;}
        public int dificultyDegree {get; set;}

        //[Required]
        public Status status {get; set; }

        public MissionDto(MissionId Id, int dificultyDegree, Status status){
            this.Id = Id;
            this.dificultyDegree = dificultyDegree;
            this.status = status;
        }
    }
}
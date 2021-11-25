using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Services.DTO
{
    public class MissionDto{

        public MissionId Id {get; set;}
        public DificultyDegree dificultyDegree {get; set;}
        public UserId requester {get; set;}
        public Status status {get; set; }

        public MissionDto(MissionId Id, DificultyDegree dificultyDegree, Status status,UserId requester){
            this.Id = Id;
            this.dificultyDegree = dificultyDegree;
            this.status = status;
            this.requester = requester;
        }
    }
}
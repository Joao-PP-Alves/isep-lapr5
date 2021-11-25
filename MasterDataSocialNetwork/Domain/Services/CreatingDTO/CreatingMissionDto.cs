using DDDNetCore.Domain.Missions;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Services.CreatingDTO
{
    public class CreatingMissionDto
    {
        public DificultyDegree dificultyDegree {get; set;}
        public UserId requester {get;set;}

        public CreatingMissionDto(DificultyDegree dificultyDegree,UserId requester){
            this.dificultyDegree = dificultyDegree;
            this.requester = requester;

        }

    }
}
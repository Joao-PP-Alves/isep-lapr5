using System;

namespace DDDNetCore.Domain.Services.DTO
{
    public class LeaderboardUserNetworkSizeDto{

        public string userName;
        public int Size {get; set;}
        
        public LeaderboardUserNetworkSizeDto(string userName,int Size){
            this.userName = userName;
            this.Size = Size;
        }
    }
}
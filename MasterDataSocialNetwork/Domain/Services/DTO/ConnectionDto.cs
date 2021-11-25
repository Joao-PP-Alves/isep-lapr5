using DDDNetCore.Domain.Users;
using System;
using System.Security.Cryptography;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Missions;

namespace DDDNetCore.Domain.Services.DTO{

    public class ConnectionDto {

        public Guid id {get; set;}
        public UserId requester {get; set;}

        public UserId targetUser {get; set;}

        public Description description {get; set;}

        public MissionId missionId {get; set;}

        public Decision decision {get; set;}

        public ConnectionDto(Guid id, UserId requester, UserId targetUser, Description description, Decision decision){
            this.id = id;
            this.requester = requester;
            this.targetUser = targetUser;
            this.description = description;
            this.decision = decision;
        }

        public ConnectionDto(Guid id, UserId requester, UserId targetUser, Description description, Decision decision,MissionId missionId){
            this.id = id;
            this.requester = requester;
            this.targetUser = targetUser;
            this.description = description;
            this.decision = decision;
            this.missionId = missionId;
        }

    }
}
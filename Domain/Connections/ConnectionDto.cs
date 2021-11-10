using DDDNetCore.Domain.Users;
using System;
using System.Security.Cryptography;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Connections{

    public class ConnectionDto {

        public Guid id {get; set;}
        public UserId requester {get; set;}

        public UserId targetUser {get; set;}

        public Description description {get; set;}

        public Decision decision {get; set;}

        public ConnectionDto(Guid id, UserId requester, UserId targetUser, Description description, Decision decision){
            this.id = id;
            this.requester = requester;
            this.targetUser = targetUser;
            this.description = description;
            this.decision = decision;
        }

    }
}
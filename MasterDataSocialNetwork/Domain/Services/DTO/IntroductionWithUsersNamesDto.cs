using System;
using DDDNetCore.Domain.Introductions;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Services.DTO
{
    public class IntroductionWithUsersNamesDto{

        public Guid Id {get;set;}
        public String decisionStatus {get;set;}
        public Description MessageToIntermediate{get;set;}
        public Description MessageToTargetUser {get;set;}
        public Description MessageFromIntermediateToTargetUser {get;set;}
        public UserId TargetUser {get;set;}

        public String targetUserName {get;set;}
        public UserId Requester {get;set;}

        public String requesterName {get;set;}
        public UserId Enabler {get;set;}

        public String enablerName {get;set;}

        public IntroductionWithUsersNamesDto(Guid Id,  IntroductionStatus decision,Description messageToIntermediateUser, Description messageToTargetUser, Description messageFromIntermediateToTargetUser,UserId Requester, UserId Enabler, UserId TargetUser,
        String targetUserName,String requesterName,String enablerName){
            this.Id = Id;
            MessageToTargetUser = messageToTargetUser;
            MessageToIntermediate = messageToIntermediateUser;
            MessageFromIntermediateToTargetUser = messageFromIntermediateToTargetUser;
            this.TargetUser = TargetUser;
            this.Requester = Requester;
            this.Enabler = Enabler;
            decisionStatus = decision.ToString();
            this.targetUserName = targetUserName;
            this.requesterName = requesterName;
            this.enablerName = enablerName;
        }

    }
}
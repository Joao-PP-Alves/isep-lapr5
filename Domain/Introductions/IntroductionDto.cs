using System;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Introductions
{
    public class IntroductionDto{

        public Guid Id {get;set;}
        public Decision Decision {get;set;}
        public string Description {get;set;}
        public UserId TargetUser {get;set;}
        public UserId Requester {get;set;}
        public UserId Enabler {get;set;}

        public IntroductionDto(Guid Id, Decision decision, string Description, UserId Requester, UserId Enabler, UserId TargetUser){
            this.Id = Id;
            this.Description = Description;
            this.TargetUser = TargetUser;
            this.Requester = Requester;
            this.Enabler = Enabler;
            this.Decision = decision;
        }
    }
}
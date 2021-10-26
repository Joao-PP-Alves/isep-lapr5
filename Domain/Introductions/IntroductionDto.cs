using System;
using DDDSample1.Domain.Users;

namespace DDDSample1.Domain.Introductions
{
    public class IntroductionDto{

        public Guid Id {get;set;}
        public string Description {get;set;}
        public UserId TargetUser {get;set;}
        public UserId Requester {get;set;}
        public UserId Enabler {get;set;}

        public IntroductionDto(Guid Id, string Description, UserId Requester, UserId Enabler, UserId TargetUser){
            this.Id = Id;
            this.Description = Description;
            this.TargetUser = TargetUser;
            this.Requester = Requester;
            this.Enabler = Enabler;
        }
    }
}
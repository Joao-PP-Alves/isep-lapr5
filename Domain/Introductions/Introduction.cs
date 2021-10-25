using System;
using System.Configuration;
using System.Dynamic;
using DDDSample1.Domain.Shared;
using DDDSample1.Domain.Users;
using Microsoft.Win32;

namespace DDDSample1.Domain.Introduction
{
    public class Introduction : Entity<IntroductionId>, IAggregateRoot
    {
        public Decision Decision {get; private set;}
        public string Description {get; private set;}
        public UserId Resquester {get; private set;}
        public UserId Enabler {get;private set;}
        public UserId TargetUser {get; private set;} 

        public bool Active {get; private set;}

        private Introduction(){
            this.Active = true;
        }

        public Introduction(string Description,UserId Requester, UserId Enabler, UserId TargetUser){
            this.Id = new IntroductionId(Guid.NewGuid());
            this.Decision = Decision.PENDING;
            this.Description = Description;
            this.TargetUser = TargetUser;
            this.Enabler = Enabler;
            this.Resquester = Requester;
            this.Active = true;
        }

        public void AcceptedIntroduction(){
            this.Decision = Decision.ACCEPTED;
        }

        public void DeclinedIntroduction(){
            this.Decision = Decision.DECLINED;
        }

    }
}
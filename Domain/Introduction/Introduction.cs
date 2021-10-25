using System;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Introduction
{
    public class Introduction : Entity<IntroductionId>, IAggregateRoot
    {
        public Decision Decision {get; private set;}

        public string Description {get; private set;}

        private Introduction(){
            this.Decision = Decision.PENDING;
        }

        public Introduction(string Description){
            this.Decision = Decision.PENDING;
            this.Description = Description;
        }

        public void AcceptedIntroduction(){
            this.Decision = Decision.ACCEPTED;
        }

        public void DeclinedIntroduction(){
            this.Decision = Decision.DECLINED;
        }
    }
}
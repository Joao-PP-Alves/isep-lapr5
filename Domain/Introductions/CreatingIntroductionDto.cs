using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Introductions
{
    public class CreatingIntroductionDto
    {
        public string Description {get;set;}
        public UserId Requester {get;set;}
        public UserId Enabler {get;set;}
        public UserId TargetUser {get;set;}

        public CreatingIntroductionDto(string description, UserId requester, UserId enabler, UserId targetUser){
            this.Description = description;
            this.Requester = requester;
            this.Enabler = enabler;
            this.TargetUser = targetUser;
        }

    }
}
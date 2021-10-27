using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users{

    public class Connection : Entity<ConnectionId> {

        private UserId requester;

        private UserId targetUser;

        private string description;

        //private Decision decision;

        private Connection(){

        }

        private Connection(UserId requester, UserId targetUser, string description){
            this.requester = requester;
            this.targetUser = targetUser;
            this.description = description;
            //this.decision = Decision.PENDING;
        }

        public void acceptConnection(){
            //this.decision = Decision.ACCEPTED;
        }

        public void declineConnection(){
            //this.decision = Decision.DECLINED;
        }

    }
}
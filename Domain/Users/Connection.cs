using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users{

    public class Connection : Entity<ConnectionId> {

        public UserId requester;

        public UserId targetUser;

        public string description;

        public Decision decision;

        private Connection(){

        }

        public Connection(UserId requester, UserId targetUser, string description){
            this.requester = requester;
            this.targetUser = targetUser;
            this.description = description;
            this.decision = Decision.PENDING;
        }

        public void acceptConnection(){
            this.decision = Decision.ACCEPTED;
        }

        public void declineConnection(){
            this.decision = Decision.DECLINED;
        }

    }
}
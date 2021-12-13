namespace DDDNetCore.Domain.Services.DTO
{
    public class NetworkNSizeDTO{

        public string UserEmail {get; set;}
        public int N {get; set;}
        
        public NetworkNSizeDTO(string UserEmail, int N){
            this.N = N;
            this.UserEmail = UserEmail;
        }
    }
}
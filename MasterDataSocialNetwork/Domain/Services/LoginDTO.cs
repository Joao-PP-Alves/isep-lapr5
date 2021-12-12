using System;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Domain.Services
{
    public class LoginDTO
    {
        public Email email {get;}

        public Password password {get;}

        public LoginDTO(Email email, Password password)
        {
            this.email = email ?? throw new ArgumentNullException(nameof(email));
            this.password = password ?? throw new ArgumentNullException(nameof(password));
        }
    }
}
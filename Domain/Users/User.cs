using System;
using System.Collections.Generic;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Shared;
using System.Configuration;
using System.Text.RegularExpressions;

namespace DDDNetCore.Domain.Users
{
    //[Table("User")]
    public class User : Entity<UserId>, IAggregateRoot
    {
        // [Required]
        public string Name { get; set; }

        // [Required]
        public Email Email { get; set; }

        // [Required]
        public DateTime Date { get; set; }

        // [Required]
        public PhoneNumber PhoneNumber { get; set; }

        public List<Tag> tags { get; set; }

        public EmotionalState emotionalState { get; set; }


        public bool Active { get; set; }

        //private HyperLink facebook;
        //private Hyperlink linkedin;

        public User()
        {
            this.Active = true;
        }

        public User(string name, Email email, DateTime date, PhoneNumber phoneNumber, List<Tag> tags, EmotionalState emotionalState)
        {
            this.Id = new UserId(Guid.NewGuid());
            this.Name = name;
            this.Date = date;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.tags = tags;
            this.emotionalState = emotionalState;
            this.Active = true;
        }

        public User(string name, Email email, PhoneNumber phoneNumber, List<Tag> tags, EmotionalState emotionalState)
        {
            this.Id = new UserId(Guid.NewGuid());
            this.Name = validateName(name);
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.tags = tags;
            this.emotionalState = emotionalState;
            this.Active = true;
        }

        private string validateName(string name){
            if (name != null){
                string aux = name.Substring(0,1);
                if (aux == " "){
                    throw new BusinessRuleValidationException("The name cannot start with a space.");
                }
            }
            return name;
        }
        
        private string validatePassword(string password){
            Regex regex = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            if (!regex.IsMatch(password)){
                throw new BusinessRuleValidationException("The password must contain at least one upper case and lower case letters, one number, one special char and it must be exacly 8 chars long.");
            }
            return password;
        }


        public void ChangeName(string name)
        {
            if (!this.Active)
            {
                return; 
            }
            if(name == null){
                return; //se o nome for nulo, mantém o mesmo
            }
            this.Name = name;
        }

        public void ChangeEmail(Email email)
        {
            if (!this.Active)
            {
                return; 
            }
            if (email == null)
            {
                return; //se o email for nulo, mantém o mesmo
            }
            this.Email = email;
        }


        public void ChangeTags(List<Tag> tags)
        {
            if (!this.Active)
            {
                return; 
            }
            if (tags == null)
            {
                return; //se a lista for nula, mantém a mesma
            }
            this.tags = tags;
        }

        public void ChangePhoneNumber(PhoneNumber phoneNumber)
        {
            if (!this.Active)
            {
                return; 
            }
            if (phoneNumber == null)
            {
                return; //se o telefone for nulo, mantém o mesmo
            }
            this.PhoneNumber = phoneNumber;
        }

        public void ChangeEmotionalState(EmotionalState emotionalState)
        {
            if (!this.Active)
            {
                return; 
            }
            // (Davide) Esta exceção não faz muito sentido. Apaguei o construtor vazio de estado emocional visto que não há nenhuma emoçao default para um construtor sem parametros
            // Sendo assim o estado emocional terá sempre obrigatoriamente uma emoção agarrada
             if (emotionalState == null)
            {
                return; //se o emotional state for nulo, mantém o mesmo
            } 
            this.emotionalState = emotionalState;
        }

        public void MarkAsInative()
        {
            if (this.Active)
            {
                throw new BusinessRuleValidationException("It is not possible to delete an active user.");
            }
            else
            {
                this.Active = false;
            }
        }

    }
}
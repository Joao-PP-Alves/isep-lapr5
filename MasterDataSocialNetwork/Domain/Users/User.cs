using System;
using System.Collections.Generic;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Shared;
using System.Configuration;
using System.Text.RegularExpressions;
using DDDNetCore.Domain.Services.DTO;

namespace DDDNetCore.Domain.Users
{
    //[Table("User")]
    public class User : Entity<UserId>, IAggregateRoot
    {
        // [Required]
        // Name
        public Name Name { get; set; }

        // [Required]
        public Email Email { get; set; }

        // [Required]
        public Password Password {get; set;}

        // [Required]
        public DateTime Date { get; set; }

        // [Required]
        public PhoneNumber PhoneNumber { get; set; }

        public List<Tag> tags { get; set; }

        public EmotionalState emotionalState { get; set; }

        public List<Friendship> friendsList { get; set; }
        //public EmotionTime EmotionTime {get; private set;} 
        public bool Active { get; set; }

        //private HyperLink facebook;
        //private Hyperlink linkedin;

        public User()
        {
            this.Active = true;
        }

        public User(Name name, Email email, List<Friendship> friendsList, Password password, DateTime date, PhoneNumber phoneNumber, List<Tag> tags, EmotionalState emotionalState /*EmotionTime EmotionTime*/)
        {
            this.Id = new UserId(Guid.NewGuid());
            this.Name = name;
            this.friendsList = friendsList;
            this.Date = date;
            this.Email = email;
            this.Password = password;
            this.PhoneNumber = phoneNumber;
            this.tags = tags;
            this.emotionalState = emotionalState;
           // this.EmotionTime = EmotionTime;
            this.Active = true;
        }

        public User(Name name, Email email, Password password, PhoneNumber phoneNumber, List<Tag> tags, EmotionalState emotionalState/*,EmotionTime EmotionTime*/)
        {
            this.Id = new UserId(Guid.NewGuid());
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.PhoneNumber = phoneNumber;
            this.tags = tags;
            this.emotionalState = emotionalState;
          //  this.EmotionTime = EmotionTime;
            this.Active = true;
        }

        


        public void ChangeName(Name name)
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

        public void AddFriendship(Friendship newFriendship)
        {
            this.friendsList.Add(newFriendship);
        }

        public void RemoveFriendship(Friendship friendship)
        {
            this.friendsList.Remove(friendship);
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
            //this.EmotionTime = new EmotionTime(DateTime.UtcNow);
        }


        public void ChangePassword(Password newPassword)
        {
            if(!this.Active) return;


            if (newPassword == null)
            {
               return;
            }
             this.Password = newPassword;
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

        //
        // public void updateEmotionTime(EmotionTime time){
        //     this.EmotionTime = time;
        // }

    }
}
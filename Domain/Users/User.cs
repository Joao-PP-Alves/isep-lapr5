using System;
using System.Collections.Generic;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Shared;
using System.Configuration;

namespace DDDNetCore.Domain.Users
{
    //[Table("User")]
    public class User : Entity<UserId>, IAggregateRoot
    {
        // [Required]
        public string Name { get; set; }

        // [Required]
        public Email Email { get; }

        // [Required]
        public DateTime Date { get; }

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
            this.Email = new Email(email.ToString());
            this.PhoneNumber = new PhoneNumber(phoneNumber.ToString());
            this.tags = tags;
            this.emotionalState = emotionalState;
            this.Active = true;
        }

        public User(string name, Email email, List<Tag> tags, EmotionalState emotionalState)
        {
            this.Id = new UserId(Guid.NewGuid());
            this.Name = name;
            this.Email = new Email(email.ToString());
            this.tags = tags;
            this.emotionalState = emotionalState;
        }

        public void ChangeName(string name)
        {
            if (!this.Active)
            {
                throw new BusinessRuleValidationException("It is not possible to change the description to an inactive product.");
            }
            this.Name = name;
        }

        public void ChangeTags(List<Tag> tags)
        {
            if (tags == null)
            {
                throw new Exception("The new tags list can't be empty.");
            }
            this.tags = tags;
        }

        public void ChangePhoneNumber(PhoneNumber phoneNumber)
        {
            if (phoneNumber == null)
            {
                throw new Exception("The phone number cannot be null.");
            }
            this.PhoneNumber = phoneNumber;
        }

        public void ChangeEmotionalState(EmotionalState emotionalState)
        {
            // (Davide) Esta exceção não faz muito sentido. Apaguei o construtor vazio de estado emocional visto que não há nenhuma emoçao default para um construtor sem parametros
            // Sendo assim o estado emocional terá sempre obrigatoriamente uma emoção agarrada
            /* if (emotionalState.emotion == null)
            {
                throw new Exception("The emotional state must contain an emotion attached.");
            } */
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
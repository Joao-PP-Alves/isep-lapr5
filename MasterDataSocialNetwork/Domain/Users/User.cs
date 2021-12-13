using System;
using System.Collections.Generic;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Tags;

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

        public ICollection<Tag> tags { get; set; }

        public EmotionalState emotionalState { get; set; }
        
        public LifeDate BirthDate { get; set; }

        public List<Friendship> friendsList { get; set; }
        public EmotionTime EmotionTime {get; private set;} 
        public bool Active { get; set; }

        //private HyperLink facebook;
        //private Hyperlink linkedin;

        public User()
        {
            Active = true;
        }

        public User(Name name, Email email, Password password, PhoneNumber phoneNumber, LifeDate birthDate, ICollection<Tag> tags, EmotionalState emotionalState,EmotionTime EmotionTime)
        {
            Id = new UserId(Guid.NewGuid());
            Name = name;
            friendsList = friendsList;
            Email = email;
            Password = password;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            this.tags = tags;
            this.emotionalState = emotionalState;
            this.EmotionTime = EmotionTime;
            Active = true;
        }

        public User(Name name, Email email, List<Friendship> friendsList, Password password, LifeDate birthDate, PhoneNumber phoneNumber, ICollection<Tag> tags, EmotionalState emotionalState,EmotionTime EmotionTime)
        {
            Id = new UserId(Guid.NewGuid());
            Name = name;
            Email = email;
            this.friendsList = friendsList;
            Password = password;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            this.tags = tags;
            this.emotionalState = emotionalState;
            this.EmotionTime = EmotionTime;
            Active = true;
        }

        public User(Name name, Email email,  Password password, PhoneNumber phoneNumber, LifeDate birthDate, ICollection<Tag> tags)
        {
            Id = new UserId(Guid.NewGuid());
            Name = name;
            Email = email;
            friendsList = new List<Friendship>();
            Password = password;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            this.tags = tags;
            emotionalState = new EmotionalState(Emotion.esperança);
            EmotionTime = new EmotionTime(DateTime.Now);
            Active = true;
        }


        public void ChangeName(Name name)
        {
            if (!Active)
            {
                return; 
            }
            if(name == null){
                return; //se o nome for nulo, mantém o mesmo
            }
            Name = name;
        }

        public void ChangeEmail(Email email)
        {
            if (!Active)
            {
                return; 
            }
            if (email == null)
            {
                return; //se o email for nulo, mantém o mesmo
            }
            Email = email;
        }


        public void ChangeTags(ICollection<Tag> tags)
        {
            if (!Active)
            {
                return; 
            }
            if (tags == null)
            {
                return; //se a lista for nula, mantém a mesma
            }
            this.tags = tags;
        }

        public void updateFriendShips(IList<Friendship> friendships)
        {
            if (friendships.Count == 0) return;
            foreach (var friendship in friendships)
            {
                if (!friendsList.Contains(friendship))
                {
                    AddFriendship(friendship);
                }
            }
        }

        public void RemoveFriendship(Friendship friendship)
        {
            friendsList.Remove(friendship);
        }

        public void ChangePhoneNumber(PhoneNumber phoneNumber)
        {
            if (!Active)
            {
                return; 
            }
            if (phoneNumber == null)
            {
                return; //se o telefone for nulo, mantém o mesmo
            }
            PhoneNumber = phoneNumber;
        }

        public void ChangeEmotionalState(EmotionalState emotionalState)
        {
            if (!Active)
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
            EmotionTime = new EmotionTime(DateTime.UtcNow);
        }


        public void ChangePassword(Password newPassword)
        {
            if(!Active) return;


            if (newPassword == null)
            {
               return;
            }
             Password = newPassword;
        }

        public void MarkAsInative()
        {
            if (Active)
            {
                throw new BusinessRuleValidationException("It is not possible to delete an active user.");
            }

            Active = false;
        }

        
        public void updateEmotionTime(EmotionTime time){
            EmotionTime = time;
        }

        public void AddFriendship(Friendship friendship)
        {
            if (friendsList == null)
            {
                friendsList = new List<Friendship>();
            }

            if (friendship == null)
            {
                throw new Exception("The friendship is invalid!");
            }
            friendsList.Add(friendship);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (GetType().Name != obj.GetType().Name)
            {
                return false;
            }

            var u = (User) obj;

            if (Id == u.Id)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
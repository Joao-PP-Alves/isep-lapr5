using System;
using System.Collections.Generic;
using DDDNetCore.Domain.Users;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    //[Table("User")]
    public class User : Entity<UserId>, IAggregateRoot
    {
       // [Required]
        private string Name {get; set;}

       // [Required]
        private Email Email {get;}

       // [Required]
        private DateTime Date {get;}

       // [Required]
        private PhoneNumber PhoneNumber {get; set;}

        private List<Tag> tags {get; set;}

        private EmotionalState emotionalState {get;set;}
        
        private bool Active {get; set;}

        //private HyperLink facebook;
        //private Hyperlink linkedin;

        public User(){
            this.Active = true;
        }

        public User(string name, Email email, DateTime date, PhoneNumber phoneNumber, List<Tag> tags, EmotionalState emotionalState){
            this.Id  = new UserId(Guid.NewGuid());
            this.Name = name;
            this.Date = date;
            this.PhoneNumber = phoneNumber;
            this.tags = tags;
            this.emotionalState = emotionalState;
            this.Active = true;
        }

        public void ChangeName(string name){
            if(!this.Active){
                throw new BusinessRuleValidationException("It is not possible to change the description to an inactive product.");
            }
            this.Name = name;
        } 

        public void ChangeTags(List<Tag> tags){
            if(tags != null){
                throw new Exception("The new tags list can't be empty.");
            } 
            this.tags = tags;
        }

        public void MarkAsInative(){
            this.Active = false;
        }

    }
}
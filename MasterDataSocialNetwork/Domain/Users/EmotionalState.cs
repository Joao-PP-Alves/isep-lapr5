using System;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class EmotionalState : IValueObject{
        public Emotion emotion {get; set;}

       

        public EmotionalState(Emotion emotion){
            try{
                this.emotion = emotion;
            } catch (Exception) {
                    throw new Exception("The inserted emotion does not exist.");
            }
        }
    }
}
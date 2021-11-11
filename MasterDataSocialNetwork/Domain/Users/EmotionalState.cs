using System;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class EmotionalState : IValueObject{
        public Emotion emotion {get; set;}

        public DateTime Time {get;set;}

        public TimeSpan TimeElapsed {get;set;}

        public EmotionalState(Emotion emotion){
            try{
                this.emotion = emotion;
                this.Time = DateTime.UtcNow;
            } catch (Exception) {
                    throw new Exception("The inserted emotion does not exist.");
            }
        }

        public static void updateElapsedTime(EmotionalState emotionalState){
            //emotionalState.TimeElapsed = DateTime.UtcNow - emotionalState.Time;
        }
    }
}
using System;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class EmotionTime : IValueObject{
        public DateTime LastEmotionalUpdate {get; set;}

        public TimeSpan Time {get;}

        public EmotionTime (DateTime date){
            if (date == null){
                this.LastEmotionalUpdate = DateTime.UtcNow;    
            } else {
                this.LastEmotionalUpdate = date;
            }
            Time = DateTime.UtcNow - date;            
        }

        public EmotionTime(){
            this.LastEmotionalUpdate = DateTime.UtcNow;
            this.Time = DateTime.UtcNow - this.LastEmotionalUpdate;            
        }
    }
}
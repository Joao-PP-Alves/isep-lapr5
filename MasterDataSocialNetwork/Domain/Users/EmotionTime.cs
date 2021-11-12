using System;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class EmotionTime : IValueObject{
        public DateTime LastEmotionalUpdate {get; set;}

        public TimeSpan Time {get;set;}

        public EmotionTime (DateTime Date){
            if (Date == null){
                this.LastEmotionalUpdate = DateTime.UtcNow;    
            } else {
                this.LastEmotionalUpdate = Date;
            }
            this.Time = DateTime.UtcNow - Date;            
        }
    }
}
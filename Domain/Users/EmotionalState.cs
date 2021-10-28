using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Users
{
    public class EmotionalState : IValueObject{
        public Emotion emotion {get; set;}

        public EmotionalState(){}

        public EmotionalState(Emotion emotion){
            this.emotion = emotion;
        }
    }
}
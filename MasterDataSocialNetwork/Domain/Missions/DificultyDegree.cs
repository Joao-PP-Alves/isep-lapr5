using System;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Missions
{
    public class DificultyDegree : IValueObject
    {
        public Level level { get; set;}

        public DificultyDegree(Level level)
        {
            try{
                this.level = level;
            } catch (Exception) {
                throw new Exception("The inserted level does not exist.");
            }
        }
    }
}
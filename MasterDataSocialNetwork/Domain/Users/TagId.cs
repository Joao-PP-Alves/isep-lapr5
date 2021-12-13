using System;
using DDDNetCore.Domain.Shared;
using Newtonsoft.Json;

namespace DDDNetCore.Domain.Users
{
    public class TagId : EntityId
    {
        [JsonConstructor]
        public TagId(Guid value) : base(value){}

        public TagId(string value) : base(value){
        }

        override
            protected  Object createFromString(String text){
            return new Guid(text);
        }

        override
            public String AsString(){
            Guid obj = (Guid) base.ObjValue;
            return obj.ToString();
        }
        
       
        public Guid AsGuid(){
            return (Guid) base.ObjValue;
        }
    }
}
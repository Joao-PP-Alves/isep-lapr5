using System;
using DDDNetCore.Domain.Shared;
using Newtonsoft.Json;

namespace DDDNetCore.Domain.Users{

    public class ConnectionId : EntityId {
        [JsonConstructor]
        public ConnectionId(Guid value) : base(value)
        {
        }

        public ConnectionId(String value) : base(value){
        }

        override
        public string AsString()
        {
            Guid obj = (Guid) base.ObjValue;
            return obj.ToString();
        }

        override
        protected object createFromString(string text)
        {
            return new Guid(text);
        }
    }
}
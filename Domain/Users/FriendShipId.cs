using System;
using DDDNetCore.Domain.Shared;
using Newtonsoft.Json;

namespace DDDNetCore.Domain.Users {
    public class FriendShipId : EntityId
    {
        [JsonConstructor]
        public FriendShipId(Guid value) : base(value)
        {
        }

        public FriendShipId(String value) : base(value){
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
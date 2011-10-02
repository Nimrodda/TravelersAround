using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using TravelersAround.DataContracts.Views;

namespace TravelersAround.DataContracts
{
    [DataContract]
    public class LoginResponse : ResponseBase
    {
        [DataMember]
        public string APIKey { get; set; }
        [DataMember]
        public int NewMessagesCount { get; set; }
        [DataMember]
        public TravelerView Profile { get; set; }
    }
}

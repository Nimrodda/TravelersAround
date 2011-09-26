using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TravelersAround.DataContracts
{
    [DataContract]
    public class TickerResponse : ResponseBase
    {
        [DataMember]
        public int NewMessagesCount { get; set; }
    }
}

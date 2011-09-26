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

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public double Latitude { get; set; }

        [DataMember]
        public double Longtitude { get; set; }
    }
}

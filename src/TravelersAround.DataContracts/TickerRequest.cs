using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TravelersAround.DataContracts
{
    [DataContract]
    public class TickerRequest
    {
        [DataMember]
        public string IPAddress { get; set; }
    }
}

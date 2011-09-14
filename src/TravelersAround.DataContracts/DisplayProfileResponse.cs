using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.DataContracts.Views;
using System.Runtime.Serialization;

namespace TravelersAround.DataContracts
{
    [DataContract]
    public class DisplayProfileResponse : ResponseBase
    {
        [DataMember]
        public TravelerView Profile { get; set; }
    }
}

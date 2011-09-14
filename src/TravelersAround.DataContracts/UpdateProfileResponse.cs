using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using TravelersAround.DataContracts.Views;

namespace TravelersAround.DataContracts
{
    [DataContract]
    public class UpdateProfileResponse : ResponseBase
    {
        [DataMember]
        public TravelerView Profile { get; set; }
    }
}

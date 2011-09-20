using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using TravelersAround.DataContracts.Views;

namespace TravelersAround.DataContracts
{
    [DataContract]
    public class UpdateProfileRequest
    {
        [DataMember]
        public string Firstname { get; set; }
        [DataMember]
        public string Lastname { get; set; }
        [DataMember]
        public string Birthdate { get; set; }
        [DataMember]
        public string StatusMessage { get; set; }
        [DataMember]
        public bool IsAvailable { get; set; }
        [DataMember]
        public string Gender { get; set; }
    }
}

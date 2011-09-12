using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace TravelersAround.HTTPHost
{
    [DataContract]
    public class APIKeyResponse
    {
        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public bool Success { get; set; }
    }
}
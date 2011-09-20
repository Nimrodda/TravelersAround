using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TravelersAround.DataContracts
{
    [DataContract]
    public class AddFriendRequest
    {
        [DataMember]
        public string FriendID { get; set; }
    }
}

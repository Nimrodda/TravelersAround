using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TravelersAround.DataContracts.Views
{
    [DataContract]
    public class MessageView
    {
        [DataMember]
        public string MessageID { get; set; }
        [DataMember]
        public string Subject { get; set; }
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public string SentDate { get; set; }
        [DataMember]
        public string SenderName { get; set; }
        [DataMember]
        public IList<string> RecipientsNames { get; set; }
        [DataMember]
        public bool IsRead { get; set; }

    }
}

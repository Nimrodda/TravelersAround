using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace TravelersAround.DataContracts
{
    [DataContract]
    public class TickerRequest
    {
        [DataMember]
        [RegexValidator(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$", 
            MessageTemplateResourceType = typeof(R.String.ErrorMessages),
            MessageTemplateResourceName = "InvalidIPAddress")]
        public string IPAddress { get; set; }
    }
}

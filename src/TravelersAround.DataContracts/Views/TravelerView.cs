﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TravelersAround.DataContracts.Views
{
    [DataContract]
    public class TravelerView
    {
        [DataMember]
        public Guid TravelerID { get; internal set; }
        [DataMember]
        public string Firstname { get; set; }
        [DataMember]
        public string Lastname { get; set; }
        [DataMember]
        public DateTime Birthdate { get; set; }
        [DataMember]
        public string StatusMessage { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public double Longtitude { get; set; }
        [DataMember]
        public double Latitude { get; set; }
        [DataMember]
        public bool IsAvailable { get; set; }
    }
}
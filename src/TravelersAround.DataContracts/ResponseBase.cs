﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TravelersAround.DataContracts
{
    [DataContract]
    public abstract class ResponseBase
    {
        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }

        public ResponseBase()
        {
            Success = false;
            ErrorMessage = "An error has occured while processing your request";
        }

        public void MarkSuccess()
        {
            Success = true;
            ErrorMessage = null;
        }
    }

}

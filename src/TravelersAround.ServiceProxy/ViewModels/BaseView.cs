using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.DataContracts.Views;

namespace TravelersAround.ServiceProxy.ViewModels
{
    public abstract class BaseView
    {
        public string ResponseMessage { get; internal set; }
        public bool Success { get; internal set; }
        public int Page { get; set; }
    }
}

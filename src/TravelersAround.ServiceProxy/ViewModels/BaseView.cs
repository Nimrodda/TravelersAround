using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelersAround.ServiceProxy.ViewModels
{
    public abstract class BaseView
    {
        public string ErrorMessage { get; internal set; }
        public bool Success { get; internal set; }
        public int Page { get; set; }
    }
}

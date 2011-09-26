using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelersAround.ServiceProxy.ViewModels
{
    public class TickerModel : BaseView
    {
        public int NewMessagesCount { get; set; }
        public string IPAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
    }
}

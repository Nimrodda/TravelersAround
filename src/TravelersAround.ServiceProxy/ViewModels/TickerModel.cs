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
    }
}

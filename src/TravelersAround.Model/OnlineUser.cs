using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelersAround.Model
{
    public class OnlineUser
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime Expiration { get; set; }
        public bool IsOnline { get; set; }
    }
}

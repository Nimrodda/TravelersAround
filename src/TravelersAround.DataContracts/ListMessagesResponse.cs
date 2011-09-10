using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.DataContracts.Views;

namespace TravelersAround.DataContracts
{
    public class ListMessagesResponse : ResponseBase
    {
        public IList<MessageView> MessagesList { get; set; }
    }
}


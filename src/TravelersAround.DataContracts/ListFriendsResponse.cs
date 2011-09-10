using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.DataContracts.Views;

namespace TravelersAround.DataContracts
{
    public class ListFriendsResponse : ResponseBase
    {
        public IList<TravelerView>  FriendsList { get; set; }
    }
}

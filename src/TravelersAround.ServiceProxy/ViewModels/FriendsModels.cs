using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.DataContracts.Views;
using TravelersAround.Infrastructure;

namespace TravelersAround.ServiceProxy.ViewModels
{
    public class FriendsListView : BaseView
    {
        public PagedList<TravelerView> FriendsList { get; set; }

        public int index { get; set; }

        public int count { get; set; }
    }
}

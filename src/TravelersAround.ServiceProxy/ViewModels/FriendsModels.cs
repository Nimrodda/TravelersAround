using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.DataContracts.Views;

namespace TravelersAround.ServiceProxy.ViewModels
{
    public class FriendsListView : BaseView
    {
        public IList<TravelerView> Friends { get; internal set; }

        public int index { get; set; }

        public int count { get; set; }
    }
}

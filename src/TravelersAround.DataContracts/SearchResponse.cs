using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.DataContracts.Views;

namespace TravelersAround.DataContracts
{
    public class SearchResponse : ResponseBase
    {
        public IList<TravelerView> Travelers { get; set; }
    }
}

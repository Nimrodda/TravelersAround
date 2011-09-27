using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TravelersAround.DataContracts.Views;
using TravelersAround.Infrastructure;

namespace TravelersAround.ServiceProxy.ViewModels
{
    public class SearchView : BaseView
    {
        public PagedList<TravelerView> Travelers { get; set; }
        public bool IncludeOfflineTravelers { get; set; }
        public string IPAddress { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
    }
}

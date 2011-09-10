using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelersAround.Model
{
    public struct GeoCoordinates
    {
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}

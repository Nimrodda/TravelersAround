using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Model.Entities;

namespace TravelersAround.Model
{
    public interface ILocationDeterminator
    {
        IEnumerable<Traveler> FindNearByTravelers(int distance, double travelerLatitude, double travelerLongtitude);
    }
}

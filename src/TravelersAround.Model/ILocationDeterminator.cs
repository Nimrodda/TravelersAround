using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Model.Entities;
using TravelersAround.Infrastructure;

namespace TravelersAround.Model
{
    public interface ILocationDeterminator
    {
        PagedList<Traveler> FindNearByTravelers(int distance, double travelerLatitude, double travelerLongtitude, int index, int count, Guid travelerID);
    }
}

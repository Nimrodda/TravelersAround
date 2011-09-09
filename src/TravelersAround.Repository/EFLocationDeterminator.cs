using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Model;
using TravelersAround.Model.Entities;
using System.Data.Objects;

namespace TravelersAround.Repository
{
    public class EFLocationDeterminator : ILocationDeterminator
    {
        public IEnumerable<Traveler> FindNearByTravelers(int distance, double travelerLatitude, double travelerLongtitude)
        {
            TravelersAroundEntities dataContext = new TravelersAroundEntities();
            using (dataContext)
            {
                return dataContext.FindNearByTravelers(distance, travelerLatitude, travelerLongtitude).ToList();
            }
        }
    }
}

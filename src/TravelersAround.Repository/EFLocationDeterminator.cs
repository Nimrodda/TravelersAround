using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Model;
using TravelersAround.Model.Entities;
using TravelersAround.Infrastructure;
using System.Data.Objects;

namespace TravelersAround.Repository
{
    public class EFLocationDeterminator : ILocationDeterminator
    {
        public PagedList<Traveler> FindNearByTravelers(int distance, double travelerLatitude, double travelerLongtitude, int index, int count, Guid travelerID)
        {
            TravelersAroundEntities dataContext = new TravelersAroundEntities();
            using (dataContext)
            {
                return dataContext.FindNearByTravelers(distance, travelerLatitude, travelerLongtitude, travelerID).ToList().ToPagedList(index, count);
            }
        }
    }
}

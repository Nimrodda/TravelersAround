using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Model.Entities;

namespace TravelersAround.Model.Factories
{
    public static class TravelerFactory
    {
        public static Traveler CreateTraveler(Guid id, string firstname, string lastname, string birthdate, string gender, double latitude, double longtitude)
        {
            return new Traveler
            {
                TravelerID = id,
                Firstname = firstname,
                Lastname = lastname,
                Gender = gender,
                Birthdate = DateTime.Parse(birthdate),
                Latitude = latitude,
                Longtitude = longtitude
            };
        }
    }
}

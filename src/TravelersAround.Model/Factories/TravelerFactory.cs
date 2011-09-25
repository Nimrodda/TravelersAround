using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Model.Entities;

namespace TravelersAround.Model.Factories
{
    public static class TravelerFactory
    {
        public static Traveler CreateTraveler(Guid id, 
                                            string firstname, 
                                            string lastname, 
                                            DateTime birthdate, 
                                            string gender,
                                            double latitude, 
                                            double longtitude, 
                                            string apiKey, 
                                            string city, 
                                            string country)
        {
            return new Traveler
            {
                TravelerID = id,
                Firstname = firstname,
                Lastname = lastname,
                Gender = gender,
                Birthdate = birthdate,
                Latitude = latitude,
                Longtitude = longtitude,
                ApiKey = apiKey,
                City = city,
                Country = country
            };
        }
    }
}

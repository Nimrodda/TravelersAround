using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.DataContracts.Views;
using TravelersAround.Model.Entities;

namespace TravelersAround.Service.Mappers
{
    public static class TravelerExtensions
    {
        public static TravelerView ConvertToTravelerView(this Traveler traveler)
        {
            return new TravelerView
            {
                TravelerID = traveler.TravelerID.ToString(),
                Birthdate = traveler.Birthdate.ToString(),
                StatusMessage = traveler.StatusMessage,
                Firstname = traveler.Firstname,
                Lastname = traveler.Lastname,
                Gender = traveler.Gender,
                IsAvailable = traveler.IsAvailable,
                Latitude = traveler.Latitude,
                Longtitude = traveler.Longtitude,
                IsOnline = traveler.IsOnline,
                City = traveler.City,
                Country = traveler.Country
                
            };
        }

        public static IList<TravelerView> ConvertToTravelerViewList(this IEnumerable<Traveler> travelers)
        {
            IList<TravelerView> travelerViewList = new List<TravelerView>();
            if (travelers != null)
            {
                foreach (Traveler traveler in travelers)
                {
                    travelerViewList.Add(traveler.ConvertToTravelerView());
                }
            }
            return travelerViewList;
        }
    }
}

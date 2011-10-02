using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.DataContracts.Views;
using TravelersAround.Model.Entities;
using TravelersAround.DataContracts;
using TravelersAround.Infrastructure;

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

        public static PagedList<TravelerView> ConvertToTravelerViewList(this PagedList<Traveler> travelers)
        {
            PagedList<TravelerView> travelersViewList = new PagedList<TravelerView>();
            travelersViewList.HasNext = travelers.HasNext;
            travelersViewList.HasPrevious = travelers.HasPrevious;
            travelersViewList.TotalEntitiesCount = travelers.TotalEntitiesCount;
            travelersViewList.TotalPageCount = travelers.TotalPageCount;
            if (travelers != null)
            {
                foreach (Traveler traveler in travelers.Entities)
                {
                    travelersViewList.Entities.Add(traveler.ConvertToTravelerView());
                }
            }
            return travelersViewList;
        }
    }
}

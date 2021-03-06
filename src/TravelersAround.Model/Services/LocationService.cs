﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Model.Entities;
using TravelersAround.Model.Exceptions;
using TravelersAround.Infrastructure;

namespace TravelersAround.Model.Services
{
    public class LocationService
    {
        private ILocationDeterminator _locationDeterminator;
        private IRepository _repository;
        private IGeoCoder _geoCoder;

        public LocationService(ILocationDeterminator locationDeterminator, 
                            IRepository repository,
                            IGeoCoder geoCoder)
        {
            _locationDeterminator = locationDeterminator;
            _repository = repository;
            _geoCoder = geoCoder;
        }

        public GeoCoordinates GetCurrentLocationWithIPAddress(string ipAddress)
        {
            return _geoCoder.ConvertIPAddressToGeoCoordinates(ipAddress);
        }

        public GeoCoordinates GetCurrentLocationWithStreetAddress(string streetAddress)
        {
            return _geoCoder.ConvertStreetAddressToGeoCoordinates(streetAddress);
        }

        public PagedList<Traveler> GetListOfTravelersWithin(int kmDistance, int index, int count, Traveler traveler)
        {
            if (traveler.Latitude > 0 && traveler.Longtitude > 0)
                return _locationDeterminator.FindNearByTravelers(kmDistance, traveler.Latitude, traveler.Longtitude, index, count, traveler.TravelerID);

            else
                throw new InvalidTravelerLocationException();

        }

        public void UpdateTravelerCoordinates(Traveler traveler, string ipAddress)
        {
            if (String.IsNullOrEmpty(ipAddress)) return;

            GeoCoordinates travelerGeoCoords = GetCurrentLocationWithIPAddress(ipAddress);

            //Updating traveler's location only if it's different than what's stored in the database
            if (travelerGeoCoords.IsValid() && traveler.IsLocationChanged(travelerGeoCoords))
            {
                traveler.Latitude = travelerGeoCoords.Latitude;
                traveler.Longtitude = travelerGeoCoords.Longtitude;
                traveler.City = travelerGeoCoords.City;
                traveler.Country = travelerGeoCoords.Country;
            }
        }

        public void UpdateTravelerCoordinates(Traveler traveler, GeoCoordinates coords)
        {

        }

    }
}

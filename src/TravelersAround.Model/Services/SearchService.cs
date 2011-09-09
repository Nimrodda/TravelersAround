using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Model.Entities;
using TravelersAround.Model.Exceptions;

namespace TravelersAround.Model.Services
{
    public class SearchService
    {
        private ILocationDeterminator _locationDeterminator;
        private IRepository _repository;
        private IGeoCoder _geoCoder;

        public SearchService(ILocationDeterminator locationDeterminator, 
                            IRepository repository,
                            IGeoCoder geoCoder)
        {
            _locationDeterminator = locationDeterminator;
            _repository = repository;
            _geoCoder = geoCoder;
        }
        
        public void SetCurrentLocationWithIPAddress(Guid travelerID, string ipAddress)
        {
            Traveler traveler = _repository.FindBy<Traveler>(t => t.TravelerID == travelerID);
            GeoCoordinates coordinates = _geoCoder.ConvertIPAddressToGeoCoordinates(ipAddress);
            traveler.Longtitude = coordinates.Longtitude;
            traveler.Latitude = coordinates.Latitude;
            _repository.Save(traveler);
            _repository.Commit();
        }

        public void SetCurrentLocationWithStreetAddress(Guid travelerID, string streetAddress)
        {
            Traveler traveler = _repository.FindBy<Traveler>(t => t.TravelerID == travelerID);
            GeoCoordinates coordinates = _geoCoder.ConvertStreetAddressToGeoCoordinates(streetAddress);
            traveler.Longtitude = coordinates.Longtitude;
            traveler.Latitude = coordinates.Latitude;
            _repository.Save(traveler);
            _repository.Commit();
        }

        public IEnumerable<Traveler> GetListOfTravelersWithin(int kmDistance, Guid travelerID)
        {
            Traveler traveler = _repository.FindBy<Traveler>(t => t.TravelerID == travelerID);
            if (traveler.Latitude > 0 && traveler.Longtitude > 0) 
                return _locationDeterminator.FindNearByTravelers(kmDistance, traveler.Latitude, traveler.Longtitude);
            else
                throw new InvalidTravelerLocationException();

        }
    }
}

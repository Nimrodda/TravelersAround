using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Contracts;
using TravelersAround.DataContracts;
using System.ServiceModel.Activation;
using System.ServiceModel;
using TravelersAround.Model;
using TravelersAround.Model.Entities;
using TravelersAround.Model.Factories;
using TravelersAround.Model.Services;
using Ninject;

namespace TravelersAround.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class MembershipService : IMembershipService
    {
        private IRepository _repository;
        private IMembership _membership;
        private IGeoCoder _geoCoder;
        private ILocationDeterminator _locationDeterminator;
        private IAPIKeyGenerator _apiKeyGen;
    
        public MembershipService(IRepository repository, 
                                IMembership membership,
                                IGeoCoder geoCoder,
                                ILocationDeterminator locationDeterminator,
                                IAPIKeyGenerator apiKeyGen)
        {
            _repository = repository;
            _membership = membership;
            _locationDeterminator = locationDeterminator;
            _geoCoder = geoCoder;
            _apiKeyGen = apiKeyGen;
        }

        public RegisterResponse Register(string email, string password, string confirmPassword, string firstname, string lastname, string birthdate, string gender)
        {
            RegisterResponse response = new RegisterResponse();

            try
            {
                Guid newTravelerID = _membership.CreateUser(email, password, email);
                //TODO: determine IP
                LocationService locationSvc = new LocationService(_locationDeterminator, _repository, _geoCoder);
                GeoCoordinates travelerGeoCoords = locationSvc.GetCurrentLocationWithIPAddress("IPADD");
                Traveler newTraveler = TravelerFactory.CreateTraveler(newTravelerID, firstname, lastname, birthdate, gender, travelerGeoCoords.Latitude, travelerGeoCoords.Longtitude);
                string travelerApiKey = _apiKeyGen.Generate(password);
                APIKeyService.Store(travelerApiKey, newTravelerID);
                _repository.Add<Traveler>(newTraveler);
                _repository.Commit();
                response.APIKey = newTravelerID.ToString("N");
                response.MarkSuccess();
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }

            return response;

        }

        public LoginResponse Login(string email, string password)
        {
            LoginResponse response = new LoginResponse();
            try
            {
                bool isMember = _membership.ValidateUser(email, password);
                if (isMember)
                {
                    Guid travelerID = _membership.GetUserTravelerID(email);
                    Traveler traveler = _repository.FindBy<Traveler>(t => t.TravelerID == travelerID);
                    string travelerApiKey = _apiKeyGen.Generate(password);
                    APIKeyService.Store(travelerApiKey, traveler.TravelerID);
                    //TODO: determine IP
                    LocationService locationSvc = new LocationService(_locationDeterminator, _repository, _geoCoder);
                    GeoCoordinates travelerGeoCoords = locationSvc.GetCurrentLocationWithIPAddress("IPADD");
                    if (traveler.IsLocationChanged(travelerGeoCoords))
                    {
                        traveler.Latitude = travelerGeoCoords.Latitude;
                        traveler.Longtitude = travelerGeoCoords.Longtitude;
                        _repository.Save<Traveler>(traveler);
                        _repository.Commit();
                    }
                    response.APIKey = travelerID.ToString("N");
                    response.MarkSuccess();
                }
                else
                {
                    response.ErrorMessage = R.ErrorMessages.InvalidCredentials;
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }
            return response;
        }
    }
}

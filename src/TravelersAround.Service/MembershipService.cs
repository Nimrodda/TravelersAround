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
using log4net;

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
        private ILog _log;
    
        public MembershipService(IRepository repository, 
                                IMembership membership,
                                IGeoCoder geoCoder,
                                ILocationDeterminator locationDeterminator,
                                IAPIKeyGenerator apiKeyGen,
                                ILog log)
        {
            _repository = repository;
            _membership = membership;
            _locationDeterminator = locationDeterminator;
            _geoCoder = geoCoder;
            _apiKeyGen = apiKeyGen;
            _log = log;
        }

        public RegisterResponse Register(RegisterRequest registerReq)
        {
            RegisterResponse response = new RegisterResponse();

            try
            {
                Guid newTravelerID = _membership.CreateUser(registerReq.Email, registerReq.Password, registerReq.Email);
                //TODO: determine IP
                LocationService locationSvc = new LocationService(_locationDeterminator, _repository, _geoCoder);
                GeoCoordinates travelerGeoCoords = locationSvc.GetCurrentLocationWithIPAddress("IPADD");
                Traveler newTraveler = TravelerFactory.CreateTraveler(
                                                                    newTravelerID, 
                                                                    registerReq.Firstname, 
                                                                    registerReq.Lastname, 
                                                                    registerReq.Birthdate, 
                                                                    registerReq.Gender, 
                                                                    travelerGeoCoords.Latitude, 
                                                                    travelerGeoCoords.Longtitude);
                string travelerApiKey = _apiKeyGen.Generate(registerReq.Password);
                APIKeyService.Store(travelerApiKey, newTravelerID);
                _repository.Add<Traveler>(newTraveler);
                _repository.Commit();
                response.APIKey = travelerApiKey;
                response.MarkSuccess();
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }

            return response;

        }

        public LoginResponse Login(LoginRequest loginReq)
        {
            LoginResponse response = new LoginResponse();
            try
            {
                bool isMember = _membership.ValidateUser(loginReq.Email, loginReq.Password);
                if (isMember)
                {
                    Guid travelerID = _membership.GetUserTravelerID(loginReq.Email);
                    Traveler traveler = _repository.FindBy<Traveler>(t => t.TravelerID == travelerID);
                    string travelerApiKey = _apiKeyGen.Generate(loginReq.Password);
                    APIKeyService.Store(travelerApiKey, traveler.TravelerID);
                    //TODO: determine IP
                    LocationService locationSvc = new LocationService(_locationDeterminator, _repository, _geoCoder);
                    GeoCoordinates travelerGeoCoords = locationSvc.GetCurrentLocationWithIPAddress("80.221.20.181");
                    if (traveler.IsLocationChanged(travelerGeoCoords))
                    {
                        traveler.Latitude = travelerGeoCoords.Latitude;
                        traveler.Longtitude = travelerGeoCoords.Longtitude;
                        _repository.Save<Traveler>(traveler);
                        _repository.Commit();
                    }
                    response.APIKey = travelerApiKey;
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

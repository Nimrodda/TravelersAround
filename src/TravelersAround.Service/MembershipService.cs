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
using TravelersAround.Model.Exceptions;
using Ninject;
using log4net;

namespace TravelersAround.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class MembershipService : ServiceBase, IMembershipService
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
                                IAPIKeyGenerator apiKeyGen,
                                ILog log) 
            : base(log)
        {
            _repository = repository;
            _membership = membership;
            _locationDeterminator = locationDeterminator;
            _geoCoder = geoCoder;
            _apiKeyGen = apiKeyGen;
        }

        public RegisterResponse Register(RegisterRequest registerReq)
        {
            RegisterResponse response = new RegisterResponse();

            try
            {
                //Creating new membership for traveler
                Guid newTravelerID = _membership.CreateUser(registerReq.Email, registerReq.Password, registerReq.Email);

                //Getting traveler's location based on his IP address if possible
                GeoCoordinates travelerGeoCoords = GetCoordinates();

                //Issuing an API key and storing in cache
                string travelerApiKey = GetUniqueApiKey(registerReq.Password);
                APIKeyService apiKeySvc = new APIKeyService();
                apiKeySvc.Store(travelerApiKey, newTravelerID);

                //Creating new traveler profile
                Traveler newTraveler = TravelerFactory.CreateTraveler(
                    newTravelerID, registerReq.Firstname, registerReq.Lastname, DateTime.Parse(registerReq.Birthdate), 
                    registerReq.Gender, travelerGeoCoords.Latitude, travelerGeoCoords.Longtitude, travelerApiKey, travelerGeoCoords.City, travelerGeoCoords.Country);

                //Persisting the new traveler profile to the database
                _repository.Add<Traveler>(newTraveler);
                _repository.Commit();

                response.APIKey = travelerApiKey;
                response.MarkSuccess();
            }
            catch (MembershipCreationFailedException ex)
            {
                response.ErrorMessage = R.String.ErrorMessages.ResourceManager.GetString(ex.Message);
            }
            catch (Exception ex)
            {
                ReportError(ex, response);
            }

            return response;

        }

        public LoginResponse Login(LoginRequest loginReq)
        {
            LoginResponse response = new LoginResponse();
            try
            {
                //Validating traveler's membership
                bool isMember = _membership.ValidateUser(loginReq.Email, loginReq.Password);
                if (isMember)
                {
                    Guid travelerID = _membership.GetUserTravelerID(loginReq.Email);
                    //Loading traveler's profile
                    Traveler traveler = _repository.FindBy<Traveler>(t => t.TravelerID == travelerID);

                    //Getting traveler's location based on his IP address if possible
                    GeoCoordinates travelerGeoCoords = GetCoordinates();

                    //Issuing an API key and storing in cache
                    string travelerApiKey = GetUniqueApiKey(loginReq.Password);
                    APIKeyService apiKeySvc = new APIKeyService();
                    apiKeySvc.Store(travelerApiKey, traveler.TravelerID);

                    //Updating traveler's location only if the location was changed since last login
                    if (travelerGeoCoords.IsValid() && traveler.IsLocationChanged(travelerGeoCoords))
                    {
                        traveler.Latitude = travelerGeoCoords.Latitude;
                        traveler.Longtitude = travelerGeoCoords.Longtitude;
                        traveler.City = travelerGeoCoords.City;
                        traveler.Country = travelerGeoCoords.Country;
                    }

                    traveler.ApiKey = travelerApiKey;

                    _repository.Save<Traveler>(traveler);
                    _repository.Commit();

                    response.APIKey = travelerApiKey;
                    response.MarkSuccess();
                }
                else
                {
                    response.ErrorMessage = R.String.ErrorMessages.InvalidCredentials;
                }
            }
            catch (Exception ex)
            {
                ReportError(ex, response);
            }
            return response;
        }

        /// <summary>
        /// Retreives GeoCoodinates based on valid traveler's IP address only
        /// </summary>
        /// <returns>String representation of IP Address</returns>
        private GeoCoordinates GetCoordinates()
        {
            if (APIKeyService.CurrentTravelerIPAddress != null)
            {
                LocationService locationSvc = new LocationService(_locationDeterminator, _repository, _geoCoder);
                return locationSvc.GetCurrentLocationWithIPAddress(APIKeyService.CurrentTravelerIPAddress);
            }
            else return new GeoCoordinates();
        }

        /// <summary>
        /// Generates a unique API key
        /// </summary>
        /// <param name="password">Traveler's password</param>
        /// <returns>String representation of API key</returns>
        private string GetUniqueApiKey(string password)
        {
            string newTravelerApiKey;
            APIKeyService apiKeySvc = new APIKeyService();
            //Verifying that the newly generated key is unique by checking if it exists in the cache
            do
            {
                newTravelerApiKey = _apiKeyGen.Generate(password);
            }
            while (apiKeySvc.IsValidAPIKey(newTravelerApiKey));

            return newTravelerApiKey;
        }

        public LogoutResponse Logout(string apiKey)
        {
            LogoutResponse response = new LogoutResponse();
            try
            {
                APIKeyService apiKeySvc = new APIKeyService();
                if (apiKeySvc.IsValidAPIKey(apiKey))
                {
                    Guid travelerId = apiKeySvc.GetAssociatedIdTo(apiKey);

                    //Removes the traveler's api key so the next time traveler will have to first login to get a new api key
                    Traveler traveler = _repository.FindBy<Traveler>(t => t.TravelerID == travelerId);
                    traveler.ApiKey = null;

                    //Removes the traveler's cache record
                    apiKeySvc.Delete(apiKey);

                    //Persist changes to DB
                    _repository.Save<Traveler>(traveler);
                    _repository.Commit();

                    response.MarkSuccess();
                }
                else
                {
                    response.ErrorMessage = R.String.ErrorMessages.InvalidAPIKey;
                }
            }
            catch (Exception ex)
            {
                ReportError(ex, response);
            }
            return response;
        }
    }
}

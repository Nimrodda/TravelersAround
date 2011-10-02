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
using TravelersAround.Service.Mappers;
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
        private ICache _cache;
        private IAPIKeyGenerator _apiKeyGen;

        public MembershipService(IRepository repository, 
                                IMembership membership,
                                IGeoCoder geoCoder,
                                ILocationDeterminator locationDeterminator,
                                ILog log,
                                ICache cache,
                                IAPIKeyGenerator apiKeyGen) 
            : base(log)
        {
            _repository = repository;
            _membership = membership;
            _locationDeterminator = locationDeterminator;
            _geoCoder = geoCoder;
            _cache = cache;
            _apiKeyGen = apiKeyGen;
        }

        public RegisterResponse Register(RegisterRequest registerReq)
        {
            RegisterResponse response = new RegisterResponse();

            try
            {
                //Creating new membership for traveler
                Guid newTravelerID = _membership.CreateUser(registerReq.Email, registerReq.Password, registerReq.Email);

                //Issuing an API key and storing in cache
                APIKeyService apiKeySvc = new APIKeyService(_repository, _apiKeyGen);
                string travelerApiKey = apiKeySvc.GetUniqueApiKey(registerReq.Password);
                apiKeySvc.Store(travelerApiKey, newTravelerID);

                //Creating new traveler profile
                Traveler newTraveler = TravelerFactory.CreateTraveler(
                    newTravelerID, registerReq.Firstname, registerReq.Lastname, DateTime.Parse(registerReq.Birthdate), 
                    registerReq.Gender, travelerApiKey);

                //Persisting the new traveler profile to the database
                _repository.Add<Traveler>(newTraveler);
                _repository.Commit();

                response.APIKey = travelerApiKey;
                response.MarkSuccess();
            }
            catch (MembershipCreationFailedException ex)
            {
                response.ResponseMessage = R.String.ErrorMessages.ResourceManager.GetString(ex.Message);
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

                    //Issuing an API key and storing in cache
                    APIKeyService apiKeySvc = new APIKeyService(_repository, _apiKeyGen);
                    string travelerApiKey = apiKeySvc.GetUniqueApiKey(loginReq.Password);
                    apiKeySvc.Store(travelerApiKey, traveler.TravelerID);
                    traveler.ApiKey = travelerApiKey;

                    _repository.Save<Traveler>(traveler);
                    _repository.Commit();

                    response.NewMessagesCount = traveler.Messages.Count(m => m.IsRead == false && m.FolderID == (int)FolderType.Inbox);
                    response.APIKey = travelerApiKey;
                    response.Profile = traveler.ConvertToTravelerView();
                    response.MarkSuccess();
                }
                else
                {
                    response.ResponseMessage = R.String.ErrorMessages.InvalidCredentials;
                }
            }
            catch (Exception ex)
            {
                ReportError(ex, response);
            }
            return response;
        }

        public LogoutResponse Logout(string apiKey)
        {
            LogoutResponse response = new LogoutResponse();
            try
            {
                APIKeyService apiKeySvc = new APIKeyService(_repository, _apiKeyGen);
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
                    response.ResponseMessage = R.String.ErrorMessages.InvalidAPIKey;
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

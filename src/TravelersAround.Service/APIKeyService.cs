using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using TravelersAround.Model;
using TravelersAround.Model.Entities;

namespace TravelersAround.Service
{
    public class APIKeyService
    {
        public static string CurrentTravelerAPIKey { get; set; }
        public static string CurrentTravelerIPAddress { get; set; }

        private static ICache _apiKeys = DepenedencyRegistration.Get<ICache>();
        private IRepository _repository = DepenedencyRegistration.Get<IRepository>();

        public static int IdleTime { get { return _apiKeys.IdleTime; } }
        public static int IdleUsersCleanUpTime { get { return _apiKeys.IdleUsersCleanUpTime; } }


        
        /// <summary>
        /// Validating API key by first looking in the cache and if not found then fallback to the Database
        /// </summary>
        /// <param name="key">API Key</param>
        /// <returns>True if found</returns>
        public bool IsValidAPIKey(string key)
        {
            //Trying to find api key in the cache first
            if (String.IsNullOrEmpty((string)_apiKeys.GetValue(key)))
            {
                //Falling back to the database to find the api key
                Traveler traveler = _repository.FindBy<Traveler>(t => t.ApiKey == key);
                if (traveler != null)
                {
                    //Found in the DB and persisting to cache
                    Store(key, traveler.TravelerID);
                    return true;
                }
                else
                {
                    //The API key couldn't be find either in the cache or DB
                    return false;
                }

            }
            else
            {
                //Found in the cache
                return true;
            }
        }

        /// <summary>
        /// Gets the Traveler ID assoicated to current request's API key
        /// </summary>
        /// <returns>TravelerID</returns>
        public static Guid GetAssociatedID()
        {
            return new Guid((string)_apiKeys.GetValue(CurrentTravelerAPIKey));
        }

        public Guid GetAssociatedIdTo(string key)
        {
            return new Guid((string)_apiKeys.GetValue(key));
        }

        public void Store(string apiKey, Guid travelerID)
        {
            //Checking if the current traveler already has API key in the cache
            string key = _apiKeys.GetKey(travelerID.ToString());
            if (!String.IsNullOrEmpty(key))
            {
                Delete(key);
            }

            _apiKeys.Add(apiKey, travelerID.ToString());
        }

        public void Delete(string key)
        {
            if (IsValidAPIKey(key)) _apiKeys.Remove(key);
        }

        public void SetOnline(string apiKey)
        {
            _apiKeys.SetOnline(apiKey);
        }

        public void IdleUsersCleanUp()
        {
            _apiKeys.RemoveExpired();
        }

        public void MarkIdleUsersOffline()
        {
            _apiKeys.SetIdleUserOffline();
        }


    }
}
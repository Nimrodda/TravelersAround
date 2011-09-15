using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using TravelersAround.Model;

namespace TravelersAround.Service
{
    public static class APIKeyService
    {
        public static string CurrentTravelerAPIKey { get; set; }

        private static ICache _apiKeys = DepenedencyRegistration.Get<ICache>();

        public static bool IsValidAPIKey(string key)
        {
            return !String.IsNullOrEmpty((string)_apiKeys.GetValue(key));
        }

        /// <summary>
        /// Gets the Traveler ID assoicated to current request's API key
        /// </summary>
        /// <returns>TravelerID</returns>
        public static Guid GetAssociatedID()
        {
            return new Guid((string)_apiKeys.GetValue(CurrentTravelerAPIKey));
        }

        public static Guid GetAssociatedIdTo(string key)
        {
            return new Guid((string)_apiKeys.GetValue(key));
        }

        public static void Store(string apiKey, Guid travelerID)
        {
            //Checking if the current traveler already has API key in the cache
            string key = _apiKeys.GetKey(travelerID.ToString());
            if (!String.IsNullOrEmpty(key))
            {
                Delete(key);
            }

            _apiKeys.Add(apiKey, travelerID.ToString());
        }

        public static void Delete(string key)
        {
            if (IsValidAPIKey(key)) _apiKeys.Remove(key);
        }
    }
}
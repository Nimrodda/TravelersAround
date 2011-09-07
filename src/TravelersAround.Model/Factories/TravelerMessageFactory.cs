using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Model.Entities;

namespace TravelersAround.Model.Factories
{
    public static class TravelerMessageFactory
    {
        public static TravelerMessage CreateTravelerMessageFrom(Traveler traveler, Message message)
        {
            TravelerMessage travelerMsg = new TravelerMessage();
            travelerMsg.Message = message;
            travelerMsg.Traveler = traveler;
            travelerMsg.TravelerID = traveler.TravelerID;
            travelerMsg.IsRead = false;
            travelerMsg.FolderID = FolderFactory.CreateFolderFrom(traveler, message);
            return travelerMsg;
        }
    }
}

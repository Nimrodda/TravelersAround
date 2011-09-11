using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Contracts;
using System.ServiceModel.Activation;
using System.ServiceModel;

namespace TravelersAround.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class TravelersAroundService : ITravelersAroundService
    {
        public TravelersAroundService()
        {
        }

        public DataContracts.RegisterResponse Register(string email, string password, string confirmPassword, string firstname, string lastname, string birthdate, string gender)
        {
            throw new NotImplementedException();
        }

        public DataContracts.LoginResponse Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public DataContracts.AddFriendResponse AddFriend(string friendID)
        {
            throw new NotImplementedException();
        }

        public DataContracts.RemoveFriendResponse RemoveFriend(string friendID)
        {
            throw new NotImplementedException();
        }

        public DataContracts.ListFriendsResponse ListFriends()
        {
            throw new NotImplementedException();
        }

        public DataContracts.ListMessagesResponse ListMessages()
        {
            throw new NotImplementedException();
        }

        public DataContracts.SendMessageResponse SendMessage(string subject, string body, string recipientID)
        {
            throw new NotImplementedException();
        }

        public DataContracts.DeleteMessageResponse DeleteMessage(string messageID)
        {
            throw new NotImplementedException();
        }

        public DataContracts.ReadMessageResponse ReadMessage(string messageID)
        {
            throw new NotImplementedException();
        }

        public DataContracts.UpdateProfileResponse UpdateProfile(string firstname, string lastname, string birthdate, string statusMessage, string gender, bool isAvailable)
        {
            throw new NotImplementedException();
        }

        public DataContracts.DisplayProfileResponse DisplayProfile()
        {
            throw new NotImplementedException();
        }

        public DataContracts.SearchResponse Search(bool availabilityMark)
        {
            throw new NotImplementedException();
        }
    }
}

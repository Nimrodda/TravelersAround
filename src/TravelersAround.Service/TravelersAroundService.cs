using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Contracts;
using System.ServiceModel.Activation;
using System.ServiceModel;
using TravelersAround.DataContracts;

namespace TravelersAround.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class TravelersAroundService : ITravelersAroundService
    {
        public TravelersAroundService()
        {
        }

        public RegisterResponse Register(string email, string password, string confirmPassword, string firstname, string lastname, string birthdate, string gender)
        {
            throw new NotImplementedException();
        }

        public LoginResponse Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public AddFriendResponse AddFriend(string friendID)
        {
            throw new NotImplementedException();
        }

        public RemoveFriendResponse RemoveFriend(string friendID)
        {
            throw new NotImplementedException();
        }

        public ListFriendsResponse ListFriends()
        {
            throw new NotImplementedException();
        }

        public ListMessagesResponse ListMessages()
        {
            throw new NotImplementedException();
        }

        public SendMessageResponse SendMessage(string subject, string body, string recipientID)
        {
            throw new NotImplementedException();
        }

        public DeleteMessageResponse DeleteMessage(string messageID)
        {
            throw new NotImplementedException();
        }

        public ReadMessageResponse ReadMessage(string messageID)
        {
            throw new NotImplementedException();
        }

        public UpdateProfileResponse UpdateProfile(string firstname, string lastname, string birthdate, string statusMessage, string gender, bool isAvailable)
        {
            throw new NotImplementedException();
        }

        public DisplayProfileResponse DisplayProfile()
        {
            throw new NotImplementedException();
        }

        public SearchResponse Search(bool availabilityMark)
        {
            throw new NotImplementedException();
        }
    }
}

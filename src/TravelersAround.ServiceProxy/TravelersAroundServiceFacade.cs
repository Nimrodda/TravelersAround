using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Contracts;
using TravelersAround.ServiceProxy.ViewModels;
using TravelersAround.DataContracts;
using System.IO;

namespace TravelersAround.ServiceProxy
{
    public class TravelersAroundServiceFacade : ITravelersAroundServiceFacade
    {
        private ITravelersAroundService _travelersAroundService;

        public FriendsListView AddFriend(string friendID)
        {
            throw new NotImplementedException();
        }

        public FriendsListView RemoveFriend(string friendID)
        {
            throw new NotImplementedException();
        }

        public FriendsListView ListFriends(int index, int count)
        {
            throw new NotImplementedException();
        }

        public MessagesListView ListMessages(string folderName, int index, int count)
        {
            throw new NotImplementedException();
        }

        public MessageSendView SendMessage(MessageSendView view)
        {
            throw new NotImplementedException();
        }

        public MessagesListView DeleteMessage(string messageID)
        {
            throw new NotImplementedException();
        }

        public MessageReadView ReadMessage(string messageID)
        {
            throw new NotImplementedException();
        }

        public ProfileUpdateView UpdateProfile(ProfileUpdateView view)
        {
            throw new NotImplementedException();
        }

        public ProfileDisplayView DisplayProfile()
        {
            throw new NotImplementedException();
        }

        public SearchView Search(bool availabilityMark, int index, int count)
        {
            throw new NotImplementedException();
        }

        public ProfileUpdateView UploadProfilePicture(Stream pictureStream)
        {
            throw new NotImplementedException();
        }

        public Stream GetProfilePicture(string travelerID)
        {
            throw new NotImplementedException();
        }
    }
}

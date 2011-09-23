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
    public class TravelersAroundServiceFacade : ServiceFacadeBase, ITravelersAroundServiceFacade
    {
        private ITravelersAroundService _travelersAroundService;

        public TravelersAroundServiceFacade(ITravelersAroundService travelersAroundService)
        {
            _travelersAroundService = travelersAroundService;
        }

        public FriendsListView AddFriend(string friendID)
        {
            return (FriendsListView)GetMappedObject(_travelersAroundService.AddFriend(friendID), typeof(FriendsListView));
        }

        public FriendsListView RemoveFriend(string friendID)
        {
            return (FriendsListView)GetMappedObject(_travelersAroundService.RemoveFriend(friendID), typeof(FriendsListView));
        }

        public FriendsListView ListFriends(int index, int count)
        {
            return (FriendsListView)GetMappedObject(_travelersAroundService.ListFriends(index, count), typeof(FriendsListView));
        }

        public MessagesListView ListMessages(string folderName, int index, int count)
        {
            return (MessagesListView)GetMappedObject(_travelersAroundService.ListMessages(folderName, index, count), typeof(MessagesListView));
        }

        public MessageSendView SendMessage(MessageSendView view)
        {
            SendMessageRequest request = (SendMessageRequest)GetMappedObject(view, typeof(SendMessageRequest));
            return (MessageSendView)GetMappedObject(_travelersAroundService.SendMessage(request), typeof(MessageSendView));
        }

        public MessagesListView DeleteMessage(string messageID)
        {
            return (MessagesListView)GetMappedObject(_travelersAroundService.DeleteMessage(messageID), typeof(MessagesListView));
        }

        public MessageReadView ReadMessage(string messageID)
        {
            return (MessageReadView)GetMappedObject(_travelersAroundService.ReadMessage(messageID), typeof(MessageReadView));
        }

        public ProfileUpdateView UpdateProfile(ProfileUpdateView view)
        {
            UpdateProfileRequest request = (UpdateProfileRequest)GetMappedObject(view, typeof(UpdateProfileRequest));
            return (ProfileUpdateView)GetMappedObject(_travelersAroundService.UpdateProfile(request), typeof(ProfileUpdateView));
        }

        public ProfileDisplayView DisplayProfile()
        {
            return (ProfileDisplayView)GetMappedObject(_travelersAroundService.DisplayProfile(), typeof(ProfileDisplayView));
        }

        public SearchView Search(bool availabilityMark, int index, int count)
        {
            return (SearchView)GetMappedObject(_travelersAroundService.Search(availabilityMark, index, count), typeof(SearchView));
        }

        public ProfileUpdateView UploadProfilePicture(Stream pictureStream)
        {
            return (ProfileUpdateView)GetMappedObject(_travelersAroundService.UploadProfilePicture(pictureStream), typeof(ProfileUpdateView));
        }

        public Stream GetProfilePicture(string travelerID)
        {
            return _travelersAroundService.GetProfilePicture(travelerID);
        }
    }
}

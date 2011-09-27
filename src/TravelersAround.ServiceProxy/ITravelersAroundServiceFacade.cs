using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.ServiceProxy.ViewModels;
using System.IO;

namespace TravelersAround.ServiceProxy
{
    public interface ITravelersAroundServiceFacade
    {
        FriendsListView AddFriend(string friendID);

        FriendsListView RemoveFriend(string friendID);

        FriendsListView ListFriends(int index, int count);

        MessagesListView ListMessages(string folderName, int index, int count);

        MessageSendView SendMessage(MessageSendView view);

        MessagesListView DeleteMessage(string messageID);

        MessageReadView ReadMessage(string messageID);

        ProfileUpdateView UpdateProfile(ProfileUpdateView view);

        ProfileDisplayView DisplayProfile();

        SearchView Search(bool availabilityMark, int index, int count, string ipAddress = null, double lat = 0, double lon = 0);

        ProfileUpdateView UploadProfilePicture(Stream pictureStream);

        Stream GetProfilePicture(string travelerID);

        TickerModel Tick(TickerModel tick);
    }
}

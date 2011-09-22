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
        bool AddFriend(string friendID);

        bool RemoveFriend(string friendID);

        FriendsListView ListFriends(int index, int count);

        MessagesListView ListMessages(string folderName, int index, int count);

        MessageSendView SendMessage(MessageSendView view);

        bool DeleteMessage(string messageID);

        MessageReadView ReadMessage(string messageID);

        ProfileUpdateView UpdateProfile(ProfileUpdateView view);

        ProfileDisplayView DisplayProfile();

        SearchtView Search(bool availabilityMark, int index, int count);

        bool UploadProfilePicture(Stream pictureStream);

        Stream GetProfilePicture(string travelerID);
    }
}

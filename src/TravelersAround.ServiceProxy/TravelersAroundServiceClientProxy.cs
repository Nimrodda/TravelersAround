using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Contracts;
using TravelersAround.DataContracts;
using System.Reflection;
using System.Collections.Specialized;
using System.IO;

namespace TravelersAround.ServiceProxy
{
    public class TravelersAroundServiceClientProxy : ServiceClientProxyBase, ITravelersAroundService
    {
        public TravelersAroundServiceClientProxy(string serviceBaseUrl) : base(serviceBaseUrl) { }

        public AddFriendResponse AddFriend(string friendID)
        {
            string queryString = HttpRequestAdapter.ConstructQueryString(MethodBase.GetCurrentMethod().GetParameters(), friendID);
            return HttpRequestAdapter.WebHttpRequest<AddFriendResponse>(_serviceBaseUrl, "Friends/Add", queryString, HttpRequestAdapter.Method.POST);
        }

        public RemoveFriendResponse RemoveFriend(string friendID)
        {
            string queryString = HttpRequestAdapter.ConstructQueryString(MethodBase.GetCurrentMethod().GetParameters(), friendID);
            return HttpRequestAdapter.WebHttpRequest<RemoveFriendResponse>(_serviceBaseUrl, "Friends/Remove", queryString, HttpRequestAdapter.Method.POST);
        }

        public ListFriendsResponse ListFriends(int index, int count)
        {
            string queryString = HttpRequestAdapter.ConstructQueryString(MethodBase.GetCurrentMethod().GetParameters(), index, count);
            return HttpRequestAdapter.WebHttpRequest<ListFriendsResponse>(_serviceBaseUrl, "Friends/List", queryString);
        }

        public ListMessagesResponse ListMessages(string folderName, int index, int count)
        {
            string queryString = HttpRequestAdapter.ConstructQueryString(MethodBase.GetCurrentMethod().GetParameters(), folderName, index, count);
            return HttpRequestAdapter.WebHttpRequest<ListMessagesResponse>(_serviceBaseUrl, "Messages/List", queryString);
        }

        public SendMessageResponse SendMessage(SendMessageRequest sendMsgReq)
        {
            return HttpRequestAdapter.WebHttpPost<SendMessageResponse>(_serviceBaseUrl, "Messages/Send", sendMsgReq);
        }

        public DeleteMessageResponse DeleteMessage(string messageID)
        {
            string queryString = HttpRequestAdapter.ConstructQueryString(MethodBase.GetCurrentMethod().GetParameters(), messageID);
            return HttpRequestAdapter.WebHttpRequest<DeleteMessageResponse>(_serviceBaseUrl, "Messages/Delete", queryString, HttpRequestAdapter.Method.POST);
        }

        public ReadMessageResponse ReadMessage(string messageID)
        {
            string queryString = HttpRequestAdapter.ConstructQueryString(MethodBase.GetCurrentMethod().GetParameters(), messageID);
            return HttpRequestAdapter.WebHttpRequest<ReadMessageResponse>(_serviceBaseUrl, "Messages/Read", queryString);
        }

        public UpdateProfileResponse UpdateProfile(UpdateProfileRequest updateProfileReq)
        {
            return HttpRequestAdapter.WebHttpPost<UpdateProfileResponse>(_serviceBaseUrl, "Profile/Update", updateProfileReq);
        }

        public DisplayProfileResponse DisplayProfile()
        {
            return HttpRequestAdapter.WebHttpRequest<DisplayProfileResponse>(_serviceBaseUrl, "Profile/Display", String.Empty);
        }

        public SearchResponse Search(bool availabilityMark, int index, int count)
        {
            string queryString = HttpRequestAdapter.ConstructQueryString(MethodBase.GetCurrentMethod().GetParameters(), availabilityMark, index, count);
            return HttpRequestAdapter.WebHttpRequest<SearchResponse>(_serviceBaseUrl, "Search", queryString);
        }

        public ProfilePictureUploadResponse UploadProfilePicture(Stream pictureStream)
        {
            return HttpRequestAdapter.WebHttpPost<ProfilePictureUploadResponse>(_serviceBaseUrl, "Profile/Picture", pictureStream, String.Empty);
        }

        public Stream GetProfilePicture(string travelerID)
        {
            string queryString = HttpRequestAdapter.ConstructQueryString(MethodBase.GetCurrentMethod().GetParameters(), travelerID);
            return HttpRequestAdapter.WebHttpRequest(_serviceBaseUrl, "Profile/Picture", queryString);
        }
    }
}

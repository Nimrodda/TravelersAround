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
        private string _apiKey;

        public TravelersAroundServiceClientProxy(string serviceBaseUrl, string apiKey) : base(serviceBaseUrl) 
        {
            _apiKey = apiKey;
        }

        private string apiKeyQueryString
        {
            get
            {
                return String.Concat("&apikey=", _apiKey);
            }
        }

        public AddFriendResponse AddFriend(string friendID)
        {
            string queryString = HttpRequestAdapter.ConstructQueryString(MethodBase.GetCurrentMethod().GetParameters(), friendID) + apiKeyQueryString;
            return HttpRequestAdapter.WebHttpRequest<AddFriendResponse>(_serviceBaseUrl, "Friends/Add", queryString, HttpRequestAdapter.Method.POST);
        }

        public RemoveFriendResponse RemoveFriend(string friendID)
        {
            string queryString = HttpRequestAdapter.ConstructQueryString(MethodBase.GetCurrentMethod().GetParameters(), friendID) + apiKeyQueryString;
            return HttpRequestAdapter.WebHttpRequest<RemoveFriendResponse>(_serviceBaseUrl, "Friends/Remove", queryString, HttpRequestAdapter.Method.POST);
        }

        public ListFriendsResponse ListFriends(int index, int count)
        {
            string queryString = HttpRequestAdapter.ConstructQueryString(MethodBase.GetCurrentMethod().GetParameters(), index, count) + apiKeyQueryString;
            return HttpRequestAdapter.WebHttpRequest<ListFriendsResponse>(_serviceBaseUrl, "Friends/List", queryString);
        }

        public ListMessagesResponse ListMessages(string folderName, int index, int count)
        {
            string queryString = HttpRequestAdapter.ConstructQueryString(MethodBase.GetCurrentMethod().GetParameters(), folderName, index, count) + apiKeyQueryString;
            return HttpRequestAdapter.WebHttpRequest<ListMessagesResponse>(_serviceBaseUrl, "Messages/List", queryString);
        }

        public SendMessageResponse SendMessage(SendMessageRequest sendMsgReq)
        {
            return HttpRequestAdapter.WebHttpPostRequest<SendMessageResponse>(_serviceBaseUrl, "Messages/Send", sendMsgReq, String.Concat("?apikey=", _apiKey));
        }

        public DeleteMessageResponse DeleteMessage(string messageID)
        {
            string queryString = HttpRequestAdapter.ConstructQueryString(MethodBase.GetCurrentMethod().GetParameters(), messageID) + apiKeyQueryString;
            return HttpRequestAdapter.WebHttpRequest<DeleteMessageResponse>(_serviceBaseUrl, "Messages/Delete", queryString, HttpRequestAdapter.Method.POST);
        }

        public ReadMessageResponse ReadMessage(string messageID)
        {
            string queryString = HttpRequestAdapter.ConstructQueryString(MethodBase.GetCurrentMethod().GetParameters(), messageID) + apiKeyQueryString;
            return HttpRequestAdapter.WebHttpRequest<ReadMessageResponse>(_serviceBaseUrl, "Messages/Read", queryString);
        }

        public UpdateProfileResponse UpdateProfile(UpdateProfileRequest updateProfileReq)
        {
            return HttpRequestAdapter.WebHttpPostRequest<UpdateProfileResponse>(_serviceBaseUrl, "Profile/Update", updateProfileReq, String.Concat("?apikey=", _apiKey));
        }

        public DisplayProfileResponse DisplayProfile()
        {
            return HttpRequestAdapter.WebHttpRequest<DisplayProfileResponse>(_serviceBaseUrl, "Profile/Display", String.Concat("?apikey=", _apiKey));
        }

        public SearchResponse Search(bool availabilityMark, int index, int count)
        {
            string queryString = HttpRequestAdapter.ConstructQueryString(MethodBase.GetCurrentMethod().GetParameters(), availabilityMark, index, count) + apiKeyQueryString;
            return HttpRequestAdapter.WebHttpRequest<SearchResponse>(_serviceBaseUrl, "Search", queryString);
        }

        public ProfilePictureUploadResponse UploadProfilePicture(Stream pictureStream)
        {
            return HttpRequestAdapter.WebHttpPostRequest<ProfilePictureUploadResponse>(_serviceBaseUrl, "Profile/Picture", pictureStream, String.Concat("?apikey=", _apiKey));
        }

        public Stream GetProfilePicture(string travelerID)
        {
            string queryString = HttpRequestAdapter.ConstructQueryString(MethodBase.GetCurrentMethod().GetParameters(), travelerID) + apiKeyQueryString;
            return HttpRequestAdapter.WebHttpRequest(_serviceBaseUrl, "Profile/Picture", queryString);
        }
    }
}

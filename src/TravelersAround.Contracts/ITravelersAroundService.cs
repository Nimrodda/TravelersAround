using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using TravelersAround.DataContracts;


namespace TravelersAround.Contracts
{
    [ServiceContract]
    public interface ITravelersAroundService
    {
        //Relationship service
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Friends/Add?friendId={friendID}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        AddFriendResponse AddFriend(string friendID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Friends/Remove?friendId={friendID}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        RemoveFriendResponse RemoveFriend(string friendID);

        [OperationContract]
        [WebGet(UriTemplate = "Friends/List?index={index}&count={count}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        ListFriendsResponse ListFriends(int index, int count);

        //Messaging service
        [OperationContract]
        [WebGet(UriTemplate = "Messages/List?folderName={folderName}&index={index}&count={count}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        ListMessagesResponse ListMessages(string folderName, int index, int count);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Messages/Send?subject={subject}&body={body}&recipientId={recipientID}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        SendMessageResponse SendMessage(string subject, string body, string recipientID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Messages/Delete?messageId={messageID}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        DeleteMessageResponse DeleteMessage(string messageID);

        [OperationContract]
        [WebGet(UriTemplate = "Messages/Read?messageId={messageID}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        ReadMessageResponse ReadMessage(string messageID);

        //Profile services
        [OperationContract]
        [WebInvoke(Method = "POST",
                UriTemplate = @"Profile/Update?firstname={firstname}&
                                lastname={lastname}&
                                birthdate={birthdate}&
                                statusMessage={statusMessage}&
                                gender={gender}&
                                isAvailable={isAvailable}",
                BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        UpdateProfileResponse UpdateProfile(string firstname, string lastname, string birthdate, string statusMessage, string gender, bool isAvailable);

        [OperationContract]
        [WebGet(UriTemplate = "Profile/Display", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        DisplayProfileResponse DisplayProfile();

        //Search service
        [OperationContract]
        [WebGet(UriTemplate = "Search/?mark={availabilityMark}&index={index}&count={count}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        SearchResponse Search(bool availabilityMark,int index, int count);
    }
}

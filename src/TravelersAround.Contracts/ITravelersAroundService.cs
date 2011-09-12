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
        [WebInvoke(Method = "POST", UriTemplate = "Friends/Add?friendId={friendID}", BodyStyle = WebMessageBodyStyle.Bare)]
        AddFriendResponse AddFriend(string friendID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Friends/Remove?friendId={friendID}", BodyStyle = WebMessageBodyStyle.Bare)]
        RemoveFriendResponse RemoveFriend(string friendID);

        [OperationContract]
        [WebGet(UriTemplate = "Friends/List", BodyStyle = WebMessageBodyStyle.Bare)]
        ListFriendsResponse ListFriends();

        //Messaging service
        [OperationContract]
        [WebGet(UriTemplate = "Messages/List", BodyStyle = WebMessageBodyStyle.Bare)]
        ListMessagesResponse ListMessages();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Messages/Send?subject={subject}&body={body}&recipientId={recipientID}", BodyStyle = WebMessageBodyStyle.Bare)]
        SendMessageResponse SendMessage(string subject, string body, string recipientID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Messages/Delete?messageId={messageID}", BodyStyle = WebMessageBodyStyle.Bare)]
        DeleteMessageResponse DeleteMessage(string messageID);

        [OperationContract]
        [WebGet(UriTemplate = "Messages/Read?messageId={messageID}", BodyStyle = WebMessageBodyStyle.Bare)]
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
                BodyStyle = WebMessageBodyStyle.Bare)]
        UpdateProfileResponse UpdateProfile(string firstname, string lastname, string birthdate, string statusMessage, string gender, bool isAvailable);

        [OperationContract]
        [WebGet(UriTemplate = "Profile/Display", BodyStyle = WebMessageBodyStyle.Bare)]
        DisplayProfileResponse DisplayProfile();

        //Search service
        [OperationContract]
        [WebGet(UriTemplate = "Search/?mark={availabilityMark}", BodyStyle = WebMessageBodyStyle.Bare)]
        SearchResponse Search(bool availabilityMark);
    }
}

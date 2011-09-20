using TravelersAround.ServiceProxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TravelersAround.DataContracts;
using System.IO;

namespace TravelersAround.Test
{
    
    
    /// <summary>
    ///This is a test class for TravelersAroundServiceClientProxyTest and is intended
    ///to contain all TravelersAroundServiceClientProxyTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TravelersAroundServiceClientProxyTest
    {
        private const string _serviceBaseUrl = "http://localhost.:6149/TravelersAroundService";
        
        //api key of user: test1@ta.com
        private const string _apiKey = "aqTXIbLdMKbPmVFkBuPz6YiwYGQZSYrSKqOceaBquDckYFr-BYF9NY2ZCdiZRvZEaK2iFe6yP6_r_vptTSiOZg2";
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for AddFriend
        ///</summary>
        [TestMethod()]
        public void AddFriendTest()
        {
            TravelersAroundServiceClientProxy target = new TravelersAroundServiceClientProxy(_serviceBaseUrl, _apiKey); // TODO: Initialize to an appropriate value
            string friendID = "26b7d8fb-efc3-409d-b001-41b52eef544d";
            AddFriendResponse actual;
            actual = target.AddFriend(friendID);
            Assert.IsTrue(actual.Success);
        }

        /// <summary>
        ///A test for DeleteMessage
        ///</summary>
        [TestMethod()]
        public void DeleteMessageTest()
        {
            TravelersAroundServiceClientProxy target = new TravelersAroundServiceClientProxy(_serviceBaseUrl, _apiKey); // TODO: Initialize to an appropriate value
            string messageID = "d3fc5f98-cfe2-e011-8ebf-206a8a339500";
            DeleteMessageResponse actual;
            actual = target.DeleteMessage(messageID);
            Assert.IsTrue(actual.Success);
        }

        /// <summary>
        ///A test for DisplayProfile
        ///</summary>
        [TestMethod()]
        public void DisplayProfileTest()
        {
            TravelersAroundServiceClientProxy target = new TravelersAroundServiceClientProxy(_serviceBaseUrl, _apiKey); // TODO: Initialize to an appropriate value
            DisplayProfileResponse actual;
            actual = target.DisplayProfile();
            Assert.IsNotNull(actual.Profile);
        }

        /// <summary>
        ///A test for GetProfilePicture
        ///</summary>
        [TestMethod()]
        public void GetProfilePictureTest()
        {
            TravelersAroundServiceClientProxy target = new TravelersAroundServiceClientProxy(_serviceBaseUrl, _apiKey); // TODO: Initialize to an appropriate value
            string travelerID = string.Empty; // TODO: Initialize to an appropriate value

            Stream actual = target.GetProfilePicture(travelerID);
            MemoryStream ms = new MemoryStream();
            actual.CopyTo(ms);
            
        }

        /// <summary>
        ///A test for ListFriends
        ///</summary>
        [TestMethod()]
        public void ListFriendsTest()
        {
            TravelersAroundServiceClientProxy target = new TravelersAroundServiceClientProxy(_serviceBaseUrl, _apiKey); // TODO: Initialize to an appropriate value
            int index = 5; // TODO: Initialize to an appropriate value
            int count = 10; // TODO: Initialize to an appropriate value
            ListFriendsResponse actual;
            actual = target.ListFriends(index, count);
        }

        /// <summary>
        ///A test for ListMessages
        ///</summary>
        [TestMethod()]
        public void ListMessagesTest()
        {
            TravelersAroundServiceClientProxy target = new TravelersAroundServiceClientProxy(_serviceBaseUrl, _apiKey); // TODO: Initialize to an appropriate value
            string folderName = "Sent";
            int index = 0; // TODO: Initialize to an appropriate value
            int count = 10; // TODO: Initialize to an appropriate value
            ListMessagesResponse actual;
            actual = target.ListMessages(folderName, index, count);
        }

        /// <summary>
        ///A test for ReadMessage
        ///</summary>
        [TestMethod()]
        public void ReadMessageTest()
        {
            TravelersAroundServiceClientProxy target = new TravelersAroundServiceClientProxy(_serviceBaseUrl, _apiKey); // TODO: Initialize to an appropriate value
            string messageID = "d3fc5f98-cfe2-e011-8ebf-206a8a339500";
            ReadMessageResponse actual;
            actual = target.ReadMessage(messageID);
        }

        /// <summary>
        ///A test for RemoveFriend
        ///</summary>
        [TestMethod()]
        public void RemoveFriendTest()
        {
            TravelersAroundServiceClientProxy target = new TravelersAroundServiceClientProxy(_serviceBaseUrl, _apiKey); // TODO: Initialize to an appropriate value
            string friendID = "26b7d8fb-efc3-409d-b001-41b52eef544d";
            RemoveFriendResponse actual;
            actual = target.RemoveFriend(friendID);
            
        }

        /// <summary>
        ///A test for Search
        ///</summary>
        [TestMethod()]
        public void SearchTest()
        {
            TravelersAroundServiceClientProxy target = new TravelersAroundServiceClientProxy(_serviceBaseUrl, _apiKey); // TODO: Initialize to an appropriate value
            bool availabilityMark = true; // TODO: Initialize to an appropriate value
            int index = 0; // TODO: Initialize to an appropriate value
            int count = 10; // TODO: Initialize to an appropriate value
            SearchResponse actual;
            actual = target.Search(availabilityMark, index, count);
            Assert.IsTrue(actual.Success);
        }

        /// <summary>
        ///A test for SendMessage
        ///</summary>
        [TestMethod()]
        public void SendMessageTest()
        {
            TravelersAroundServiceClientProxy target = new TravelersAroundServiceClientProxy(_serviceBaseUrl, _apiKey); // TODO: Initialize to an appropriate value
            SendMessageRequest sendMsgReq = new SendMessageRequest { Body = "is num", Subject = "head", RecipientID = new Guid("26b7d8fb-efc3-409d-b001-41b52eef544d") };
            SendMessageResponse actual;
            actual = target.SendMessage(sendMsgReq);
            Assert.IsTrue(actual.Success);
        }

        /// <summary>
        ///A test for UpdateProfile
        ///</summary>
        [TestMethod()]
        public void UpdateProfileTest()
        {
            TravelersAroundServiceClientProxy target = new TravelersAroundServiceClientProxy(_serviceBaseUrl, _apiKey); // TODO: Initialize to an appropriate value
            UpdateProfileRequest updateProfileReq = new UpdateProfileRequest { Birthdate = DateTime.Now, Firstname = "Acer", Gender = "F", IsAvailable = true, Lastname = "Laptop", StatusMessage = "Sup guys?" }; 
            UpdateProfileResponse actual;
            actual = target.UpdateProfile(updateProfileReq);
            Assert.IsTrue(actual.Success);
        }

        /// <summary>
        ///A test for UploadProfilePicture
        ///</summary>
        [TestMethod()]
        public void UploadProfilePictureTest()
        {
            TravelersAroundServiceClientProxy target = new TravelersAroundServiceClientProxy(_serviceBaseUrl, _apiKey); // TODO: Initialize to an appropriate value
            Stream pictureStream = new FileStream("d:\\me.gif", FileMode.Open, FileAccess.Read);
            ProfilePictureUploadResponse actual;
            actual = target.UploadProfilePicture(pictureStream);
            Assert.IsTrue(actual.Success);
        }
    }
}

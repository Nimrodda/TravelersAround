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
        private const string _serviceBaseUrl = "http://localhost.:2848/TravelersAroundService";
        
        //api key of user: test1@ta.com
        private const string _apiKey = "OMhsROUKSLMGsTQDz0RV6JeJB2ULRnCCaxh4J583MNz2ihYluiMi6n8Dw_PL4pSJkV-4UjveVDjQ9wBCaKvbyg2";
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
        }

        /// <summary>
        ///A test for DeleteMessage
        ///</summary>
        [TestMethod()]
        public void DeleteMessageTest()
        {
            string serviceBaseUrl = string.Empty; // TODO: Initialize to an appropriate value
            string apiKey = string.Empty; // TODO: Initialize to an appropriate value
            TravelersAroundServiceClientProxy target = new TravelersAroundServiceClientProxy(serviceBaseUrl, apiKey); // TODO: Initialize to an appropriate value
            string messageID = string.Empty; // TODO: Initialize to an appropriate value
            DeleteMessageResponse expected = null; // TODO: Initialize to an appropriate value
            DeleteMessageResponse actual;
            actual = target.DeleteMessage(messageID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DisplayProfile
        ///</summary>
        [TestMethod()]
        public void DisplayProfileTest()
        {
            string serviceBaseUrl = string.Empty; // TODO: Initialize to an appropriate value
            string apiKey = string.Empty; // TODO: Initialize to an appropriate value
            TravelersAroundServiceClientProxy target = new TravelersAroundServiceClientProxy(serviceBaseUrl, apiKey); // TODO: Initialize to an appropriate value
            DisplayProfileResponse expected = null; // TODO: Initialize to an appropriate value
            DisplayProfileResponse actual;
            actual = target.DisplayProfile();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetProfilePicture
        ///</summary>
        [TestMethod()]
        public void GetProfilePictureTest()
        {
            string serviceBaseUrl = string.Empty; // TODO: Initialize to an appropriate value
            string apiKey = string.Empty; // TODO: Initialize to an appropriate value
            TravelersAroundServiceClientProxy target = new TravelersAroundServiceClientProxy(serviceBaseUrl, apiKey); // TODO: Initialize to an appropriate value
            string travelerID = string.Empty; // TODO: Initialize to an appropriate value
            Stream expected = null; // TODO: Initialize to an appropriate value
            Stream actual;
            actual = target.GetProfilePicture(travelerID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ListFriends
        ///</summary>
        [TestMethod()]
        public void ListFriendsTest()
        {
            string serviceBaseUrl = string.Empty; // TODO: Initialize to an appropriate value
            string apiKey = string.Empty; // TODO: Initialize to an appropriate value
            TravelersAroundServiceClientProxy target = new TravelersAroundServiceClientProxy(serviceBaseUrl, apiKey); // TODO: Initialize to an appropriate value
            int index = 0; // TODO: Initialize to an appropriate value
            int count = 0; // TODO: Initialize to an appropriate value
            ListFriendsResponse expected = null; // TODO: Initialize to an appropriate value
            ListFriendsResponse actual;
            actual = target.ListFriends(index, count);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ListMessages
        ///</summary>
        [TestMethod()]
        public void ListMessagesTest()
        {
            string serviceBaseUrl = string.Empty; // TODO: Initialize to an appropriate value
            string apiKey = string.Empty; // TODO: Initialize to an appropriate value
            TravelersAroundServiceClientProxy target = new TravelersAroundServiceClientProxy(serviceBaseUrl, apiKey); // TODO: Initialize to an appropriate value
            string folderName = string.Empty; // TODO: Initialize to an appropriate value
            int index = 0; // TODO: Initialize to an appropriate value
            int count = 0; // TODO: Initialize to an appropriate value
            ListMessagesResponse expected = null; // TODO: Initialize to an appropriate value
            ListMessagesResponse actual;
            actual = target.ListMessages(folderName, index, count);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ReadMessage
        ///</summary>
        [TestMethod()]
        public void ReadMessageTest()
        {
            string serviceBaseUrl = string.Empty; // TODO: Initialize to an appropriate value
            string apiKey = string.Empty; // TODO: Initialize to an appropriate value
            TravelersAroundServiceClientProxy target = new TravelersAroundServiceClientProxy(serviceBaseUrl, apiKey); // TODO: Initialize to an appropriate value
            string messageID = string.Empty; // TODO: Initialize to an appropriate value
            ReadMessageResponse expected = null; // TODO: Initialize to an appropriate value
            ReadMessageResponse actual;
            actual = target.ReadMessage(messageID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for RemoveFriend
        ///</summary>
        [TestMethod()]
        public void RemoveFriendTest()
        {
            string serviceBaseUrl = string.Empty; // TODO: Initialize to an appropriate value
            string apiKey = string.Empty; // TODO: Initialize to an appropriate value
            TravelersAroundServiceClientProxy target = new TravelersAroundServiceClientProxy(serviceBaseUrl, apiKey); // TODO: Initialize to an appropriate value
            string friendID = string.Empty; // TODO: Initialize to an appropriate value
            RemoveFriendResponse expected = null; // TODO: Initialize to an appropriate value
            RemoveFriendResponse actual;
            actual = target.RemoveFriend(friendID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Search
        ///</summary>
        [TestMethod()]
        public void SearchTest()
        {
            string serviceBaseUrl = string.Empty; // TODO: Initialize to an appropriate value
            string apiKey = string.Empty; // TODO: Initialize to an appropriate value
            TravelersAroundServiceClientProxy target = new TravelersAroundServiceClientProxy(serviceBaseUrl, apiKey); // TODO: Initialize to an appropriate value
            bool availabilityMark = false; // TODO: Initialize to an appropriate value
            int index = 0; // TODO: Initialize to an appropriate value
            int count = 0; // TODO: Initialize to an appropriate value
            SearchResponse expected = null; // TODO: Initialize to an appropriate value
            SearchResponse actual;
            actual = target.Search(availabilityMark, index, count);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SendMessage
        ///</summary>
        [TestMethod()]
        public void SendMessageTest()
        {
            string serviceBaseUrl = string.Empty; // TODO: Initialize to an appropriate value
            string apiKey = string.Empty; // TODO: Initialize to an appropriate value
            TravelersAroundServiceClientProxy target = new TravelersAroundServiceClientProxy(serviceBaseUrl, apiKey); // TODO: Initialize to an appropriate value
            SendMessageRequest sendMsgReq = null; // TODO: Initialize to an appropriate value
            SendMessageResponse expected = null; // TODO: Initialize to an appropriate value
            SendMessageResponse actual;
            actual = target.SendMessage(sendMsgReq);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateProfile
        ///</summary>
        [TestMethod()]
        public void UpdateProfileTest()
        {
            string serviceBaseUrl = string.Empty; // TODO: Initialize to an appropriate value
            string apiKey = string.Empty; // TODO: Initialize to an appropriate value
            TravelersAroundServiceClientProxy target = new TravelersAroundServiceClientProxy(serviceBaseUrl, apiKey); // TODO: Initialize to an appropriate value
            UpdateProfileRequest updateProfileReq = null; // TODO: Initialize to an appropriate value
            UpdateProfileResponse expected = null; // TODO: Initialize to an appropriate value
            UpdateProfileResponse actual;
            actual = target.UpdateProfile(updateProfileReq);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UploadProfilePicture
        ///</summary>
        [TestMethod()]
        public void UploadProfilePictureTest()
        {
            string serviceBaseUrl = string.Empty; // TODO: Initialize to an appropriate value
            string apiKey = string.Empty; // TODO: Initialize to an appropriate value
            TravelersAroundServiceClientProxy target = new TravelersAroundServiceClientProxy(serviceBaseUrl, apiKey); // TODO: Initialize to an appropriate value
            Stream pictureStream = null; // TODO: Initialize to an appropriate value
            ProfilePictureUploadResponse expected = null; // TODO: Initialize to an appropriate value
            ProfilePictureUploadResponse actual;
            actual = target.UploadProfilePicture(pictureStream);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}

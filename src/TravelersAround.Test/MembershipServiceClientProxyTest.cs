using TravelersAround.ServiceProxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TravelersAround.DataContracts;

namespace TravelersAround.Test
{
    
    
    /// <summary>
    ///This is a test class for MembershipServiceClientProxyTest and is intended
    ///to contain all MembershipServiceClientProxyTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MembershipServiceClientProxyTest
    {

        private const string _serviceBaseUrl = "http://localhost.:2848/MembershipService";
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
        ///A test for Login
        ///</summary>
        [TestMethod()]
        public void LoginTest()
        {

            MembershipServiceClientProxy target = new MembershipServiceClientProxy(_serviceBaseUrl);
            LoginRequest loginReq = new LoginRequest { Email = "test1@ta.com", Password = "123456" };
            LoginResponse actual = target.Login(loginReq);
            Assert.IsTrue(actual.Success);
            
        }

        /// <summary>
        ///A test for Register
        ///</summary>
        [TestMethod()]
        public void Register_With_Existing_Email_Returns_EmailAlreadyExistsErrorMessage()
        {
            string serviceBaseUrl = _serviceBaseUrl; 
            MembershipServiceClientProxy target = new MembershipServiceClientProxy(serviceBaseUrl);
            RegisterRequest registerReq = new RegisterRequest { Birthdate = DateTime.Now.ToString(), ConfirmPassword = "123456", Password = "123456", Email = "test2@ta.com", Firstname = "Test2", Gender = "F", Lastname = "Test2" };
            RegisterResponse actual = target.Register(registerReq);
        }

        [TestMethod()]
        public void Register_New()
        {
            string serviceBaseUrl = _serviceBaseUrl;
            MembershipServiceClientProxy target = new MembershipServiceClientProxy(serviceBaseUrl);
            RegisterRequest registerReq = new RegisterRequest { Birthdate = DateTime.Now.ToString(), ConfirmPassword = "123456", Password = "123456", Email = "test4@ta.com", Firstname = "Test4", Gender = "F", Lastname = "Test4" };
            RegisterResponse actual = target.Register(registerReq);
        }
    }
}

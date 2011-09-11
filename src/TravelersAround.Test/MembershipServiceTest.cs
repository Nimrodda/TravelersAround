using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelersAround.Service;

namespace TravelersAround.Test
{
    [TestClass]
    public class MembershipServiceTest
    {
        [TestMethod]
        public void RegisterNewUser()
        {
            Membership ms = new Membership();
            ms.CreateUser("user2@bla.com", "123456", "user2@bla.com");
        }

        [TestMethod]
        public void ValidateExistingUser()
        {
            Membership ms = new Membership();
            Assert.IsTrue(ms.ValidateUser("user1", "123456"));
        }
    }
}

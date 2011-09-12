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
        public void RegisterNewTraveler()
        {
            MembershipService memberSvc = new MembershipService();
            
            var actual = memberSvc.Register("test1@ta.com", "123456", "123456", "Nimrod", "Dayan", "05/05/1982", "M");

            Assert.IsTrue(actual.Success);
        }

        [TestMethod]
        public void LoginValidUser()
        {
            MembershipService memberSvc = new MembershipService();
            var actual = memberSvc.Login("test1@ta.com", "123456");
            Assert.IsTrue(actual.Success);
        }
    }
}

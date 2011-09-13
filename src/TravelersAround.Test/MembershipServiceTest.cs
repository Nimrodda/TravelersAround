using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelersAround.Service;
using TravelersAround.Model;
using TravelersAround.Repository;
using TravelersAround.GeoCoding;

namespace TravelersAround.Test
{
    [TestClass]
    public class MembershipServiceTest
    {
        [TestMethod]
        public void RegisterNewTraveler()
        {
            DepenedencyRegistration.Register();
            MembershipService memberSvc = DepenedencyRegistration.Get<MembershipService>();

            var actual = memberSvc.Register("test1@ta.com", "123456", "123456", "Nimrod", "Dayan", "05/05/1982", "M");

            Assert.IsTrue(actual.Success);
        }

        [TestMethod]
        public void LoginValidUser()
        {
            //DepenedencyRegistration.Register();
            //MembershipService memberSvc = new MembershipService();//DepenedencyRegistration.Get<MembershipService>();
            //var actual = memberSvc.Login("test1@ta.com", "123456");
            //Assert.IsTrue(actual.Success);
        }
    }
}

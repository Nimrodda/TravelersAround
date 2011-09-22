using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelersAround.Service;
using TravelersAround.Model;
using TravelersAround.Repository;
using TravelersAround.GeoCoding;
using TravelersAround.DataContracts;

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
            RegisterRequest req = new RegisterRequest();
            var actual = memberSvc.Register(req);

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

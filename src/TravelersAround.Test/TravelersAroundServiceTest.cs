using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelersAround.Service;

namespace TravelersAround.Test
{
    [TestClass]
    public class TravelersAroundServiceTest
    {
        [TestMethod]
        public void TickTest()
        {
            DepenedencyRegistration.Register();
            TravelersAroundService svc = DepenedencyRegistration.Get<TravelersAroundService>();
            svc.Tick(new DataContracts.TickerRequest());
        }
    }
}

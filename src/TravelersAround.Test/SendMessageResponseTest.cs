using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelersAround.DataContracts;

namespace TravelersAround.Test
{
    [TestClass]
    public class SendMessageResponseTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            SendMessageResponse res = new SendMessageResponse();
            Assert.IsNotNull(res.ErrorMessage);
        }
    }
}

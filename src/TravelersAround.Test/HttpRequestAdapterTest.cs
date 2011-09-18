using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelersAround.ServiceProxy;
using System.Reflection;

namespace TravelersAround.Test
{
    [TestClass]
    public class HttpRequestAdapterTest
    {
        [TestMethod]
        public void ConstructQueryStringTest()
        {

            TravelersAroundServiceClientProxy a = new TravelersAroundServiceClientProxy("asdfsadf");
            a.ListMessages("inbox", 0, 10);
        }
    }
}

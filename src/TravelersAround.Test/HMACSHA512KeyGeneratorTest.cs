using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using System.Web;

namespace TravelersAround.Test
{
    [TestClass]
    public class HMACSHA512KeyGeneratorTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic["bla"] = 1;
            var b = dic.FirstOrDefault(a => a.Value == 2);
            
        }
    }
}

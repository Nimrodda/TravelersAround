using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelersAround.Model;

namespace TravelersAround.Test
{
    [TestClass]
    public class SQLiteCacheTest
    {
        [TestMethod]
        public void GetValue_Returns_Null()
        {
            SQLiteCache cache = new SQLiteCache();
            object actual = cache.GetValue("123123123");
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetKey_Returns_Null()
        {
            SQLiteCache cache = new SQLiteCache();
            object actual = cache.GetKey("123123123");
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void Remove_Returns_Zero()
        {
            SQLiteCache cache = new SQLiteCache();
            int actual = cache.Remove("asdasd");
            Assert.IsTrue(actual == 0);
        }

    }
}

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelersAround.Repository;
using TravelersAround.Model.Entities;
using TravelersAround.GeoCoding;

namespace TravelersAround.Test
{
    [TestClass]
    public class GeoCoderTest
    {
        
        [TestMethod]
        public void ConvertIPAddressToGeoCoordinatesTest()
        {
            GeoCoder geo = new GeoCoder();
            var coords = geo.ConvertIPAddressToGeoCoordinates("80.221.20.181");
        }
    }
}

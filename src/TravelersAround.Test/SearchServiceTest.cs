using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelersAround.Repository;
using TravelersAround.Model.Entities;

namespace TravelersAround.Test
{
    [TestClass]
    public class SearchServiceTest
    {
        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        [TestMethod]
        public void StoredProcedureTest()
        {
            EFLocationDeterminator ld = new EFLocationDeterminator();
            double travelerLat = 60.167799;
            double travelerLon = 24.92825;
            int distance = 10;
            var results = ld.FindNearByTravelers(distance, travelerLat, travelerLon);
            Assert.IsNotNull(results);
        }
    }
}

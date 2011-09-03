using TravelersAround.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.EntityClient;
using TravelersAround.Model.Entities;
using System.Data.Objects;
using System.Linq.Expressions;

namespace TravelersAround.Test
{
    
    
    [TestClass()]
    public class EFRepositoryTest
    {
        [TestMethod()]
        public void InsertTraveler()
        {
            EFRepository r = new EFRepository();

            Traveler t = new Traveler
            {
                Birthdate = DateTime.Now,
                Gender = "M",
                Firstname = "Nimrod",
                Lastname = "Dayan"
            };

            r.Add<Traveler>(t);
            Assert.IsTrue(r.Commit().Succeeded);                
            
        }

        [TestMethod()]
        public void UpdateTraveler()
        {
            EFRepository r = new EFRepository();
            var t = r.FindBy<Traveler>(x => x.Firstname == "Nimrod");
            t.Firstname = "Nimolodo";

            Assert.IsTrue(r.Commit().Succeeded);

        }

        [TestMethod()]
        public void DeleteTraveler()
        {
            EFRepository r = new EFRepository();
            var t = r.FindBy<Traveler>(x => x.Firstname == "Nimrod");
            r.Remove<Traveler>(t);

            Assert.IsTrue(r.Commit().Succeeded);

        }
    }
}

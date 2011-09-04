using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelersAround.Model.Services;
using TravelersAround.Model;
using TravelersAround.Repository;
using TravelersAround.Model.Entities;
using Moq;

namespace TravelersAround.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Traveler traveler = new Traveler { Firstname = "Nimrod" };
            var repo = new Mock<IRepository>();
            repo.Setup<Traveler>(r => r.FindBy<Traveler>(t => t.TravelerID == new Guid("d0293215-1dd7-e011-ae1d-206a8a339500"))).Returns(traveler);
            //IRepository repo = new EFRepository();
            MessageService ms = new MessageService(repo.Object);
            var messages = ms.ListMessages(new Guid("d0293215-1dd7-e011-ae1d-206a8a339500"), FolderType.Inbox);
            Console.WriteLine(messages.FirstOrDefault().Message.Body);
            Assert.IsNotNull(messages.FirstOrDefault().Message);
        }
    }
}

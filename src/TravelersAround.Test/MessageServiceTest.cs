using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TravelersAround.Model.Entities;
using TravelersAround.Model;
using TravelersAround.Model.Services;
using TravelersAround.Model.Exceptions;

namespace TravelersAround.Test
{
    [TestClass]
    public class MessageServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(TravelerNotFoundException))]
        public void ReadMessage_With_Invalid_TravelerID()
        {
            var mockRepo = new Mock<IRepository>();
            Traveler expected = new Traveler { Firstname = "Nimrod" };
            mockRepo.Setup(m => m.FindBy<Traveler>(t => t.TravelerID == It.IsAny<Guid>())).Returns(expected);
            MessageService ms = new MessageService(mockRepo.Object);
            var actual = ms.ReadMessage(new Guid("c085ce65-9fd8-e011-813c-206a8a339500"), It.IsAny<Guid>());
            mockRepo.VerifyAll();
            Assert.AreEqual(expected, actual);

            
        }

        [TestMethod]
        public void TestMethod2()
        {
            var mockTraveler = new Mock<Traveler>(MockBehavior.Strict);
            Message expected = new Message { Body = "bbla" };
            mockTraveler.Setup(a => a.ReadMessage(It.IsAny<Guid>())).Returns(expected);
            Message actual = mockTraveler.Object.ReadMessage(It.IsAny<Guid>());
            Assert.AreEqual(expected, actual);
        }

    }
}

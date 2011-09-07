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
using TravelersAround.Model.Exceptions;
using System.Diagnostics;

namespace TravelersAround.Test
{
    [TestClass]
    public class MessageServiceIntegrationTest
    {
        [TestMethod]
        public void ListMessagesTest()
        {
            IRepository repo = new EFRepository();
            MessageService ms = new MessageService(repo);
            Guid traveler = new Guid("d0293215-1dd7-e011-ae1d-206a8a339500");
            var msgs = ms.ListMessages(traveler, FolderType.Inbox, 0, 5);
            foreach (var item in msgs)
            {
                Trace.WriteLine(String.Format("{0} {1} {2}", item.Message.Subject, item.Message.Author.Fullname, item.Message.SentDate));
                
            }
        }
            
        [TestMethod]
        public void SendMessageTest()
        {
            IRepository repo = new EFRepository(); 
            MessageService ms = new MessageService(repo);
            Guid author = new Guid("37519c07-acd4-e011-ad03-206a8a339500");
            Guid[] recipients = new Guid[] { 
                new Guid("d0293215-1dd7-e011-ae1d-206a8a339500"), 
                new Guid("45d03989-9fd8-e011-813c-206a8a339500") };

            ms.SendMessage("blablabla", "blablablbla?", author, recipients);
        }

        [TestMethod]
        public void ReadMessageTest()
        {
            IRepository repo = new EFRepository();
            MessageService ms = new MessageService(repo);
            var message = ms.ReadMessage(new Guid("c085ce65-9fd8-e011-813c-206a8a339500"), new Guid("37519c07-acd4-e011-ad03-206a8a339500"));
            Assert.IsNotNull(message);
        }

        [TestMethod]
        [ExpectedException(typeof(TravelerNotFoundException))]
        public void SendMessage_With_Invalid_AuthorID()
        {
            IRepository repo = new EFRepository();
            MessageService ms = new MessageService(repo);
            Guid author = new Guid("11111111-1111-1111-1111-111111111111");
            Guid[] recipients = new Guid[] { 
                new Guid("d0293215-1dd7-e011-ae1d-206a8a339500"), 
                new Guid("45d03989-9fd8-e011-813c-206a8a339500") };

            ms.SendMessage("blablabla", "blablablbla?", author, recipients);
        }

        [TestMethod]
        [ExpectedException(typeof(TravelerNotFoundException))]
        public void SendMessage_With_Invalid_Recipients()
        {
            IRepository repo = new EFRepository();
            MessageService ms = new MessageService(repo);
            Guid author = new Guid("37519c07-acd4-e011-ad03-206a8a339500");
            Guid[] recipients = new Guid[] { new Guid("11111111-1111-1111-1111-111111111111") };
                

            ms.SendMessage("blablabla", "blablablbla?", author, recipients);
        }

        [TestMethod]
        [ExpectedException(typeof(MessageDoesNotExistException))]
        public void ReadMessage_With_Invalid_MessageID()
        {
            IRepository repo = new EFRepository();
            MessageService ms = new MessageService(repo);
            var message = ms.ReadMessage(new Guid("11111111-1111-1111-1111-111111111111"), new Guid("37519c07-acd4-e011-ad03-206a8a339500"));
            Assert.IsNotNull(message);
        }

        [TestMethod]
        [ExpectedException(typeof(TravelerNotFoundException))]
        public void ReadMessage_With_Invalid_TravelerID()
        {
            IRepository repo = new EFRepository();
            MessageService ms = new MessageService(repo);
            var message = ms.ReadMessage(new Guid("c085ce65-9fd8-e011-813c-206a8a339500"), new Guid("11111111-1111-1111-1111-111111111111"));
            Assert.IsNotNull(message);
        }
    }
}

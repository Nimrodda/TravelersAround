using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Model.Entities;
using TravelersAround.Model.Factories;
using TravelersAround.Model.Exceptions;
using TravelersAround.Infrastructure;

namespace TravelersAround.Model.Services
{
    public class MessageService
    {
        private IRepository _repository;

        public MessageService(IRepository repository)
        {
            _repository = repository;
        }

        public PagedList<TravelerMessage> ListMessages(Guid travelerID, FolderType folder, int index = 0, int count = 10)
        {
            Traveler traveler = _repository.FindBy<Traveler>(t => t.TravelerID == travelerID);
            if (traveler == null) throw new TravelerNotFoundException();
            if (traveler.HasMessagesInFolder(folder))
                return traveler.Messages.Where(m => m.FolderID == (int)folder).ToPagedList(index, count);
            else
                return new PagedList<TravelerMessage>();
        }

        public void SendMessage(string subject, string body, Guid authorID, Guid[] recipientIDs)
        {
            var recipients = _repository.FindAllBy<Traveler>(t => recipientIDs.Contains(t.TravelerID));
            if (recipients == null || recipients.Count() != recipientIDs.Count()) throw new TravelerNotFoundException();
            Traveler author = _repository.FindBy<Traveler>(r => r.TravelerID == authorID);
            if (author == null) throw new TravelerNotFoundException();
            Message message = MessageFactory.CreateMessageFrom(subject, body, author, recipients);          
            _repository.Add<Message>(message);
            _repository.Commit();
        }

        public TravelerMessage ReadMessage(Guid messageID, Guid travelerID)
        {
            Traveler traveler = _repository.FindBy<Traveler>(t => t.TravelerID == travelerID);
            if (traveler == null) throw new TravelerNotFoundException();
            TravelerMessage message = traveler.ReadMessage(messageID);
            _repository.Save<TravelerMessage>(message);
            _repository.Commit();
            return message;
        }

    }
}

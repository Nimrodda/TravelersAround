using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Model.Entities;
using TravelersAround.Model.Factories;
using TravelersAround.Model.Exceptions;

namespace TravelersAround.Model.Services
{
    public class MessageService
    {
        private IRepository _repository;

        public MessageService(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<TravelerMessage> ListMessages(Guid travelerID, FolderType folder, int index = 0, int count = 10)
        {
            Traveler traveler = _repository.FindBy<Traveler>(t => t.TravelerID == travelerID);
            if (traveler == null) throw new TravelerNotFoundException();
            if (traveler.HasMessagesInFolder(folder))
                return traveler.Messages.Where(m => m.FolderID == (int)folder).Skip(index).Take(count);
            else 
                return null;
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

        public Message ReadMessage(Guid messageID, Guid travelerID)
        {
            Traveler traveler = _repository.FindBy<Traveler>(t => t.TravelerID == travelerID);
            if (traveler == null) throw new TravelerNotFoundException();
            Message message = traveler.ReadMessage(messageID);
            _repository.Save<Message>(message);
            _repository.Commit();
            return message;
        }

    }
}

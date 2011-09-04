using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Model.Entities;
using TravelersAround.Model.Factories;

namespace TravelersAround.Model.Services
{
    public class MessageService
    {
        private IRepository _repository;

        public enum Operation
        {
            CreateMessage,
            ReadMessage,
            DeleteMessage
        }

        public MessageService(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<TravelerMessage> ListMessages(Guid travelerID, FolderType folder)
        {
            Traveler traveler = _repository.FindBy<Traveler>(t => t.TravelerID == travelerID);
            return traveler.Messages.Where(m => m.Folder.FolderID == (int)folder);
        }

        public OperationStatus SendMessage(Message message, Guid recipientID)
        {
            OperationStatus operStat;

            try
            {
                Traveler recipient = _repository.FindBy<Traveler>(r => r.TravelerID == recipientID);
                message.AddRecipient(recipient);
                operStat = OperationStatusFactory.CreateFromBoolean(_repository.Commit() > 0);
            }
            catch (Exception ex)
            {
                operStat = OperationStatusFactory.CreateFromException(ex);
            }

            return operStat;
        }

        public Message ReadMessage(Guid messageID, Guid travelerID)
        {
            Traveler traveler = _repository.FindBy<Traveler>(t => t.TravelerID == travelerID);
            Message message = _repository.FindBy<Message>(m => m.MessageID == messageID);

            traveler.MarkMessageRead(message);
            
            _repository.Commit();
            return message;
        }

    }
}

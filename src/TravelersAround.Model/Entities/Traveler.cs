using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelersAround.Model.Entities
{
    public class Traveler : IAggregateRoot
    {
        public Guid TravelerID { get; internal set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public string StatusMessage { get; set; }
        public string Gender { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public bool IsAvailable { get; set; }
        public virtual IList<Traveler> Relationships { get; set; }
        public virtual IList<TravelerMessage> Messages { get; set; }

        public void AddFriend(Traveler friend)
        {
            if (Relationships == null)
            {
                Relationships = new List<Traveler>();
            }
            if (!Relationships.Contains(friend))
            {
                Relationships.Add(friend);
            }
            else
            {
                throw new ApplicationException(String.Format("Friend with ID:{0} already exists in the friend list of traveler {1}", friend.TravelerID, TravelerID));
            }
        }

        public void RemoveFriend(Traveler friend)
        {
            if (Relationships == null)
            {
                throw new ApplicationException(String.Format("Cannot remove friend, no friends exist in the list of traveler {0}", TravelerID));
            }
            if (Relationships.Contains(friend))
            {
                Relationships.Remove(friend);
            }
            else
            {
                throw new ApplicationException(String.Format("Cannot remove friend with ID:{0}, no such friend exists in the friend list of traveler {1}", friend.TravelerID, TravelerID));
            }
        }

        public void MarkMessageRead(Message message)
        {
            if (Messages == null)
            {
                throw new ApplicationException(String.Format("Cannot mark message read, no messages exists for traveler {0}", TravelerID));
            }
            TravelerMessage travelerMsg = Messages.FirstOrDefault(m => m.MessageID == message.MessageID);
            if (travelerMsg != null)
            {
                travelerMsg.IsRead = true;
            }
            else
            {
                throw new ApplicationException(String.Format("Cannot mark message ID: {0} as read, no such message exists in the list of traveler {1}", message.MessageID, TravelerID));
            }
        }

        public void MarkMessageUnread(Message message)
        {
            if (Messages == null)
            {
                throw new ApplicationException(String.Format("Cannot mark message read, no messages exists for traveler {0}", TravelerID));
            }
            TravelerMessage travelerMsg = Messages.FirstOrDefault(m => m.MessageID == message.MessageID);
            if (travelerMsg != null)
            {
                travelerMsg.IsRead = false;
            }
            else
            {
                throw new ApplicationException(String.Format("Cannot mark message ID: {0} as unread, no such message exists in the list of traveler {1}", message.MessageID, TravelerID));
            }
        }

        public void SendMessage(Traveler recipient)
        {

        }

        public Message ReadMessage(TravelerMessage travelerMessage)
        {
            TravelerMessage message = Messages.FirstOrDefault(m => m.MessageID == travelerMessage.MessageID);
            if (message != null)
            {
                return message.Message;
            }
            else
            {
                throw new ApplicationException(String.Format("No message with ID:{0} exist for this user", travelerMessage.MessageID));
            }
        }

    }
}

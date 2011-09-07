using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Model.Exceptions;

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

        public string Fullname
        {
            get
            {
                return String.Format("{0} {1}", Firstname, Lastname);
            }
        }

        public void AddFriend(Traveler friend)
        {
            if (!HasFriends())
            {
                Relationships = new List<Traveler>();
            }
            if (!Relationships.Contains(friend))
            {
                Relationships.Add(friend);
            }
            else
            {
                throw new FriendAlreadyExistsException();
            }
        }

        public void RemoveFriend(Traveler friend)
        {
            if (!HasFriends())
            {
                throw new FriendListEmptyException();
            }
            if (Relationships.Contains(friend))
            {
                Relationships.Remove(friend);
            }
            else
            {
                throw new FriendDoesNotExistException();
            }
        }

        public bool HasMessages()
        {
            return Messages != null;
        }

        public bool HasMessagesInFolder(FolderType folder)
        {
            return HasMessages() && Messages.Count(f => f.FolderID == (int)folder) > 0;
        }

        public int GetHowManyNewMessages()
        {
            if (HasMessages())
            {
                return Messages.Count(m => m.IsRead == false);
            }
            else
            {
                throw new NoMessagesExistException();
            }

        }

        public bool HasFriends()
        {
            return Relationships != null;
        }

        public virtual Message ReadMessage(Guid messageID)
        {
            if (HasMessages())
            {
                TravelerMessage travelerMessage = Messages.FirstOrDefault(m => m.MessageID == messageID);
                if (travelerMessage != null)
                {
                    MarkMessageRead(travelerMessage);
                    return travelerMessage.Message;
                }
                else
                {
                    throw new MessageDoesNotExistException();
                }
                
            }
            else
            {
                throw new NoMessagesExistException();
            }
        }

        public void MarkMessageRead(TravelerMessage travelerMessage)
        {
            travelerMessage.IsRead = true;
        }

        public void MarkMessageUnread(TravelerMessage travelerMessage)
        {
            travelerMessage.IsRead = false;
        }
    }
}

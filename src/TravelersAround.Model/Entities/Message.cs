using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Model.Factories;

namespace TravelersAround.Model.Entities
{
    public class Message : IAggregateRoot
    {
        public Guid MessageID { get; internal set; }
        public Guid AuthorID { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime SentDate { get; set; }
        public virtual Traveler Author { get; set; }
        public virtual IList<TravelerMessage> Recipients { get; set; }

        public bool HasRecipients()
        {
            return Recipients != null;
        }

        public void AddRecipient(Traveler recipient)
        {
            if (!HasRecipients())
            {
                Recipients = new List<TravelerMessage>();
            }
            Recipients.Add(TravelerMessageFactory.CreateTravelerMessageFrom(recipient, this));
        }


    }
}

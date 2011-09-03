using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelersAround.Model.Entities
{
    public class Message : IAggregateRoot
    {
        public Guid MessageID { get; set; }
        public Guid AuthorID { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime SentDate { get; set; }
        public virtual Traveler Author { get; set; }
        public virtual IList<Traveler> Owners { get; set; }
    }
}

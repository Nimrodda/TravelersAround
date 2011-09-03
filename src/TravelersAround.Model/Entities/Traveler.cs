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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelersAround.Model.Entities
{
    public class TravelerMessage :IAggregateRoot
    {
        public Guid MessageID { get; set; }
        public Guid TravelerID { get; set; }
        public bool IsRead { get; set; }
        public int FolderID { get; set; }
        public virtual Traveler Traveler { get; set; }
        public virtual Message Message { get; set; }
    }
}

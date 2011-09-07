using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Model.Entities;

namespace TravelersAround.Model.Factories
{
    public static class FolderFactory
    {
        public static int CreateFolderFrom(Traveler traveler, Message message)
        {
            if (traveler.TravelerID == message.AuthorID)
            {
                return (int)FolderType.Sent;
                
            }
            else
            {
                return (int)FolderType.Inbox;
            }
        }
    }
}

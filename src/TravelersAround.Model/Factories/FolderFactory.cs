using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Model.Entities;

namespace TravelersAround.Model.Factories
{
    public static class FolderFactory
    {
        public static Folder CreateFolderFrom(Traveler traveler, Message message)
        {
            if (traveler.TravelerID == message.AuthorID)
            {
                return new Folder
                {
                    FolderID = (int)FolderType.Sent
                };
            }
            else
            {
                return new Folder
                {
                    FolderID = (int)FolderType.Inbox
                };
            }
        }
    }
}

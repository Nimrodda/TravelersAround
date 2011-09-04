using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelersAround.Model.Entities
{
    public enum FolderType
    {
        Inbox = 1,
        Sent = 2,
        Trash = 3
    }

    public class Folder
    {
        public int FolderID { get; set; }
        public string Name { get; set; }
    }
}

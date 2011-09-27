using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TravelersAround.DataContracts.Views;
using System.Collections.Specialized;
using TravelersAround.Infrastructure;

namespace TravelersAround.ServiceProxy.ViewModels
{
    public class MessagesListView : BaseView
    {
        public string Folder { get; set; }

        public PagedList<MessageView> MessagesList { get; set; }

    }

    public class MessageSendView : BaseView
    {
        public List<DropDownListItem> FriendsDropDownList { get; set; }

        public string RecipientName { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public string RecipientID { get; set; }
    }

    public class MessageReadView : BaseView
    {
        public string ReturnToFolder { get; set; }

        public MessageView Message { get; set; }
    }

    public class MessageDeleteView : BaseView
    {
        [Required]
        public string[] MessageIDs { get; set; }

        public string Folder { get; set; }
    }

}

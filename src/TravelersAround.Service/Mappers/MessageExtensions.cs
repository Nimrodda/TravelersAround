using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.DataContracts.Views;
using TravelersAround.Model.Entities;

namespace TravelersAround.Service.Mappers
{
    public static class MessageExtensions
    {
        public static MessageView ConvertToMessageView(this TravelerMessage travelerMessage)
        {
            return new MessageView
            {
                Body = travelerMessage.Message.Body,
                IsRead = travelerMessage.IsRead,
                MessageID = travelerMessage.MessageID,
                SenderName = travelerMessage.Message.Author.Fullname,
                SentDate = travelerMessage.Message.SentDate,
                Subject = travelerMessage.Message.Subject,
                RecipientsNames = new string[] { travelerMessage.Traveler.Firstname }
            };
        }

        public static IList<MessageView> ConvertToMessageViewList(this IEnumerable<TravelerMessage> travelerMessages)
        {
            IList<MessageView> msgViewList = new List<MessageView>();
            foreach (TravelerMessage travMsg in travelerMessages)
            {
                msgViewList.Add(travMsg.ConvertToMessageView());
            }
            return msgViewList;
        }
    }
}

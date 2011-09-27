using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.DataContracts.Views;
using TravelersAround.Model.Entities;
using TravelersAround.Infrastructure;

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
                MessageID = travelerMessage.MessageID.ToString(),
                SenderName = travelerMessage.Message.Author.Fullname,
                SentDate = travelerMessage.Message.SentDate.ToString(),
                Subject = travelerMessage.Message.Subject,
                RecipientsNames = travelerMessage.Message.Recipients.Where(x => x.TravelerID != travelerMessage.TravelerID).Select(t => t.Traveler.Fullname).ToList(),
                SenderID = travelerMessage.Message.AuthorID.ToString()
            };
        }

        public static PagedList<MessageView> ConvertToMessageViewPagedList(this PagedList<TravelerMessage> travelerMessages)
        {
            PagedList<MessageView> msgViewPagedList = new PagedList<MessageView>();
            msgViewPagedList.HasNext = travelerMessages.HasNext;
            msgViewPagedList.HasPrevious = travelerMessages.HasPrevious;
            msgViewPagedList.TotalEntitiesCount = travelerMessages.TotalEntitiesCount;
            msgViewPagedList.TotalPageCount = travelerMessages.TotalPageCount;
            msgViewPagedList.Entities = new List<MessageView>();
            if (travelerMessages != null)
            {
                foreach (TravelerMessage travMsg in travelerMessages.Entities)
                {
                    msgViewPagedList.Entities.Add(travMsg.ConvertToMessageView());
                }
            }
            return msgViewPagedList;
        }
    }
}

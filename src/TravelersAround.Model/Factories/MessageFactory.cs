using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Model.Entities;

namespace TravelersAround.Model.Factories
{
    public static class MessageFactory
    {
        public static Message CreateMessageFrom(string subject, string body, Traveler author, IEnumerable<Traveler> recipients)
        {
            Message message = new Message
            {
                Author = author,
                AuthorID = author.TravelerID,
                Body = body,
                Subject = subject,
                SentDate = DateTime.Now
            };
            message.AddRecipient(author);
            foreach (var recipeient in recipients)
	        {
                message.AddRecipient(recipeient);
	        }
            
            return message;

        }
    }
}

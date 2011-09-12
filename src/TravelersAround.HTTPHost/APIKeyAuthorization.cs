using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using TravelersAround.Service;
using System.Runtime.Serialization.Json;

namespace TravelersAround.HTTPHost
{
    public class APIKeyAuthorization : ServiceAuthorizationManager
    {
        private const string APIKEY = "APIKey";

        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            string key = GetAPIKey(operationContext);

            if (APIKeyRepository.IsValidAPIKey(key))
            {
                return true;
            }
            else
            {
                CreateErrorReply(operationContext, key);
                
                return false;
            }
        }

        private string GetAPIKey(OperationContext operationContext)
        {
            // Get the request message
            var request = operationContext.RequestContext.RequestMessage;

            // Get the HTTP Request
            var requestProp = (HttpRequestMessageProperty)request.Properties[HttpRequestMessageProperty.Name];

            // Get the query string
            NameValueCollection queryParams = HttpUtility.ParseQueryString(requestProp.QueryString);

            // Return the API key (if present, null if not)
            return queryParams[APIKEY];
        }

        private static void CreateErrorReply(OperationContext operationContext, string key)
        {
            using (Message reply = Message.CreateMessage(MessageVersion.None, 
                null, new APIKeyResponse { Message = "Access Denied", Success = false }, 
                new DataContractJsonSerializer(typeof(APIKeyResponse))))
            {
                //Formating response in JSON
                reply.Properties[WebBodyFormatMessageProperty.Name] = new WebBodyFormatMessageProperty(WebContentFormat.Json);

                //Response headers
                HttpResponseMessageProperty responseProp = new HttpResponseMessageProperty
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    StatusDescription = String.Format("Access denied", key)
                };
                responseProp.Headers[HttpResponseHeader.ContentType] = "application/json";

                reply.Properties[HttpResponseMessageProperty.Name] = responseProp;
                
                operationContext.RequestContext.Reply(reply);
                operationContext.RequestContext = null;
            }
                
                    
        }

        
        
    }
}
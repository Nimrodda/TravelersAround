using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Description;

namespace TravelersAround.HTTPHost
{
    public class WebHttpBehaviorExtension : WebHttpBehavior
    {
        protected override void AddServerErrorHandlers(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.ChannelDispatcher.ErrorHandlers.Clear();
            endpointDispatcher.ChannelDispatcher.ErrorHandlers.Add(new ErrorHandlerExtension());
        }
    }
}
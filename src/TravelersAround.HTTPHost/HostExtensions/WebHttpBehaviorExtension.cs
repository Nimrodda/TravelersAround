using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace TravelersAround.HTTPHost
{
    public class WebHttpBehaviorExtension : WebHttpBehavior
    {
        protected override void AddServerErrorHandlers(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            //endpointDispatcher.ChannelDispatcher.ErrorHandlers.Clear();
            //endpointDispatcher.ChannelDispatcher.ErrorHandlers.Add(new ErrorHandlerExtension());
            base.AddServerErrorHandlers(endpoint, endpointDispatcher);
        }

        public override void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            base.ApplyDispatchBehavior(endpoint, endpointDispatcher);
            endpointDispatcher.DispatchRuntime.InstanceProvider = new NinjectInstanceProvider(endpoint.Contract.ContractType);

        }
    }
}
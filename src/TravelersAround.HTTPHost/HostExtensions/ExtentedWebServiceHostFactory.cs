using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Activation;
using System.ServiceModel;

namespace TravelersAround.HTTPHost
{
    public class ExtentedWebServiceHostFactory : WebServiceHostFactory
    {
        public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses)
        {
            var svchost = base.CreateServiceHost(constructorString, baseAddresses);
            svchost.Description.Endpoints[0].Behaviors.Add(new WebHttpBehaviorExtension());
            return svchost;
        }
    }
}
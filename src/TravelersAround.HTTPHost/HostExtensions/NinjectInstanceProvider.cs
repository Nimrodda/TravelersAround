using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Dispatcher;
using TravelersAround.Contracts;
using TravelersAround.Service;

namespace TravelersAround.HTTPHost
{
    public class NinjectInstanceProvider : IInstanceProvider
    {
        private Type _serviceType;

        public NinjectInstanceProvider(Type serviceType)
        {
            _serviceType = serviceType;
        }

        //IInstanceProvider members
        public object GetInstance(System.ServiceModel.InstanceContext instanceContext, System.ServiceModel.Channels.Message message)
        {
            return DepenedencyRegistration.Get(_serviceType);
        }

        public object GetInstance(System.ServiceModel.InstanceContext instanceContext)
        {
            return DepenedencyRegistration.Get(_serviceType);
        }

        public void ReleaseInstance(System.ServiceModel.InstanceContext instanceContext, object instance)
        {
            return;
        }
    }
}
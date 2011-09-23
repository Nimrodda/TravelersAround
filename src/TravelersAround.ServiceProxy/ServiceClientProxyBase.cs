using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Reflection;

namespace TravelersAround.ServiceProxy
{
    public abstract class ServiceClientProxyBase
    {
        protected readonly string _serviceBaseUrl;

        public ServiceClientProxyBase(string serviceBaseUrl)
        {
            _serviceBaseUrl = serviceBaseUrl;
        }

    }
}

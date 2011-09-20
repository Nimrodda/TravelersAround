using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace TravelersAround.Service
{
    public static class DepenedencyRegistration
    {
        public static IKernel Kernel { get; private set; }
        
        public static void Register()
        {
            log4net.Config.XmlConfigurator.Configure();
            Kernel = new StandardKernel(new CustomModule());
        }

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }

        public static object Get(Type type)
        {
            return Kernel.Get(type);
        }
    }
}

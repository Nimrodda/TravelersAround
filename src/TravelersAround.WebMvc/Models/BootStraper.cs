using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace TravelersAround.WebMvc.Models
{
    public static class BootStraper
    {
        public static IKernel Kernel { get; private set; }
        
        public static void RegisterDependencies()
        {
            Kernel = new StandardKernel(new WebMvcModule());
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

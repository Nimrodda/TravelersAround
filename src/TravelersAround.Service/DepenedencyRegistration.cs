using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace TravelersAround.Service
{
    public static class DepenedencyRegistration
    {
        public static void Register()
        {
            IKernel kernel = new StandardKernel(new CustomModule());
        }
    }
}

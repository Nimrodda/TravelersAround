using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;

namespace TravelersAround.WebMvc.Models
{
    public class IoCControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType != null)
                return (new StandardKernel(new WebMvcModule()).Get(controllerType)) as IController;
            else
                return base.GetControllerInstance(requestContext, controllerType);
        }
    }
}
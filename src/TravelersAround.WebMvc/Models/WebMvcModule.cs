using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using TravelersAround.ServiceProxy;
using TravelersAround.Contracts;
using System.Web;

namespace TravelersAround.WebMvc.Models
{
    public class WebMvcModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITravelersAroundServiceFacade>().To<TravelersAroundServiceFacade>();
            Bind<IMembershipServiceFacade>().To<MembershipServiceFacade>();
            Bind<IFormsAuthenticationService>().To<FormsAuthenticationService>();
            Bind<ITravelersAroundService>().To<TravelersAroundServiceClientProxy>().WithConstructorArgument("serviceBaseUrl", "").WithConstructorArgument("apiKey", "");
            Bind<IMembershipService>().To<MembershipServiceClientProxy>().WithConstructorArgument("serviceBaseUrl", ""); 
            
        }
    }
}

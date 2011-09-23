using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using TravelersAround.ServiceProxy;
using TravelersAround.Contracts;
using System.Web;
using System.Configuration;

namespace TravelersAround.WebMvc.Models
{
    public class WebMvcModule : NinjectModule
    {
        public override void Load()
        {
            string apiKey = HttpContext.Current.User.Identity.Name;
            string serviceBaseUrl = ConfigurationManager.AppSettings["ServiceBaseUrl"];

            Bind<ITravelersAroundServiceFacade>().To<TravelersAroundServiceFacade>();
            Bind<IMembershipServiceFacade>().To<MembershipServiceFacade>();
            Bind<IFormsAuthenticationService>().To<FormsAuthenticationService>();
            Bind<ITravelersAroundService>().To<TravelersAroundServiceClientProxy>().WithConstructorArgument("serviceBaseUrl", serviceBaseUrl)
                                                                                .WithConstructorArgument("apiKey", apiKey);

            Bind<IMembershipService>().To<MembershipServiceClientProxy>().WithConstructorArgument("serviceBaseUrl", serviceBaseUrl); 
            
        }
    }
}

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
            

            Bind<ITravelersAroundServiceFacade>().To<TravelersAroundServiceFacade>();
            Bind<IMembershipServiceFacade>().To<MembershipServiceFacade>();
            Bind<IFormsAuthenticationService>().To<FormsAuthenticationService>();
            Bind<ITravelersAroundService>().To<TravelersAroundServiceClientProxy>()
                .WithConstructorArgument("serviceBaseUrl", ConfigurationManager.AppSettings["TravelersAroundServiceUrl"])
                .WithConstructorArgument("apiKey", apiKey);

            Bind<IMembershipService>().To<MembershipServiceClientProxy>()
                .WithConstructorArgument("serviceBaseUrl", ConfigurationManager.AppSettings["MembershipServiceUrl"]); 
            
        }
    }
}

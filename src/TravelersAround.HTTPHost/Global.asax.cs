using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using System.ServiceModel.Activation;
using TravelersAround.Service;

namespace TravelersAround.HTTPHost
{
    public class Global : HttpApplication
    {
        private void RegisterRoutes()
        {
            // Edit the base address of Service1 by replacing the "Service1" string below
            RouteTable.Routes.Add(new ServiceRoute("MembershipService", new WebServiceHostFactory(), typeof(MembershipService)));
            RouteTable.Routes.Add(new ServiceRoute("TravelersAroundService", new WebServiceHostFactory(), typeof(TravelersAroundService)));
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
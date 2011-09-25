using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using System.ServiceModel.Activation;
using TravelersAround.Service;
using System.Threading;
using System.Configuration;

namespace TravelersAround.HTTPHost
{
    public class Global : HttpApplication
    {
        private Timer _setIdleUsersTimer;
        private Timer _cacheCleanUpTimer;

        private void RegisterRoutes()
        {
            RouteTable.Routes.Add(new ServiceRoute("MembershipService", new ExtentedWebServiceHostFactory(), typeof(MembershipService)));
            RouteTable.Routes.Add(new ServiceRoute("TravelersAroundService", new ExtentedWebServiceHostFactory(), typeof(TravelersAroundService)));
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes();
            DepenedencyRegistration.Register();

            APIKeyService apiKeySvc = new APIKeyService();

            //Timer for setting all idle users as offline
            _setIdleUsersTimer = new Timer(s => { System.Diagnostics.Debug.WriteLine(DateTime.Now); apiKeySvc.MarkIdleUsersOffline(); }, null, TimeSpan.Zero, TimeSpan.FromMinutes(APIKeyService.IdleTime));

            //Timer for removing all users that have been idle more than X hours, as defined by the service
            _cacheCleanUpTimer = new Timer(s => apiKeySvc.IdleUsersCleanUp(), null, TimeSpan.FromHours(APIKeyService.IdleUsersCleanUpTime), TimeSpan.FromHours(APIKeyService.IdleUsersCleanUpTime));
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            string sessionId = Session.SessionID;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            APIKeyService.CurrentTravelerIPAddress = GetIPAddressAsString();
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

        private string GetIPAddressAsString()
        {
            if (HttpContext.Current != null && !HttpContext.Current.Request.IsLocal)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return null;
            }
        }
    }
}
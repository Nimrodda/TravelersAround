using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using TravelersAround.Contracts;
using TravelersAround.DataContracts;

namespace TravelersAround.ServiceProxy
{
    public class MembershipServiceClientProxy : ServiceClientProxyBase, IMembershipService
    {
        public MembershipServiceClientProxy(string serviceBaseUrl) : base(serviceBaseUrl) { }

        public RegisterResponse Register(RegisterRequest registerReq)
        {
            return HttpRequestAdapter.WebHttpPostRequest<RegisterResponse>(_serviceBaseUrl, MethodBase.GetCurrentMethod().Name, registerReq);
        }

        public LoginResponse Login(LoginRequest loginReq)
        {
            return HttpRequestAdapter.WebHttpPostRequest<LoginResponse>(_serviceBaseUrl, MethodBase.GetCurrentMethod().Name, loginReq);
        }


        public LogoutResponse Logout(string apiKey)
        {
            string queryString = String.Concat("?apikey=", apiKey);
            return HttpRequestAdapter.WebHttpRequest<LogoutResponse>(_serviceBaseUrl, "Logout", queryString, HttpRequestAdapter.Method.GET);
        }
    }
}

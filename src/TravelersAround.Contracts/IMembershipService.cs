using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using TravelersAround.DataContracts;

namespace TravelersAround.Contracts
{
    [ServiceContract]
    public interface IMembershipService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
                UriTemplate = "Register/",
                BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        RegisterResponse Register(RegisterRequest registerReq);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Login/", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        LoginResponse Login(LoginRequest loginReq);
    }
}

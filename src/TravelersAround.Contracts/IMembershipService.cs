﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using TravelersAround.DataContracts;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WCF;

namespace TravelersAround.Contracts
{
    [ServiceContract]
    [ValidationBehavior]
    public interface IMembershipService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
                UriTemplate = "Register/",
                BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        RegisterResponse Register(RegisterRequest registerReq);

        [OperationContract]
        [FaultContract(typeof(ValidationFault))]
        [WebInvoke(Method = "POST", UriTemplate = "Login/", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        LoginResponse Login(LoginRequest loginReq);

        [OperationContract]
        [WebGet(UriTemplate = "Logout?apiKey={apiKey}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        LogoutResponse Logout(string apiKey);
    }
}

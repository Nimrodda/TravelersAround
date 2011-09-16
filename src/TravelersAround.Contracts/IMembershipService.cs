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
                UriTemplate = @"Register?email={email}&password={password}&confirmPassword={confirmPassword}&firstname={firstname}&lastname={lastname}&birthdate={birthdate}&gender={gender}",
                BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        RegisterResponse Register(string email, string password, string confirmPassword, string firstname, string lastname, string birthdate, string gender);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = @"Login?email={email}&password={password}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        LoginResponse Login(string email, string password);
    }
}

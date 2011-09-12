using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Dispatcher;

namespace TravelersAround.HTTPHost
{
    public class ErrorHandlerExtension : IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            return true;
        }

        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {

            #region temp
            //    if (error is FaultException)

            //{

            //    // extract the our FaultContract object from the exception object.

            //    var detail = error.GetType().GetProperty(“Detail”).GetGetMethod().Invoke(error, null);

            //    // create a fault message containing our FaultContract object

            //    fault = Message.CreateMessage(version, “”, detail, new DataContractJsonSerializer(detail.GetType()));

            //    // tell WCF to use JSON encoding rather than default XML

            //    var wbf = new WebBodyFormatMessageProperty(WebContentFormat.Json);

            //    fault.Properties.Add(WebBodyFormatMessageProperty.Name, wbf);



            //    // return custom error code.

            //    var rmp = new HttpResponseMessageProperty();

            //    rmp.StatusCode = System.Net.HttpStatusCode.BadRequest;

            //    // put appropraite description here..

            //    rmp.StatusDescription = “See fault object for more information.”;

            //    fault.Properties.Add(HttpResponseMessageProperty.Name, rmp);

            //}

            //else

            //{

            //    fault = Message.CreateMessage(version, “”, “An non-fault exception is occured.”, new DataContractJsonSerializer(typeof(string)));

            //    var wbf = new WebBodyFormatMessageProperty(WebContentFormat.Json);

            //    fault.Properties.Add(WebBodyFormatMessageProperty.Name, wbf);

            //    // return custom error code.

            //    var rmp = new HttpResponseMessageProperty();

            //    rmp.StatusCode = System.Net.HttpStatusCode.InternalServerError;

            //    // put appropraite description here..

            //    rmp.StatusDescription = “Uknown exception…”;

            //    fault.Properties.Add(HttpResponseMessageProperty.Name, rmp);

            //}
            //}
            #endregion
        }
    }
}
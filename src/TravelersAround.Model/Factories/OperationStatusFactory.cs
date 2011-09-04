using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Model.Entities;

namespace TravelersAround.Model.Factories
{
    public class OperationStatusFactory
    {
        public static OperationStatus CreateFromException(Exception ex, string message ="")
        {
            OperationStatus opStatus = new OperationStatus
            {
                Succeeded = false,
                Message = message,
            };

            if (ex != null)
            {
                opStatus.ExceptionMessage = ex.Message;
                opStatus.ExceptionStackTrace = ex.StackTrace;
                opStatus.ExceptionInnerMessage = (ex.InnerException == null) ? null : ex.InnerException.Message;
                opStatus.ExceptionInnerStackTrace = (ex.InnerException == null) ? null : ex.InnerException.StackTrace;
            }
            return opStatus;
        }

        public static OperationStatus CreateFromBoolean(bool succeeded, string message = "")
        {
            return new OperationStatus { Succeeded = true, Message = message };
        }
    }
}

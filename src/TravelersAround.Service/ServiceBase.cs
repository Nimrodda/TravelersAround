﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.DataContracts;
using log4net;

namespace TravelersAround.Service
{
    public abstract class ServiceBase
    {
        protected ILog _log;

        public ServiceBase(ILog log)
        {
            _log = log;
        }

        /// <summary>
        /// Sets the response Message if a key found by the exception type name in R.ErrorMessages
        /// otherwise, logs the exception
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="response"></param>
        protected void ReportError(Exception exception, ResponseBase response)
        {
            string errorMessage = R.String.ErrorMessages.ResourceManager.GetString(exception.GetType().Name);
            if (String.IsNullOrEmpty(errorMessage))
            {
                _log.Error(response, exception);
            }
            else response.ResponseMessage = errorMessage;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelersAround.ServiceProxy;

namespace TravelersAround.WebMvc.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected ITravelersAroundServiceFacade _taService;

        public ControllerBase(ITravelersAroundServiceFacade taService)
        {
            _taService = taService;
        }
    }
}

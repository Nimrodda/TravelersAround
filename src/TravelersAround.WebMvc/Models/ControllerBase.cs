using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelersAround.ServiceProxy;
using TravelersAround.ServiceProxy.ViewModels;

namespace TravelersAround.WebMvc.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected const int PAGE_SIZE = 10;
        protected ProfileDisplayView CurrentTravelerProfileCache
        {
            get
            {
                return (ProfileDisplayView)Session["CurrentTravelerProfile"];
            }
            set
            {
                Session["CurrentTravelerProfile"] = value;
            }
        }
        
        protected ITravelersAroundServiceFacade _taService;

        public ControllerBase(ITravelersAroundServiceFacade taService)
        {
            _taService = taService;
            
        }
    }
}

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
        protected const int PAGE_SIZE = 10;

        protected ITravelersAroundServiceFacade _taService;
        protected bool IsAsyncRequest { get; private set; }
        private bool _isAsync;

        public ControllerBase(ITravelersAroundServiceFacade taService)
        {
            _taService = taService;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string async = Request.QueryString["async"];
            bool.TryParse(async, out _isAsync);
            IsAsyncRequest = _isAsync;
            
            base.OnActionExecuting(filterContext);
        }
    }
}

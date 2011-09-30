using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelersAround.ServiceProxy;
using TravelersAround.ServiceProxy.ViewModels;

namespace TravelersAround.WebMvc.Controllers
{
    [Authorize]
    public class SearchController : ControllerBase
    {
        public SearchController(ITravelersAroundServiceFacade taService)
            : base(taService)
        {
        }

        public ActionResult Index(SearchView model, int p = 0)
        {
            model.IPAddress = Request.UserHostAddress;
            
#if DEBUG
            model.IPAddress = "80.221.20.181";
#endif
            model = _taService.Search(model.IncludeOfflineTravelers, p * PAGE_SIZE, PAGE_SIZE, model.IPAddress);
            
            if (!model.Success)
            {
                ModelState.AddModelError("", model.ResponseMessage);
            }

            return View(model);
        }

    }
}

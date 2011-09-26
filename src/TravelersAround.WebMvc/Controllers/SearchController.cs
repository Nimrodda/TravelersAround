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
            model = _taService.Search(model.IncludeOfflineTravelers, p * PAGE_SIZE, PAGE_SIZE);
            
            if (!model.Success)
            {
                ModelState.AddModelError("", model.ErrorMessage);
            }

            return View(model);
        }

    }
}

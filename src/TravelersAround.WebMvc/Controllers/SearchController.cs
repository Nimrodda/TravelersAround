using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelersAround.ServiceProxy;
using TravelersAround.ServiceProxy.ViewModels;

namespace TravelersAround.WebMvc.Controllers
{
    public class SearchController : ControllerBase
    {
        public SearchController(ITravelersAroundServiceFacade taService)
            : base(taService)
        {
        }

        //[HttpGet]
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //[HttpPost]
        public ActionResult Index(SearchView model, int p = 0)
        {
            model = _taService.Search(model.AvailabilityMark, p * PAGE_SIZE, PAGE_SIZE);
            
            if (!model.Success)
            {
                ModelState.AddModelError("", model.ErrorMessage);
            }

            return View(model);
        }

    }
}

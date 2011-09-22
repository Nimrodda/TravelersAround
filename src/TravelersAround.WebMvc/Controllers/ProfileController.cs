using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelersAround.ServiceProxy;
using TravelersAround.ServiceProxy.ViewModels;

namespace TravelersAround.WebMvc.Controllers
{
    public class ProfileController : ControllerBase
    {
        public ProfileController(ITravelersAroundServiceFacade taService) : base(taService)
        {
        }

        //ProfileDisplay
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(ProfileUpdateView model)
        {
            return View(model);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelersAround.ServiceProxy.ViewModels;
using TravelersAround.ServiceProxy;

namespace TravelersAround.WebMvc.Controllers
{
    public class AjaxController : ControllerBase
    {

        public AjaxController(ITravelersAroundServiceFacade taService)
            : base(taService)
        {
        }

        [Authorize]
        public JsonResult Tick()
        {
            TickerModel model = new TickerModel();
            model.IPAddress = Request.UserHostAddress;
            model = _taService.Tick(model);
            model.IPAddress = Request.UserHostAddress;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

    }
}

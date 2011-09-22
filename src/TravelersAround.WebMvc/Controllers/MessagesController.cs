using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelersAround.ServiceProxy;
using TravelersAround.ServiceProxy.ViewModels;

namespace TravelersAround.WebMvc.Controllers
{
    public class MessagesController : Controller
    {
        private ITravelersAroundServiceFacade _taService;

        public MessagesController(ITravelersAroundServiceFacade taService)
        {
            _taService = taService;
        }

        //MessagesList
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Send()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Send(MessageSendView model)
        {
            return View();
        }

        public ActionResult Read(string id)
        {
            return View();
        }

        public ActionResult Delete(string id)
        {
            return View("Index");
        }
    }
}

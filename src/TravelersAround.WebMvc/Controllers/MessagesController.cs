using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelersAround.ServiceProxy;
using TravelersAround.ServiceProxy.ViewModels;

namespace TravelersAround.WebMvc.Controllers
{
    public class MessagesController : ControllerBase
    {
        public MessagesController(ITravelersAroundServiceFacade taService) : base(taService)
        {
        }

        //MessagesList
        public ActionResult Index(string folder, int p = 0)
        {
            MessagesListView model = _taService.ListMessages(folder, p * PAGE_SIZE, PAGE_SIZE);
            return View(model);
        }

        [HttpGet]
        public ActionResult Send()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Send(MessageSendView model)
        {
            if (ModelState.IsValid)
            {
                model = _taService.SendMessage(model);
                if (model.Success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", model.ErrorMessage);
                }
            }
            return View(model);
        }

        public ActionResult Read(string id)
        {
            MessageReadView model = _taService.ReadMessage(id);
            if (!model.Success)
            {
                ModelState.AddModelError("", model.ErrorMessage);
            }
            return View(model);
        }

        public ActionResult Delete(string id)
        {
            MessagesListView model = _taService.DeleteMessage(id);
            if (model.Success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", model.ErrorMessage);
            }
            return View("Index", model);
        }
    }
}

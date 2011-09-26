using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelersAround.ServiceProxy;
using TravelersAround.ServiceProxy.ViewModels;
using System.IO;

namespace TravelersAround.WebMvc.Controllers
{
    [Authorize]
    public class ProfileController : ControllerBase
    {
        public ProfileController(ITravelersAroundServiceFacade taService) : base(taService)
        {
        }

        //ProfileDisplay
        public ActionResult Index()
        {
            ProfileDisplayView model = _taService.DisplayProfile();
            if (!model.Success)
            {
                ModelState.AddModelError("", model.ErrorMessage);
            }

            return View(model);
        }
        
        [HttpGet]
        public ActionResult Edit()
        {
            ProfileUpdateView model = _taService.DisplayProfile().ConvertToProfileUpdateView();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProfileUpdateView model)
        {
            if (ModelState.IsValid)
            {
                model = _taService.UpdateProfile(model);
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

        public ActionResult LoadPicture(string id)
        {
            Stream picture = _taService.GetProfilePicture(id);

            return new FileStreamResult(picture, "image/jpeg");
        }

        [HttpPost]
        public ActionResult UploadPicture(HttpPostedFile uploadedFile)
        {
            ProfileUpdateView model = _taService.UploadProfilePicture(uploadedFile.InputStream);
            if (model.Success)
            {
                return RedirectToAction("Edit");
            }
            else
            {
                ModelState.AddModelError("", model.ErrorMessage);
            }
            return View("Edit", model);
        }
    }
}

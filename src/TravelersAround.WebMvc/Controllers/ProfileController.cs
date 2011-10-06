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
            ProfileDisplayView model = CurrentTravelerProfileCache ?? _taService.DisplayProfile();
            if (model.Success)
            {
                CurrentTravelerProfileCache = model;
            }
            else
            {
                ModelState.AddModelError("", model.ResponseMessage);
            }
            ViewBag.NotificationMessage = TempData["NotificationMessage"];
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult SessionInfo()
        {
            ProfileDisplayView model = CurrentTravelerProfileCache ?? _taService.DisplayProfile();
            return PartialView("_SessionInfo", model);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            ProfileUpdateView model = (CurrentTravelerProfileCache ?? _taService.DisplayProfile()).MapToProfileUpdateView();
            if (model.Success)
            {
                CurrentTravelerProfileCache = model.MapToProfileDisplayView();
            }
            else
            {
                ModelState.AddModelError("", model.ResponseMessage);
            }
            ViewBag.NotificationMessage = TempData["NotificationMessage"];
            if (Request.IsAjaxRequest()) 
                return Json(model, JsonRequestBehavior.AllowGet);
            else 
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
                    CurrentTravelerProfileCache = null;
                    if (Request.IsAjaxRequest()) return Json(model);
                    TempData["NotificationMessage"] = R.String.SuccessMessages.SuccessProfileUpdate;
                    return RedirectToAction("Edit");
                }
                else
                {
                    ModelState.AddModelError("", model.ResponseMessage);
                }
            }
            //TODO: handle situation when model is not valid and return proper error for ajax calls
            return View(model);
        }

        public ActionResult LoadPicture(string id)
        {
            MemoryStream picture = _taService.GetProfilePicture(id) as MemoryStream;
            byte[] buffer = picture.ToArray();
            if (buffer.Length == 0)
            {
                FileStream tempImage = new FileStream(Server.MapPath("~/Content/img/anonymous.jpg"), FileMode.Open, FileAccess.Read);
                buffer = new byte[tempImage.Length];
                tempImage.Read(buffer, 0, (int)tempImage.Length);
            }
            return File(buffer, "image/jpeg");
        }

        [HttpPost]
        public ActionResult UploadPicture(HttpPostedFileBase uploadedFile)
        {
            string errorMessage = "File format not supposed. Only JPEG and PNG are accepted. Max size is 1.5MB";

            if (uploadedFile != null && uploadedFile.ContentLength > 0 &&
                (uploadedFile.ContentType.ToLower().Contains("jpeg") || uploadedFile.ContentType.ToLower().Contains("png")))
            {
                ProfileUpdateView model = _taService.UploadProfilePicture(uploadedFile.InputStream);
                if (model.Success)
                {
                    return RedirectToAction("Edit");
                }
                else
                {
                    errorMessage = model.ResponseMessage;
                }
            }
            ModelState.AddModelError("uploadedFile", errorMessage);
            return View("Edit");
        }
    }
}


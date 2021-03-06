﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using TravelersAround.WebMvc.Models;
using TravelersAround.ServiceProxy;
using TravelersAround.ServiceProxy.ViewModels;

namespace TravelersAround.WebMvc.Controllers
{
    public class AccountController : ControllerBase
    {

        private IFormsAuthenticationService _formsService;
        private IMembershipServiceFacade _membershipService;

        public AccountController(IMembershipServiceFacade membershipService,
                                IFormsAuthenticationService formsService,
                                ITravelersAroundServiceFacade taService)
            :base(taService)
        {
            _membershipService = membershipService;
            _formsService = formsService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginView model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                model = _membershipService.Login(model);
                if (model.Success)
                {
                    CurrentTravelerProfileCache = model.MapToProfileDisplayView();
                    
                    _formsService.SignIn(model.ApiKey, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Search");
                    }
                }
                else
                {
                    ModelState.AddModelError("", model.ResponseMessage);
                }
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            _membershipService.Logout(User.Identity.Name);
            _formsService.SignOut();
            Session.Abandon();
            
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterView model)
        {
            if (ModelState.IsValid)
            {
                model = _membershipService.Register(model);
                if (model.Success)
                {
                    _formsService.SignIn(model.ApiKey, true);
                    return RedirectToAction("Index", "Search");
                }
                else
                {
                    ModelState.AddModelError("", model.ResponseMessage);
                }
            }

            return View(model);
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }
        /*
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordView model)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePasswordSuccess
        // **************************************

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }
        */
    }
}

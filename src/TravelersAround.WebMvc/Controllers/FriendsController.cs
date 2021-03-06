﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelersAround.ServiceProxy.ViewModels;
using TravelersAround.ServiceProxy;

namespace TravelersAround.WebMvc.Controllers
{
    [Authorize]
    public class FriendsController : ControllerBase
    {
        public FriendsController(ITravelersAroundServiceFacade taService) : base(taService)
        {
        }

        //FriendsList
        public ActionResult Index(int p = 0)
        {
            FriendsListView model = _taService.ListFriends(p * PAGE_SIZE, PAGE_SIZE);
            model.Page = p;
            ViewBag.NotificationMessage = TempData["NotificationMessage"];
            return View(model);
        }

        public ActionResult Add(string id)
        {
            FriendsListView model = _taService.AddFriend(id);
            if (model.Success)
            {
                TempData["NotificationMessage"] = "Traveler has been added to your friends list";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", model.ResponseMessage);
            }
            model = _taService.ListFriends(0, PAGE_SIZE);
            return View("Index", model);
        }

        public ActionResult Remove(string id)
        {
            FriendsListView model = _taService.RemoveFriend(id);
            if (model.Success)
            {
                TempData["NotificationMessage"] = "Traveler has been removed from your friends list";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", model.ResponseMessage);
            }
            return View("Index", model);
        }

    }
}

using System;
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
            return View(model);
        }

        public ActionResult Add(string id)
        {
            FriendsListView model = _taService.AddFriend(id);
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

        public ActionResult Remove(string id)
        {
            FriendsListView model = _taService.RemoveFriend(id);
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

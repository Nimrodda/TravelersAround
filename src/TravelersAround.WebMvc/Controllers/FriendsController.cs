using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelersAround.ServiceProxy.ViewModels;
using TravelersAround.ServiceProxy;

namespace TravelersAround.WebMvc.Controllers
{
    public class FriendsController : Controller
    {
        private ITravelersAroundServiceFacade _taService;

        public FriendsController(ITravelersAroundServiceFacade taService)
        {
            _taService = taService;
        }

        //FriendsList
        public ActionResult Index(int i = 0)
        {
            FriendsListView model = _taService.ListFriends(i, 10);
            return View(model);
        }

        public ActionResult Add(string id)
        {
            return View("Index");
        }

        public ActionResult Remove(string id)
        {
            return View("Index");
        }

    }
}

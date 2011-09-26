using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelersAround.ServiceProxy;
using TravelersAround.ServiceProxy.ViewModels;
using TravelersAround.WebMvc.Models;
using System.Collections.Specialized;

namespace TravelersAround.WebMvc.Controllers
{
    [Authorize]
    public class MessagesController : ControllerBase
    {
        public MessagesController(ITravelersAroundServiceFacade taService) : base(taService)
        {
        }

        //MessagesList
        public ActionResult Index(string folder = "Inbox", int p = 0)
        {
            MessagesListView model = _taService.ListMessages(folder, p * PAGE_SIZE, PAGE_SIZE);
            if (folder.Equals("inbox", StringComparison.CurrentCultureIgnoreCase))
            {
                int unreadMessagesCount = model.MessagesList.Count(message => !message.IsRead);
                if (unreadMessagesCount > 0) Session["NewMessagesCount"] = unreadMessagesCount;
            }
            model.Folder = folder;
            model.Page = p;
            ViewBag.NotificationMessage = TempData["NotificationMessage"];
            return View(model);
        }

        [HttpGet]
        public ActionResult Send(string id, int p = 0)
        {
            MessageSendView model = new MessageSendView { RecipientID = id };
            model.FriendsDropDownList = _taService.ListFriends(p * PAGE_SIZE, PAGE_SIZE).ConvertToSelectListItemList();
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Send(MessageSendView model)
        {
            if (ModelState.IsValid)
            {
                model = _taService.SendMessage(model);
                if (model.Success)
                {
                    TempData["NotificationMessage"] = "Message successfully sent!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", model.ErrorMessage);
                }
            }
            return View(model);
        }

        public ActionResult Read(string id, string returnToFolder, bool isUnread)
        {
            MessageReadView model = _taService.ReadMessage(id);
            model.ReturnToFolder = returnToFolder;
            if (!model.Success)
            {
                ModelState.AddModelError("", model.ErrorMessage);
            }
            //Setting new messages counter
            if (Session["NewMessagesCount"] != null && isUnread && returnToFolder.Equals("inbox", StringComparison.CurrentCultureIgnoreCase))
            {
                int count = (int)Session["NewMessagesCount"];
                count--;
                Session["NewMessagesCount"] = count;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(MessageDeleteView model)
        {
            MessagesListView messageListView;

            if (ModelState.IsValid)
            {
                messageListView = _taService.DeleteMessage(String.Join(",", model.MessageIDs));
                if (messageListView.Success)
                {
                    TempData["SuccessMessage"] = "Message(s) successfully deleted!";
                    return RedirectToAction("Index", new { folder = model.Folder, p = model.Page });
                }
                else
                {
                    ModelState.AddModelError("", messageListView.ErrorMessage);
                }
            }
            messageListView = _taService.ListMessages(model.Folder, model.Page * PAGE_SIZE, PAGE_SIZE);
            messageListView.Folder = model.Folder;
            messageListView.Page = model.Page;
            return View("Index", messageListView);
        }
    }
}

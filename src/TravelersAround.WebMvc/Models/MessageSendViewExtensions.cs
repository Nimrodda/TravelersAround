using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelersAround.ServiceProxy.ViewModels;
using TravelersAround.ServiceProxy;

namespace TravelersAround.WebMvc.Models
{
    public static class MessageSendViewExtensions
    {
        public static List<DropDownListItem> ConvertToSelectListItemList(this FriendsListView model)
        {
            List<DropDownListItem> selectList = new List<DropDownListItem>();

            foreach (var friend in model.FriendsList)
            {
                selectList.Add(new DropDownListItem 
                { 
                    Value = friend.TravelerID,
                    Name = String.Concat(friend.Firstname, " ", friend.Lastname)
                });
            }

            return selectList;
        }
    }
}
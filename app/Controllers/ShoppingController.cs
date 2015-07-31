using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Landmark.Classes;
using Landmark.Helper;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Landmark.Controllers
{
    public class ShoppingController : Controller
    {
        public ActionResult GetItems(string currentId)
        {
            Item currentItem = Sitecore.Context.Database.GetItem(currentId);
            var items = currentItem.Children.ToList();
            TempData["items"] = items;
            return PartialView("_GetItems");
        }

        public ActionResult GetJson(string childcategory)
        {
            Item shoppingCategory = Sitecore.Context.Database.GetItem(childcategory);
            return Redirect(Sitecore.Links.LinkManager.GetItemUrl(shoppingCategory.Children.First()));
        }

    }

}
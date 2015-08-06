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
        public ActionResult DiscoverMore(string targetId)
        {
            Item target = Sitecore.Context.Database.GetItem(targetId);
            return Redirect(Sitecore.Links.LinkManager.GetItemUrl(target));
        }

        public ActionResult GoTo(string childcategory)
        {
            Item shoppingCategory = Sitecore.Context.Database.GetItem(childcategory);
            return Redirect(Sitecore.Links.LinkManager.GetItemUrl(shoppingCategory.Children.First()));
        }

    }

}
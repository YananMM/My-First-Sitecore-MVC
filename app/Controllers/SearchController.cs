﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Landmark.Classes;
using Landmark.Helper;
using Landmark.Models;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Shell.Framework.Commands.UserManager;
using Sitecore.Configuration;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.Linq.Utilities;
using Sitecore.Mvc.Extensions;
using Sitecore.Shell.Web.UI.WebControls;

namespace Landmark.Controllers
{
    public class SearchController : Controller
    {
        public SearchController()
        {

        }

        public ActionResult SearchContent(string search)
        {
            return Redirect(LandmarkHelper.TranslateUrl(Sitecore.Links.LinkManager.GetItemUrl(Sitecore.Context.Database.GetItem(ItemGuids.SearchResultsPage))) + "?searchString=" + search);
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            if (!string.IsNullOrEmpty(search))
            {
                ViewData["SearchString"] = search;
                var language = Sitecore.Context.Language.Name.ToLower();
                string indexName = Settings.GetSetting("LandmarkIndexName");
                var index = ContentSearchManager.GetIndex(indexName);
                var query = PredicateBuilder.True<LandmarkSearchResultItem>();


                query = query.And(x => x.PageTitle.Contains(search));


                using (var context = index.CreateSearchContext())
                {

                    var searchItems = context.GetQueryable<SearchResultItem>()
                        .Where(item => item.Content.Like(search) && item.Language.Equals(language))
                        .OrderBy(item => item.CreatedDate)
                        .ToList();
                    var total = searchItems.Count;
                    if (total > 0)
                    {
                        return Content(total.ToString());
                    }
                    else
                    {
                        return Content("No Search Result");
                    }
                }
            }
            
        }
    }
}
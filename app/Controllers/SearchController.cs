using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Shell.Framework.Commands.UserManager;
using Sitecore.Configuration;

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
                using (var context = index.CreateSearchContext())
                {
                    var searchItems = context.GetQueryable<SearchResultItem>()
                        .Where(item => item.Content.Contains(search) && item.Language.Equals(language)
                        ).ToList();
                    //searchItems = searchItems.Where(item => item.Content.Like(SearchTerm));
                    return Content(searchItems.Count.ToString());
                }
            }
            return Content("");
        }
    }
}
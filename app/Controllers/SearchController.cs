using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;

namespace Landmark.Controllers
{
    public class SearchController : Controller
    {
        public SearchController()
        {

        }

        public ActionResult Search(string searchString)
        {

            if (!string.IsNullOrEmpty(searchString))
            {
                ViewData["SearchString"] = searchString;

                var language = Sitecore.Context.Language.Name.ToLower();

                var index = ContentSearchManager.GetIndex("sitecore_master_index");
                using (var context = index.CreateSearchContext())
                {
                    IQueryable<SearchResultItem> searchItems = context.GetQueryable<SearchResultItem>();
                    //searchItems = searchItems.Where(item => item.Content.Like(SearchTerm));
                    searchItems = searchItems.Where(item => item.Content.Contains(searchString));
                }
            }
            return Content("");
        }
    }
}
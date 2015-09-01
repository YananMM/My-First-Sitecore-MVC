using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Landmark.Models;
using Sitecore.ContentSearch;
using Sitecore.Configuration;

namespace Landmark.Helper
{
    public class SearchHelper
    {
        public List<LandmarkSearchResultItem> GetSearchResults(string search)
        {
            List<LandmarkSearchResultItem> results = new List<LandmarkSearchResultItem>();
            if (!string.IsNullOrEmpty(search))
            {
                var language = Sitecore.Context.Language.Name.ToLower();
                string indexName = Settings.GetSetting("LandmarkIndexName");
                var index = ContentSearchManager.GetIndex(indexName);
                //var query = PredicateBuilder.True<LandmarkSearchResultItem>();

                //query = query.And(x => x.PageTitle.Contains(search));

                using (var context = index.CreateSearchContext())
                {
                    results = context.GetQueryable<LandmarkSearchResultItem>()
                        .Where(item => item.Language.Equals(language) && item.Content.Contains(search))
                        .OrderBy(item => item.FilterType)
                        .ToList();
                    
                }
                var total = results.Count;
                
            }
            return results;
        }
    }
}
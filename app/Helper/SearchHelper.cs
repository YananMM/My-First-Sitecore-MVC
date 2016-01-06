using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Landmark.Classes;
using Landmark.Models;
using Sitecore.ContentSearch;
using Sitecore.Configuration;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc.Extensions;

namespace Landmark.Helper
{
    public class SearchHelper
    {
        public List<LandmarkSearchResultItem> GetSearchResults(string searchString=null,string type=null,string page=null)
        {
            List<LandmarkSearchResultItem> results = new List<LandmarkSearchResultItem>();

            if (!string.IsNullOrEmpty(searchString))
            {
                var language = Sitecore.Context.Language.Name.ToLower();
                string indexName = Settings.GetSetting("LandmarkIndexName");
                var index = ContentSearchManager.GetIndex(indexName);
                //var query = PredicateBuilder.True<LandmarkSearchResultItem>();

                //query = query.And(x => x.PageTitle.Contains(search));

                using (var context = index.CreateSearchContext())
                {
                    
                    results = context.GetQueryable<LandmarkSearchResultItem>()
                        .Where(item => item.Language.Equals(language) && 
                            (item.PageTitle.Contains(searchString) || item.PageContent.Contains(searchString) || item.ContentTitle.Contains(searchString)
                            || item.ContentDescription.Contains(searchString) || item.Tags.Contains(searchString)
                            || item.ArticleTitle.Contains(searchString) || item.ArticleIntro.Contains(searchString) ||item.ArticleSubtitle.Contains(searchString)))
                            .OrderBy(item=>item.FilterOrder)
                        .ToList();
                    results = (from result in results
                        let resultItem = result.GetItem()
                        where LandmarkHelper.IsShownInNavigation(resultItem)
                        select result).OrderBy(item=>item.FilterOrder).ToList();
                    if(type!=null)
                        results = results.Where(item => item.FilterType == type).ToList();
                    if (page != null)
                    {
                        int pagenumber = page == null ? 1 : Int32.Parse(page);
                        results = results.Skip((pagenumber - 1) * 10).Take(10).ToList();
                    }
                     
                }
                
            }
            IEnumerable<IGrouping<string, LandmarkSearchResultItem>> groups = results.GroupBy(x => x.FilterOrder);
            IEnumerable<LandmarkSearchResultItem> smths = groups.SelectMany(group => group);
            List<LandmarkSearchResultItem> groupResults = smths.ToList();
            return groupResults;
        }

        public List<FilterTypeResults> GetFilterTypes(string searchString = null)
        {
            List<Item> pageItems = Factory.GetDatabase("web").GetItem(ItemGuids.LandmarkHomeItem).Children.Where(item => item.Fields["Is Shown In Navigation"] != null).ToList();
            List<Item> types = pageItems.Where(item => ((CheckboxField)item.Fields["Is Shown In Navigation"]).Checked).ToList();
            List<FilterTypeResults> filterresults = new List<FilterTypeResults>();
            foreach (Item filtertype in types)
            {
                Item filterTypeEn = Factory.GetDatabase("web").GetItem(filtertype.ID, Sitecore.Data.Managers.LanguageManager.GetLanguage("en", Factory.GetDatabase("web")));
                var results = GetSearchResults(searchString, filterTypeEn.Fields["Page Title"].Value);
                FilterTypeResults one= new FilterTypeResults();
                one.id = filtertype.ID.ToString();
                one.count = results.Count.ToString();
                one.value = filtertype.Fields["Page Title"].Value;
                one.envalue = filterTypeEn.Fields["Page Title"].Value;
                filterresults.Add(one);
            }
            return filterresults;
        }
    }

    public class FilterTypeResults
    {
        public string id;
        public string value;
        public string count;
        public string envalue;
    }
}
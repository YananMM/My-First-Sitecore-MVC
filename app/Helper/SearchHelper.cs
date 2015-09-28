﻿using System;
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
                        .Where(item => item.Language.Equals(language) && item.Content.Contains(searchString))
                        .ToList();
                    if(type!=null)
                        results = results.Where(item => item.FilterType == type).ToList();
                    if (page != null)
                    {
                        int pagenumber = page == null ? 1 : Int32.Parse(page);
                        results = results.Skip(pagenumber * LandmarkHelper.NumberInOnePage).Take(LandmarkHelper.NumberInOnePage).ToList();
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
            List<Item> pageItems = SitecoreItems.LandmarkHomeItem.Children.Where(item => item.Fields["Is Shown In Navigation"] != null).ToList();
            List<Item> types = pageItems.Where(item => ((CheckboxField)item.Fields["Is Shown In Navigation"]).Checked).ToList();
            List<FilterTypeResults> filterresults = new List<FilterTypeResults>();
            foreach (Item filtertype in types)
            {
                var results = GetSearchResults(searchString, filtertype.Fields["Page Title"].Value);
                FilterTypeResults one= new FilterTypeResults();
                one.id = filtertype.ID.ToString();
                one.count = results.Count.ToString();
                one.value = filtertype.Fields["Page Title"].Value;
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
    }
}
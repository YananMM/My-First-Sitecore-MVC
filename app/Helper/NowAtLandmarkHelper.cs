using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Landmark.Classes;
using Sitecore.Data.Items;

namespace Landmark.Helper
{
    public class NowAtLandmarkHelper
    {
        public Item GetLatestArticle()
        {
            return
                LandmarkHelper.GetItemByTemplate(Sitecore.Context.Item, ItemGuids.T4PageTemplate)
                    .OrderBy(article => article.Fields["Article Date"].ToString())
                    .First();
        }

        public string GetCallOutSliderImage(Item item)
        {
            string src = string.Empty;
            return src;
        }

        public List<Item> GetTheRestArticles()
        {
            var articles = LandmarkHelper.GetItemByTemplate(Sitecore.Context.Item, ItemGuids.T4PageTemplate)
                    .OrderBy(article => article.Fields["Article Date"].ToString());
            if (articles.Count()>1)
                return articles.Reverse().Take(articles.Count() - 1).ToList();
            else
                return new List<Item>();
        }
    }
}
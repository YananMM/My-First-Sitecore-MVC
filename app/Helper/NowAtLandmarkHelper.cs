using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Landmark.Classes;
using Sitecore.Data.Fields;
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
                    .Last();
        }

        public List<Item> GetTheRestArticles(string page=null)
        {
            int pagenumber ;
            pagenumber = page != null ? Int32.Parse(page) : 1;
            List<Item> articles = LandmarkHelper.GetItemByTemplate(Sitecore.Context.Item, ItemGuids.T4PageTemplate)
                .Where(article => article.ID.ToString() != GetLatestArticle().ID.ToString())
                    .OrderBy(article => article.Fields["Article Date"].ToString()).ToList();
            foreach (var refer in LandmarkHelper.GetItemByTemplate(Sitecore.Context.Item, ItemGuids.ReferenceObjectTemplate))
            {
                ReferenceField rField = refer.Fields["Reference Item"];
                if (rField != null && rField.TargetItem != null)
                {
                    articles.Add(rField.TargetItem);
                }
            }
            if (articles.Count()>1)
                return articles.Skip((pagenumber-1) * 10).Take(10).ToList();
            else
                return new List<Item>();
        }
    }
}
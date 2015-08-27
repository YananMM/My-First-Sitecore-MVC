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
                    .Last();
        }
    }
}
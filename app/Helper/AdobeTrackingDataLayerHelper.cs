using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Landmark.Classes;

namespace Landmark.Helper
{
    public static class AdobeTrackingDataLayerHelper
    {
        public static string GetShopName()
        {
            string shopName = string.Empty;
            if (Sitecore.Context.Item.TemplateID.ToString().Equals(ItemGuids.T14ShopDetailsTemplate))
            {
                shopName = Sitecore.Context.Item.Fields["Brand Title"].Value;
            }
            return shopName;
        }

        public static string GetBuildingName()
        {
            string buildingName = string.Empty;

            var buildingField = Sitecore.Context.Item.Fields["Building"];
            if (buildingField != null)
            {
                return buildingName;
            }
            return buildingName;
        }

        public static string GetRestaurantName()
        {
            string restaurantName = string.Empty;

            return restaurantName;
        }

        public static string GetArticleTitle()
        {
            string articleTitle = string.Empty;
            var articleFiled = Sitecore.Context.Item.Fields["Article Headline"];
            if (articleFiled != null)
            {
                articleTitle = articleFiled.Value;
            }
            return articleTitle;
        }

        public static string GetPageTitle()
        {
            return Sitecore.Context.Item.Fields["Page Title"].Value;
        }

    }
}
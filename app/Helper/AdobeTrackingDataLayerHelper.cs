using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages;
using Landmark.Classes;
using Sitecore.Data.Fields;
using Sitecore.Mvc.Extensions;

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

            var buildingField = (ReferenceField)Sitecore.Context.Item.Fields["Building"];
            if (buildingField != null && buildingField.TargetItem!=null)
            {
                buildingName = buildingField.TargetItem.Fields["Building Title"].Value;
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

        public static string GetPageName()
        {
            string pageName = string.Empty;
            if (Sitecore.Context.Item.ID.ToString() == ItemGuids.PageNotFoundItem)
                pageName = "404";
            if (Sitecore.Context.Item.ID.ToString() == ItemGuids.PageNotFoundItem)
                pageName = "email_success";
            if (Sitecore.Context.Item.ID.ToString() == ItemGuids.ContactSuccessPage)
                pageName = "contact_success";
            return pageName;
        }

    }
}
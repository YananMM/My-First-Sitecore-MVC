using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Mvc.Helpers;
using Sitecore.Data.Items;

namespace Landmark.Helper
{
    public static class SitecoreFieldHelper
    {
        private static Database _webDb = Factory.GetDatabase("web");

        /// <summary>
        /// This is a custom field helper for image fields, allowing you to pass properties like 'max width' and 'max height' in as
        /// named parameters, rather than using new { }
        /// </summary>
        public static HtmlString ImageField(this SitecoreHelper helper, string fieldName, Item item, int mh, int mw, bool disableWebEditing = false)
        {
            return helper.Field(fieldName, item, new { mh, mw, DisableWebEdit = disableWebEditing });
        }

        ///<summary>
        /// This is customer field helper for image field to return image src
        /// </summary>
        public static String ImageFieldSrc(this SitecoreHelper helper, string fieldName, Item item)
        {
            string imageURL = string.Empty;
            Sitecore.Data.Fields.ImageField imageField = item.Fields[fieldName];
            if (imageField != null && imageField.MediaItem != null)
            {
                Sitecore.Data.Items.MediaItem image = new Sitecore.Data.Items.MediaItem(imageField.MediaItem);
                imageURL = Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(image));
            }
            return imageURL;
        }
    }

}
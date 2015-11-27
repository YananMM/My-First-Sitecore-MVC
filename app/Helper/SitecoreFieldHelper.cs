using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Mvc.Helpers;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Globalization;
using Sitecore.Mvc.Presentation;

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

        public static String ImageFieldSrc(string fieldName, Item item)
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

        public static String FileFieldUrl(this SitecoreHelper helper, string fieldName, Item item)
        {
            string fileUrl = string.Empty;

            Sitecore.Data.Fields.FileField fileField = item.Fields[fieldName];
            if (fileField != null && fileField.MediaItem != null)
            {
                Sitecore.Data.Items.MediaItem image = new Sitecore.Data.Items.MediaItem(fileField.MediaItem);
                fileUrl = Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(image));
            }
            return fileUrl;
        }

        public static HtmlString LinkField(this SitecoreHelper helper, string fieldName, Item item, object parameters)
        {
            return new HtmlString(helper.Field(fieldName, item, parameters).ToString().Replace("/zh-hk", "/tc").Replace("/zh-cn", "/sc"));
        }

        public static HtmlString DynamicPlaceholder(this Sitecore.Mvc.Helpers.SitecoreHelper helper, string dynamicKey)
        {
            var currentRenderingId = RenderingContext.Current.Rendering.UniqueId;
            return helper.Placeholder(string.Format("{0}_{1}", dynamicKey, currentRenderingId));
        }
    }

}
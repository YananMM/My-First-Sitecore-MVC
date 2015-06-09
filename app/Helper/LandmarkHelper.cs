using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Collections;
using Sitecore.ContentSearch.Utilities;
using Sitecore.Data.Fields;
using Sitecore.Data.Managers;
using Sitecore.Globalization;
using Sitecore.Links;
using Sitecore.Web.UI;

namespace Landmark.Helper
{
    public static class LandmarkHelper
    {
        public static bool HasValidVersion()
        {
            bool hasValidVersion = false;
            LanguageCollection collection = ItemManager.GetContentLanguages(Sitecore.Context.Item);
            foreach (var lang in collection)
            {
                var itm = Sitecore.Context.Database.GetItem(Sitecore.Context.Item.ID, lang);
                if (itm.Versions.Count > 0)
                {
                    hasValidVersion = true;
                }
            }
            return hasValidVersion;
        }
        public static bool HasValidVersion(string language)
        {
            Item item = Sitecore.Context.Database.Items[Sitecore.Context.Item.ID, Sitecore.Globalization.Language.Parse(language)];
            Item validVersion = null;
            if (item != null)
            {
                validVersion = item.Publishing.GetValidVersion(DateTime.Now, true);
            }

            return validVersion != null && validVersion.Versions.Count > 0;
        }
        public static string GetUrlByLanguage(Item item,string language)
        {
            var options = Sitecore.Links.LinkManager.GetDefaultUrlOptions();
            options.Language = LanguageManager.GetLanguage(language);
            var returnUrl = LinkManager.GetItemUrl(item, options);

            return returnUrl;
        }

        private static string TranslateUrl(string url)
        {
            string result = url;
            //Translate language codes
            result = result.Replace("/zh-HK/", "/tc/");
            result = result.Replace("/zh-CN/", "/sc/");
            //Fix root paths
            result = result.Replace("/en.aspx", "/en/default.aspx");
            result = result.Replace("/tc.aspx", "/tc/default.aspx");
            result = result.Replace("/zh-HK.aspx", "/tc/default.aspx");
            result = result.Replace("/sc.aspx", "/sc/default.aspx");
            result = result.Replace("/zh-CN.aspx", "/sc/default.aspx");
            //Fix double prefixes
            result = result.Replace("/en/en/", "/en/");
            result = result.Replace("/sc/sc/", "/sc/");
            result = result.Replace("/tc/tc/", "/tc/");
            return result;
        }

        public static bool IsShownInNavigation(Item item)
        {
            bool isShown = false;
            CheckboxField field = (CheckboxField)item.Fields["Is Shown In Navigation"];
            if (field != null)
            {
                isShown = field.Checked;
            }
            return isShown;
        }

        public static List<Item> GetChildrenPageInNavigation(Item parentItem)
        {
            List<Item> resultsList= new List<Item>();
            foreach (Item child in parentItem.Children)
            {
                if (LandmarkHelper.IsShownInNavigation(child))
                {
                    resultsList.Add(child);
                }
            }
            return resultsList;
        }

        public static string GetPageBodyClass()
        {
            string bodyClass = string.Empty;
            bodyClass = Sitecore.Context.Item.TemplateName.Split(' ')[0].ToLower();
            return bodyClass;
        }
    }
}
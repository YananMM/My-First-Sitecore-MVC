using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Collections;
using Sitecore.ContentSearch.Utilities;
using Sitecore.Data.Fields;
using Sitecore.Data.Managers;
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
            string itemUrl = string.Empty;
            var options = Sitecore.Links.LinkManager.GetDefaultUrlOptions();
            options.LanguageEmbedding = Sitecore.Links.LanguageEmbedding.Always;
            options.EmbedLanguage(Sitecore.Globalization.Language.Parse(language));
            if (HasValidVersion(language))
            {
                options.EmbedLanguage(Sitecore.Globalization.Language.Parse(language));
            }
            else
            {
                options.EmbedLanguage(Sitecore.Globalization.Language.Parse("en"));
            }
            itemUrl = Sitecore.Links.LinkManager.GetItemUrl(item, options);
            return itemUrl;
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
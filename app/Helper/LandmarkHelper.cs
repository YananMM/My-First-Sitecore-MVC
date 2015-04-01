using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Collections;
using Sitecore.Data.Managers;

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
            options.EmbedLanguage(Sitecore.Globalization.Language.Parse("en"));
            if (HasValidVersion(language))
            {
                options.EmbedLanguage(Sitecore.Globalization.Language.Parse(language));
            }
            Sitecore.Links.LinkManager.GetItemUrl(item, options);
            return itemUrl;
        }
    }
}
using System;
using Sitecore.Data.Items;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Landmark.Classes;
using Landmark.Models;
using Sitecore.Collections;
using Sitecore.Configuration;
using Sitecore.ContentSearch.Utilities;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Managers;
using Sitecore.Globalization;
using Sitecore.Links;
using Sitecore.Shell.Applications.ContentEditor;
using Sitecore.Shell.Applications.ContentManager.ReturnFieldEditorValues;
using Sitecore.Web.UI;
using DateTime = System.DateTime;

namespace Landmark.Helper
{
    public static class LandmarkHelper
    {
        private static Database _webDb = Factory.GetDatabase("web");

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
            Item item =
                Sitecore.Context.Database.Items[
                    Sitecore.Context.Item.ID, Sitecore.Globalization.Language.Parse(language)];
            Item validVersion = null;
            if (item != null)
            {
                validVersion = item.Publishing.GetValidVersion(DateTime.Now, true);
            }

            return validVersion != null && validVersion.Versions.Count > 0;
        }

        public static string GetUrlByLanguage(Item item, string language)
        {
            var options = Sitecore.Links.LinkManager.GetDefaultUrlOptions();
            options.Language = LanguageManager.GetLanguage(language);
            var returnUrl = LinkManager.GetItemUrl(item, options);

            return TranslateUrl(returnUrl);
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
            CheckboxField field = (CheckboxField) item.Fields["Is Shown In Navigation"];
            if (field != null)
            {
                isShown = field.Checked;
            }
            return isShown;
        }

        public static List<Item> GetChildrenPageInNavigation(Item parentItem)
        {
            List<Item> resultsList = new List<Item>();
            int i = 0;
            foreach (Item child in parentItem.Children)
            {
                if (LandmarkHelper.IsShownInNavigation(child))
                {
                    resultsList.Insert(i, child);
                    i++;
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

        public static string GetSlideClass(Item item)
        {
            string styleClass = string.Empty;
            //var multilistField = (MultilistField)Sitecore.Context.Item.Fields["MultilistFieldName"];
            var mainStyleField = (ReferenceField) item.Fields["Main Version Slide Style"];
            var subStyleField = (ReferenceField) item.Fields["Sub Version Slide Style"];
            if (mainStyleField.Value.Equals(ItemGuids.FullSizePhotoOverlayText) &
                subStyleField.Value.Equals(ItemGuids.TwoNumbersTwoCaptionsTextLink))
            {
                styleClass = "panel-3";
            }
            if (mainStyleField.Value.Equals(ItemGuids.LeftPhotoRightText) &
                subStyleField.Value.Equals(ItemGuids.TwoNumbersTwoCaptionsTextLink))
            {
                styleClass = "panel-1";
            }
            if (mainStyleField.Value.Equals(ItemGuids.LeftPhotoRightText) &
                subStyleField.Value.Equals(ItemGuids.OneTitleOneTextLink))
            {
                styleClass = "panel-2";
            }
            if (mainStyleField.Value.Equals(ItemGuids.FullSizePhotoOverlayText) &
                subStyleField.Value.Equals(ItemGuids.OneTitleOneTextLink))
            {
                styleClass = "panel-3";
            }
            return styleClass;
        }

        /// <summary>
        /// Gets the items by root and template.
        /// </summary>
        /// <param name="rootItem">The root item.</param>
        /// <param name="templateItem">The template item.</param>
        /// <returns>List{Item}.</returns>
        public static List<Item> GetItemsByRootAndTemplate(string rootItem, string templateItem)
        {
            Item shopping = Sitecore.Context.Database.GetItem(rootItem);
            var query = string.Format("fast:{0}//*[{1}]", shopping.Paths.FullPath,
                "@@TemplateId='" + templateItem + "'");
            return _webDb.SelectItems(query).ToList();
        }


        /// <summary>
        /// Gets the slides by template.
        /// </summary>
        /// <param name="templateId">The template unique identifier.</param>
        /// <returns>List{Item}.</returns>
        public static List<Item> GetItemByTemplate(string templateId)
        {
            var item = Sitecore.Context.Item;
            var query = string.Format("fast:{0}//*[{1}]", item.Paths.FullPath, "@@TemplateId='" + templateId + "'");
            List<Item> slidesItems = _webDb.SelectItems(query).OrderBy(i=>i.DisplayName).ToList();
            return slidesItems;
        }
    }
}
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
            CheckboxField field = (CheckboxField)item.Fields["Is Shown In Navigation"];
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
            var mainStyleField = (ReferenceField)item.Fields["Main Version Slide Style"];
            var subStyleField = (ReferenceField)item.Fields["Sub Version Slide Style"];
            if (mainStyleField.Value.Equals(ItemGuids.FullSizePhotoOverlayText) & subStyleField.Value.Equals(ItemGuids.TwoNumbersTwoCaptionsTextLink))
            {
                styleClass = "panel-3";
            }
            if (mainStyleField.Value.Equals(ItemGuids.LeftPhotoRightText) & subStyleField.Value.Equals(ItemGuids.TwoNumbersTwoCaptionsTextLink))
            {
                styleClass = "panel-1";
            }
            if (mainStyleField.Value.Equals(ItemGuids.LeftPhotoRightText) & subStyleField.Value.Equals(ItemGuids.OneTitleOneTextLink))
            {
                styleClass = "panel-2";
            }
            if (mainStyleField.Value.Equals(ItemGuids.FullSizePhotoOverlayText) & subStyleField.Value.Equals(ItemGuids.OneTitleOneTextLink))
            {
                styleClass = "panel-3";
            }
            return styleClass;
        }

        public static bool IfBrandsAlphabetValid(string s)
        {
            Database webDb = Factory.GetDatabase("web");
            Item shopping = Sitecore.Context.Database.GetItem(ItemGuids.ShoppingItem);
            var query = string.Format("fast:{0}//*[{1}]", shopping.Paths.FullPath, "@@TemplateId='" + ItemGuids.T14ShopDetailsTemplate + "'");
            List<Item> brandsItems = webDb.SelectItems(query).ToList();

            foreach (Item brand in brandsItems)
            {
                if (brand.Fields["Brand Title"].Value.ToLower().StartsWith(s))
                {
                    return true;
                }
            }
            return false;
        }

        public static List<LandmarkBrandModel> GetBrands()
        {
            List<LandmarkBrandModel> brandModels = new List<LandmarkBrandModel>();
            Database webDb = Factory.GetDatabase("web");
            Item shopping = Sitecore.Context.Database.GetItem(ItemGuids.ShoppingItem);
            var query = string.Format("fast:{0}//*[{1}]", shopping.Paths.FullPath, "@@TemplateId='" + ItemGuids.T14ShopDetailsTemplate + "'");
            List<Item> brandsItems = webDb.SelectItems(query).ToList();
            foreach (var item in brandsItems)
            {
                LandmarkBrandModel brandModel = new LandmarkBrandModel()
                {
                    Group = item["Brand Title"].Substring(0, 1),
                    Tag = item["Tags"],
                    BrandItem = item
                };
                brandModels.Add(brandModel);
            }
            return brandModels.OrderBy(p => p.Group).ToList();
        }

        public static List<Item> GetBrandsByBuildings(ID buildingId)
        {
            Database webDb = Factory.GetDatabase("web");
            Item shopping = Sitecore.Context.Database.GetItem(ItemGuids.ShoppingItem);
            var query = string.Format("fast:{0}//*[{1}]", shopping.Paths.FullPath, "@@TemplateId='" + ItemGuids.T14ShopDetailsTemplate + "'");
            List<Item> brandsItems = webDb.SelectItems(query).ToList();
            List<Item> brandsByBuildings = new List<Item>();
            foreach (Item brand in brandsItems)
            {
                var buildingsField = (MultilistField)brand.Fields["Buildings"];
                if (buildingsField != null)
                {
                    if (buildingsField.TargetIDs.Any(id => id.Guid == buildingId.Guid))
                    {
                        brandsByBuildings.Add(brand);
                    }
                }
            }
            return brandsByBuildings;
        }

        public static List<Item> GetBuildingsByCategory(ID categoryId)
        {
            Database webDb = Factory.GetDatabase("web");
            Item shopping = Sitecore.Context.Database.GetItem(ItemGuids.ShoppingItem);
            var query = string.Format("fast:{0}//*[{1}]", shopping.Paths.FullPath, "@@TemplateId='" + ItemGuids.T14ShopDetailsTemplate + "'");
            List<Item> brandsItems = webDb.SelectItems(query).ToList();
            List<Item> buildingsByCategory = new List<Item>();
            foreach (Item brand in brandsItems)
            {
                var tagsField = (MultilistField)brand.Fields["Tags"];
                if (tagsField != null)
                {
                    if (tagsField.TargetIDs.Any(id => id.Guid == categoryId.Guid))
                    {
                        var buildingsField = (MultilistField)brand.Fields["Buildings"];
                        if (buildingsField != null)
                        {
                            foreach (ID buidId in buildingsField.TargetIDs)
                            {
                                if (!buildingsByCategory.Contains(webDb.GetItem(buidId)))
                                {
                                    buildingsByCategory.Add(webDb.GetItem(buidId));
                                }
                            }
                        }
                    }
                }
            }
            return buildingsByCategory;

        }
    }
}
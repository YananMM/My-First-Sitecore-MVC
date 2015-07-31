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

        /// <summary>
        /// Gets the items by root and template.
        /// </summary>
        /// <param name="rootItem">The root item.</param>
        /// <param name="templateItem">The template item.</param>
        /// <returns>List{Item}.</returns>
        private static List<Item> GetItemsByRootAndTemplate(string rootItem, string templateItem)
        {
            Database webDb = Factory.GetDatabase("web");
            Item shopping = Sitecore.Context.Database.GetItem(rootItem);
            var query = string.Format("fast:{0}//*[{1}]", shopping.Paths.FullPath,
                "@@TemplateId='" + templateItem + "'");
            return webDb.SelectItems(query).ToList();
        }

        /// <summary>
        /// Gets the brand models.
        /// </summary>
        /// <returns>List{LandmarkBrandModel}.</returns>
        public static List<LandmarkBrandModel> GetBrandModels()
        {
            List<LandmarkBrandModel> brandModels = new List<LandmarkBrandModel>();
            List<Item> brandsItems = GetItemsByRootAndTemplate(ItemGuids.ShoppingItem, ItemGuids.T14ShopDetailsTemplate);
            foreach (var item in brandsItems)
            {
                LandmarkBrandModel brandModel = new LandmarkBrandModel()
                {
                    Group = item["Brand Title"].Substring(0, 1),
                    Tags = item["Tags"],
                    BrandItem = item
                };
                brandModels.Add(brandModel);
            }
            return brandModels.OrderBy(p => p.Group).ToList();
        }

        /// <summary>
        /// Gets the current category.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string GetCurrentCategory()
        {
            var parentItem = Sitecore.Context.Item.Parent;
            var grandParentItem = Sitecore.Context.Item.Parent.Parent;
            var shoppingCategory = GetCategorysByItem(ItemGuids.ShoppingCategory);
            var grandParentCategorys = shoppingCategory.SingleOrDefault(p => p.DisplayName == grandParentItem.DisplayName);
            var parentCategorys = GetCategorysByItem(grandParentCategorys.ID.ToString());
            string currentTag = String.Empty;
            foreach (var item in parentCategorys)
            {
                if (item.DisplayName == parentItem.DisplayName)
                {
                    currentTag = item.ID.ToString();
                }
            }
            return currentTag;
        }

        /// <summary>
        /// Gets the categorys by item.
        /// </summary>
        /// <param name="categoryId">The category unique identifier.</param>
        /// <returns>List{Item}.</returns>
        public static List<Item> GetCategorysByItem(string categoryId)
        {
            List<Item> shoppingCategorys = new ItemList();
            shoppingCategorys = GetItemsByRootAndTemplate(categoryId, ItemGuids.ShoppingCategoryObject);
            return shoppingCategorys;
        }

        /// <summary>
        /// Gets the brands groups.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>List{System.String}.</returns>
        public static List<string> GetBrandsGroups(string category)
        {
            List<LandmarkBrandModel> brandModels = GetBrandModels();
            if (!string.IsNullOrEmpty(category))
            {
                brandModels = brandModels.Where(p => p.Tags.Contains(category)).ToList();
            }
            List<string> brandGroups = new List<string>();

            foreach (var brand in brandModels)
            {
                brandGroups.Add(brand.Group.ToLower());
            }
            return brandGroups;
        }

        public static List<Item> GetCategorysInShopping() 
        {
            return GetItemsByRootAndTemplate(ItemGuids.ShoppingItem, ItemGuids.T11PageTemplate);
        }

        /// <summary>
        /// Checks the brand group.
        /// </summary>
        /// <param name="brandGroup">The brand group.</param>
        /// <param name="category">The category.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool CheckBrandGroup(string brandGroup, string category)
        {
            var brandGroups = GetBrandsGroups(category);
            if (brandGroups.Contains(brandGroup))
            {
                return true;
            }
            return false;
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
                if (tagsField.TargetIDs.Any())
                {
                    Item categoryItem = webDb.GetItem(categoryId);
                    if (tagsField.TargetIDs.Any(id => webDb.GetItem(id).DisplayName == categoryItem.DisplayName && webDb.GetItem(id).Parent.DisplayName == categoryItem.Parent.DisplayName))
                    {
                        var buildingsField = (MultilistField)brand.Fields["Buildings"];
                        if (buildingsField.TargetIDs != null && buildingsField.TargetIDs.Any())
                        {
                            foreach (ID buidId in buildingsField.TargetIDs.Where(buidId => !buildingsByCategory.Contains(webDb.GetItem(buidId))))
                            {
                                buildingsByCategory.Add(webDb.GetItem(buidId));
                            }
                        }
                    }
                }
            }
            return buildingsByCategory;

        }
    }
}
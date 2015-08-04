using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Landmark.Classes;
using Landmark.Models;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Landmark.Helper
{
    public class ShoppingHelper
    {
        private Database _webDb = Factory.GetDatabase("web");
        /// <summary>
        /// Gets the brand models.
        /// </summary>
        /// <returns>List{LandmarkBrandModel}.</returns>
        public List<LandmarkBrandModel> GetBrandModels()
        {
            List<LandmarkBrandModel> brandModels = new List<LandmarkBrandModel>();
            List<Item> brandsItems = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.ShoppingItem, ItemGuids.T14ShopDetailsTemplate);
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
        public string GetCurrentCategory()
        {
            var parentItem = Sitecore.Context.Item.Parent;
            var grandParentItem = Sitecore.Context.Item.Parent.Parent;
            var allshoppingCategories = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.ShoppingCategory, ItemGuids.CategoryObjectTemplate);
            var grandParentCategorys = allshoppingCategories.SingleOrDefault(p => p.DisplayName == grandParentItem.DisplayName);
            var parentCategorys = LandmarkHelper.GetItemsByRootAndTemplate(grandParentCategorys.ID.ToString(), ItemGuids.CategoryObjectTemplate);
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
        /// Gets the brands groups.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>List{System.String}.</returns>
        public List<string> GetBrandsGroups(string category)
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

        /// <summary>
        /// Gets the categorise.
        /// </summary>
        /// <returns>List{TextValue}.</returns>
        public List<TextValue> GetFirstCategory()
        {
            Item shoppingCategory = Sitecore.Context.Database.GetItem(ItemGuids.ShoppingCategory);
            Item shopping = Sitecore.Context.Database.GetItem(ItemGuids.ShoppingItem);
            var queryCategory = string.Format("fast:{0}//*[{1}]", shoppingCategory.Paths.FullPath, "@@TemplateId='" + ItemGuids.CategoryObjectTemplate + "'");
            List<TextValue> firstCategory = (from category in _webDb.SelectItems(queryCategory).ToList()
                                             from Item item in shopping.Children
                                             where item.DisplayName == category.DisplayName
                                             select new TextValue
                                             {
                                                 text = category["Category Name"],
                                                 value = item.ID.ToString()
                                             }).ToList();
            foreach (var item in firstCategory)
            {
                var subCategoriess = Sitecore.Context.Database.GetItem(item.value).Children.ToList();
                List<TextValue> children =
                    subCategoriess.Select(p => new TextValue
                    {
                        text = p["Page Title"],
                        value = p.ID.ToString()
                    }).ToList();
                item.children = children;
            }
            return firstCategory;
        }

        public List<string> GetRelatedCategoriesIDs()
        {
            var allshoppingCategories = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.ShoppingCategory, ItemGuids.CategoryObjectTemplate);
            Item currentShoppingPage = Sitecore.Context.Item;
            Item currentItem = Sitecore.Context.Item;
            if (currentItem.DisplayName == "By Brands" || currentItem.DisplayName == "By Buildings")
            {
                currentShoppingPage = currentItem.Parent.Parent;
            }
            List<string> relatedCategoriesIDs = new List<string>();
            foreach (var item in allshoppingCategories)
            {
                if (item.DisplayName == currentShoppingPage.DisplayName)
                {
                    var relatedCategories = item.Fields["Related Categoryies"].ToString();
                    if (!string.IsNullOrEmpty(relatedCategories))
                    {
                        relatedCategoriesIDs = relatedCategories.Split('|').ToList();
                        if (relatedCategoriesIDs.Count > 3)
                        {
                            relatedCategoriesIDs = relatedCategoriesIDs.GetRange(0, 3);
                        }
                    }
                }
            }
            return relatedCategoriesIDs;
        }

        /// <summary>
        /// Checks the brand group.
        /// </summary>
        /// <param name="brandGroup">The brand group.</param>
        /// <param name="category">The category.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool CheckBrandGroup(string brandGroup, string category)
        {
            var brandGroups = GetBrandsGroups(category);
            if (brandGroups.Contains(brandGroup))
            {
                return true;
            }
            return false;
        }

        public List<Item> GetBrandsByBuildings(ID buildingId)
        {
            Item shopping = Sitecore.Context.Database.GetItem(ItemGuids.ShoppingItem);
            var query = string.Format("fast:{0}//*[{1}]", shopping.Paths.FullPath, "@@TemplateId='" + ItemGuids.T14ShopDetailsTemplate + "'");
            List<Item> brandsItems = _webDb.SelectItems(query).ToList();
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

        public List<Item> GetBuildingsByCategory(ID categoryId)
        {
            Item shopping = Sitecore.Context.Database.GetItem(ItemGuids.ShoppingItem);
            var query = string.Format("fast:{0}//*[{1}]", shopping.Paths.FullPath, "@@TemplateId='" + ItemGuids.T14ShopDetailsTemplate + "'");
            List<Item> brandsItems = _webDb.SelectItems(query).ToList();
            List<Item> buildingsByCategory = new List<Item>();
            foreach (Item brand in brandsItems)
            {
                var tagsField = (MultilistField)brand.Fields["Tags"];
                if (tagsField.TargetIDs.Any())
                {
                    Item categoryItem = _webDb.GetItem(categoryId);
                    if (tagsField.TargetIDs.Any(id => _webDb.GetItem(id).DisplayName == categoryItem.DisplayName && _webDb.GetItem(id).Parent.DisplayName == categoryItem.Parent.DisplayName))
                    {
                        var buildingsField = (MultilistField)brand.Fields["Buildings"];
                        if (buildingsField.TargetIDs != null && buildingsField.TargetIDs.Any())
                        {
                            foreach (ID buidId in buildingsField.TargetIDs.Where(buidId => !buildingsByCategory.Contains(_webDb.GetItem(buidId))))
                            {
                                buildingsByCategory.Add(_webDb.GetItem(buidId));
                            }
                        }
                    }
                }
            }
            return buildingsByCategory;
        }

        public bool IfBrandsAlphabetValid(string s)
        {
            Item shopping = Sitecore.Context.Database.GetItem(ItemGuids.ShoppingItem);
            var query = string.Format("fast:{0}//*[{1}]", shopping.Paths.FullPath,
                "@@TemplateId='" + ItemGuids.T14ShopDetailsTemplate + "'");
            List<Item> brandsItems = _webDb.SelectItems(query).ToList();

            foreach (Item brand in brandsItems)
            {
                if (brand.Fields["Brand Title"].Value.ToLower().StartsWith(s))
                {
                    return true;
                }
            }
            return false;
        }


        public List<Item> GetT14Slides()
        {
            var item = Sitecore.Context.Item;
            var query = string.Format("fast:{0}//*[{1}]", item.Paths.FullPath, "@@TemplateId='" + ItemGuids.T14SlideObjectTemplate + "'");
            List<Item> T14SlidesItems = _webDb.SelectItems(query).ToList();
            return T14SlidesItems;
        }
    }
}
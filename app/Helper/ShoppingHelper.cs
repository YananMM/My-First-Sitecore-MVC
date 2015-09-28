﻿using System;
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
                                                 text = category["Tag Name"],
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
                    var relatedCategories = item.Fields["Related Tags"].ToString();
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
                var buildingsField = (ReferenceField)brand.Fields["Building"];
                if (buildingsField != null && buildingsField.TargetItem != null)
                {
                    if (buildingsField.TargetItem.ID.Guid == buildingId.Guid)
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
                        var buildingsField = (ReferenceField)brand.Fields["Building"];
                        if (buildingsField != null && buildingsField.TargetItem != null)
                        {
                            if (!buildingsByCategory.Contains(buildingsField.TargetItem))
                            {
                                buildingsByCategory.Add(buildingsField.TargetItem);
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

        /// <summary>
        /// Gets the shop page by tag.
        /// </summary>
        /// <param name="tagId">The tag unique identifier.</param>
        /// <returns>Item.</returns>
        public Item GetShopPageByTag(string tagId)
        {
            var tag = Sitecore.Context.Database.GetItem(tagId);
            var shoppingPages = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.ShoppingItem, ItemGuids.T11PageTemplate);

            //allShoppingPages 包括shopping page 和shopping sub page
            List<Item> allShoppingPages = new ItemList();
            allShoppingPages.AddRange(shoppingPages);
            foreach (var item in shoppingPages)
            {
                if (item.Children.Count != 0)
                {
                    allShoppingPages.AddRange(item.Children);
                }
            }
            if (allShoppingPages.Count != 0)
            {
                foreach (var item in allShoppingPages)
                {
                    if (item.DisplayName == tag.DisplayName)
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the shop floor.
        /// </summary>
        /// <returns>Item.</returns>
        public Item GetShopFloor(Item shopItem)
        {
            string floorId;
            var floorField = GetFloorId(shopItem, out floorId);
            if (floorField != null)
            {
                Item floor = Sitecore.Context.Database.GetItem(floorId);
                return floor;
            }
            return null;
        }

        private static MultilistField GetFloorId(Item shopItem, out string floorId)
        {
            MultilistField floorField = shopItem.Fields["Floor"];
            floorId = floorField.TargetIDs.First().ToString();
            return floorField;
        }

        private static MultilistField GetFloorId(out string floorId)
        {
            Item shop = Sitecore.Context.Item;
            MultilistField floorField = shop.Fields["Floor"];
            floorId = floorField.TargetIDs.First().ToString();
            return floorField;
        }

        /// <summary>
        /// Gets the brands by floor.
        /// </summary>
        /// <returns>List{Item}.</returns>
        public List<Item> GetBrandsByFloor()
        {
            string floorId = GetFloorId(out floorId).ToString();
            var allBrands = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.ShoppingItem, ItemGuids.T14ShopDetailsTemplate);
            var brandsByFloor = allBrands.Where(p => p.Fields["Floor"].ToString() == floorId).ToList();
            return brandsByFloor;
        }

        public List<Item> GetBrandsByFloor(Item floor)
        {
            var allBrands = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.ShoppingItem, ItemGuids.T14ShopDetailsTemplate);
            var brandsByFloor = allBrands.Where(p => p.Fields["Floor"].ToString() == floor.ID.ToString()).ToList();
            return brandsByFloor;
        }

        //mobile navigation
        public List<Item> GetFirstCategoryItems()
        {
            Item shoppingCategory = Sitecore.Context.Database.GetItem(ItemGuids.ShoppingCategory);
            Item shopping = Sitecore.Context.Database.GetItem(ItemGuids.ShoppingItem);
            var queryCategory = string.Format("fast:{0}//*[{1}]", shoppingCategory.Paths.FullPath, "@@TemplateId='" + ItemGuids.CategoryObjectTemplate + "'");
            List<Item> firstCategory = (from category in _webDb.SelectItems(queryCategory).ToList()
                                        from Item item in shopping.Children
                                        where item.DisplayName == category.DisplayName
                                        select item).ToList();
            return firstCategory;
        }

        /// <summary>
        /// Gets the related articles.
        /// </summary>
        /// <returns>List{Item}.</returns>
        public List<RelatedItem> GetRelatedArticles(Item item)
        {
            List<RelatedItem> relatedArticle = new List<RelatedItem>();
            var allArticles = LandmarkHelper.GetAllArticles();

            var brandTagsField = item.Fields["Tags"];
            var brandTags = brandTagsField.ToString().Split('|').ToList();
            foreach (var article in allArticles)
            {
                var articleTagsField = article.Fields["Tags"];
                var articleTags = articleTagsField.ToString().Split('|').ToList();
                var tags = articleTags.Intersect(brandTags).ToList();

                if (tags.Count() != 0)
                {
                    RelatedItem relatedItem = new RelatedItem
                    {
                        Item = article,
                        TagCount = tags.Count()
                    };
                    relatedArticle.Add(relatedItem);
                }
            }
            return relatedArticle.OrderBy(p => p.TagCount).ToList();
        }

        public Item GetCategoryFromItem(string itemid)
        {
            var allshoppingCategories = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.ShoppingCategory, ItemGuids.CategoryObjectTemplate);
            Item category = allshoppingCategories.SingleOrDefault(p => p.DisplayName == _webDb.GetItem(itemid).DisplayName);
            return category;
        }

    }
}
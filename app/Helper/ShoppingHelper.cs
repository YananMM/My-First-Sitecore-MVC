using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Landmark.Classes;
using Landmark.Models;
using Sitecore.Collections;
using Sitecore.Configuration;
using Sitecore.ContentSearch.Converters;
using Sitecore.ContentSearch.Utilities;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Web.UI.HtmlControls;

namespace Landmark.Helper
{
    public class ShoppingHelper
    {
        private Database _webDb = Factory.GetDatabase("web");
        private Item _parentItem = Sitecore.Context.Item.Parent;
        private bool isShop = true;
        private bool isDining = false;

        public ShoppingHelper()
        {
            if (Sitecore.Context.Item.Paths.Path.StartsWith("/sitecore/content/Home/Landmark/Shopping") ||
                Sitecore.Context.Item.Paths.Path.StartsWith("/sitecore/content/Home/Landmark/Dining"))
            {
                while (!_parentItem.ID.ToString().Equals(ItemGuids.ShoppingItem) && !_parentItem.ID.ToString().Equals(ItemGuids.DiningItem))
                {
                    _parentItem = _parentItem.Parent;
                }
                if (_parentItem.ID.ToString() == ItemGuids.ShoppingItem)
                {
                    isShop = true;
                    isDining = false;
                }
                else
                {
                    isDining = true;
                    isShop = false;
                }
            }
        }

        /// <summary>
        /// Gets the brand models.
        /// </summary>
        /// <returns>List{LandmarkBrandModel}.</returns>
        public List<LandmarkBrandModel> GetBrandModels(Item parentItem)
        {
            List<LandmarkBrandModel> brandModels = new List<LandmarkBrandModel>();
            List<Item> brandsItems = null;
            if(parentItem.ID.ToString()==ItemGuids.ShoppingItem)
                brandsItems = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.ShoppingItem, ItemGuids.T14ShopDetailsTemplate);
            else if (parentItem.ID.ToString() == ItemGuids.DiningItem)
            {
                brandsItems = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.DiningItem, ItemGuids.T14ShopDetailsTemplate);
            }
            foreach (var item in brandsItems)
            {
                LandmarkBrandModel brandModel = new LandmarkBrandModel()
                {
                    Group = item.DisplayName.Substring(0, 1).ToUpper(),
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
        public string GetCurrentCategory(Item currentItem)
        {
            var parentItem = currentItem.Parent;
            var grandParentItem = parentItem.Parent;
            List<Item> allshoppingCategories = null;
            if(parentItem.ID.ToString()==ItemGuids.ShoppingItem || grandParentItem.ID.ToString()==ItemGuids.ShoppingItem)
                allshoppingCategories = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.ShoppingCategory, ItemGuids.CategoryObjectTemplate);
            if(parentItem.ID.ToString()==ItemGuids.DiningItem || grandParentItem.ID.ToString()==ItemGuids.DiningItem)
                allshoppingCategories = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.DiningCategory, ItemGuids.CategoryObjectTemplate);

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
            List<LandmarkBrandModel> brandModels = GetBrandModels(Sitecore.Context.Item.Parent);
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
            Item parentItem = Sitecore.Context.Item.Parent;
            while (!parentItem.ID.ToString().Equals(ItemGuids.ShoppingItem) && !parentItem.ID.ToString().Equals(ItemGuids.DiningItem))
            {
                parentItem = parentItem.Parent;
            }
            Item brandCategory;
            Item brand;
            if (isShop)
            {
                brandCategory = Sitecore.Context.Database.GetItem(ItemGuids.ShoppingCategory);
                brand = Sitecore.Context.Database.GetItem(ItemGuids.ShoppingItem);
            }
            else
            {
                brandCategory = Sitecore.Context.Database.GetItem(ItemGuids.DiningCategory);
                brand = Sitecore.Context.Database.GetItem(ItemGuids.DiningItem);
            }
            var queryCategory = string.Format("fast:{0}//*[{1}]", brandCategory.Paths.FullPath, "@@TemplateId='" + ItemGuids.CategoryObjectTemplate + "'");
            List<TextValue> firstCategory = (from category in _webDb.SelectItems(queryCategory).ToList()
                                             from Item item in brand.Children
                                             where item.DisplayName == category.DisplayName
                                             select new TextValue
                                             {
                                                 text = category.DisplayName,
                                                 value = item.ID.ToString()
                                             }).ToList();
            foreach (var item in firstCategory)
            {
                var subCategoriess = Sitecore.Context.Database.GetItem(item.value).Children.ToList();
                List<TextValue> children =
                    subCategoriess.Select(p => new TextValue
                    {
                        text = p.DisplayName,
                        value = p.ID.ToString()
                    }).ToList();
                item.children = children;
            }
            return firstCategory;
        }

        public List<string> GetRelatedCategoriesIDs()
        {
            
            List<Item> allshoppingCategories = null;
            if (isShop)
            {
                allshoppingCategories = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.ShoppingCategory, ItemGuids.CategoryObjectTemplate);
            }
            else
            {
                allshoppingCategories = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.DiningCategory, ItemGuids.CategoryObjectTemplate);
            }
            
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

        public List<Item> GetBrandsByBuildings(string category, ID buildingId)
        {
            Item parentItem = Sitecore.Context.Item.Parent;
            while (!parentItem.ID.ToString().Equals(ItemGuids.ShoppingItem) && !parentItem.ID.ToString().Equals(ItemGuids.DiningItem))
            {
                parentItem = parentItem.Parent;
            }
            Item item = null;
            if (parentItem.ID.ToString() == ItemGuids.ShoppingItem)
            {
                item = Sitecore.Context.Database.GetItem(ItemGuids.ShoppingItem);
            }
            else
            {
                item = Sitecore.Context.Database.GetItem(ItemGuids.DiningItem);
            }

            var query = string.Format("fast:{0}//*[{1}]", item.Paths.FullPath, "@@TemplateId='" + ItemGuids.T14ShopDetailsTemplate + "'");
            List<Item> brandsItems = _webDb.SelectItems(query).ToList();
            List<Item> brandsByCategory = new List<Item>();
            if (!string.IsNullOrEmpty(category))
            {
                foreach (var brand in brandsItems)
                {
                    var tagField = brand.Fields["Tags"];
                    if (tagField.Value.Contains(category))
                    {
                        brandsByCategory.Add(brand);
                    }
                }
            }
            else
            {
                brandsByCategory = brandsItems;
            }
            List<Item> brandsByBuildings = new List<Item>();
            foreach (Item brand in brandsByCategory)
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
            Item item = null;
            if(isShop)
                item = Sitecore.Context.Database.GetItem(ItemGuids.ShoppingItem);
            if(isDining)
                item = Sitecore.Context.Database.GetItem(ItemGuids.DiningItem);
            var query = string.Format("fast:{0}//*[{1}]", item.Paths.FullPath, "@@TemplateId='" + ItemGuids.T14ShopDetailsTemplate + "'");
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
            Item item = null;
            if (isShop)
                item = Sitecore.Context.Database.GetItem(ItemGuids.ShoppingItem);
            if (isDining)
                item = Sitecore.Context.Database.GetItem(ItemGuids.DiningItem);
            var query = string.Format("fast:{0}//*[{1}]", item.Paths.FullPath,
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
        /// Checks the brand status.
        /// </summary>
        /// <param name="brandModels">The brand models.</param>
        /// <param name="s">The arguments.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool checkBrandStatus(List<LandmarkBrandModel> brandModels, string s)
        {
            foreach (var brand in brandModels)
            {
                if (brand.BrandItem.Fields["Brand Title"].Value.ToLower().StartsWith(s))
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
            List<Item> shoppingPages = null;
            if (isShop)
                shoppingPages = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.ShoppingItem, ItemGuids.T11PageTemplate);
            if (isDining)
                shoppingPages = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.DiningItem, ItemGuids.T11PageTemplate);

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
            List<Item> allBrands = null;
            if (isShop)
                allBrands = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.ShoppingItem, ItemGuids.T14ShopDetailsTemplate);
            if (isDining)
                allBrands = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.DiningItem, ItemGuids.T11PageTemplate);
            var brandsByFloor = allBrands.Where(p => p.Fields["Floor"].ToString() == floorId).ToList();
            return brandsByFloor;
        }

        public List<Item> GetBrandsByFloor(Item floor)
        {
            List<Item> allBrands = null;
            if (isShop)
                allBrands = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.ShoppingItem, ItemGuids.T14ShopDetailsTemplate);
            if (isDining)
                allBrands = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.DiningItem, ItemGuids.T11PageTemplate);
            var brandsByFloor = allBrands.Where(p => p.Fields["Floor"].ToString() == floor.ID.ToString()).ToList();
            return brandsByFloor;
        }

        //mobile navigation
        public List<Item> GetFirstCategoryItems()
        {
            Item shoppingCategory = null;
            Item shopping = null;
            if (isShop)
            {
                 shoppingCategory = Sitecore.Context.Database.GetItem(ItemGuids.ShoppingCategory);
                 shopping = Sitecore.Context.Database.GetItem(ItemGuids.ShoppingItem);
            }
            if (isDining)
            {
                shoppingCategory = Sitecore.Context.Database.GetItem(ItemGuids.DiningCategory);
                shopping = Sitecore.Context.Database.GetItem(ItemGuids.DiningItem);
            }
            
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
            Item item = _webDb.GetItem(itemid);
            List<Item> allshoppingCategories = null;
            if (isShop)
            {
                allshoppingCategories = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.ShoppingCategory, ItemGuids.CategoryObjectTemplate);
            }
            else
            {
                allshoppingCategories = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.DiningCategory, ItemGuids.CategoryObjectTemplate);
            }
            Item category = allshoppingCategories.SingleOrDefault(p => p.DisplayName == item.DisplayName);
            return category;
        }

    }
}
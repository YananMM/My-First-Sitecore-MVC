using System;
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

        /// <summary>
        /// Gets the slides by template.
        /// </summary>
        /// <param name="templateId">The template unique identifier.</param>
        /// <returns>List{Item}.</returns>
        public List<Item> GetSlidesByTemplate(string templateId)
        {
            var item = Sitecore.Context.Item;
            var query = string.Format("fast:{0}//*[{1}]", item.Paths.FullPath, "@@TemplateId='" + templateId + "'");
            List<Item> slidesItems = _webDb.SelectItems(query).ToList();
            return slidesItems;
        }

        public List<TagsTree> GetTagsTree()
        {
            List<TagsTree> tagsTrees = new List<TagsTree>();
            List<Item> allSubTags = new ItemList();
            MultilistField tagsField = Sitecore.Context.Item.Fields["Tags"];
            if (tagsField != null)
            {
                allSubTags = tagsField.GetItems().ToList();
                var tagGroups = allSubTags.GroupBy(p => p.ParentID).Select(p => new { id = p.Key, children = p }).ToList();
                foreach (var item in tagGroups)
                {
                    TagsTree tagsTree = new TagsTree()
                    {
                        CurrentItem = Sitecore.Context.Database.GetItem(item.id.ToString()),
                        Children = item.children.Select(p => new TagsTree()
                        {
                            CurrentItem = Sitecore.Context.Database.GetItem(p.ID.ToString()),
                        }).ToList()
                    };
                    tagsTrees.Add(tagsTree);
                }
            }
            return tagsTrees;
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
        public Item GetShopFloor()
        {
            string floorId;
            var floorField = GetFloorId(out floorId);
            if (floorField != null)
            {
                Item floor = Sitecore.Context.Database.GetItem(floorId);
                return floor;
            }
            return null;
        }

        private static MultilistField GetFloorId(out string floorId)
        {
            Item item = Sitecore.Context.Item;
            MultilistField floorField = item.Fields["Floor"];
            floorId = floorField.TargetIDs.First().ToString();
            return floorField;
        }

        /// <summary>
        /// Gets the brands by floor.
        /// </summary>
        /// <param name="floorId">The floor unique identifier.</param>
        /// <returns>List{Item}.</returns>
        public List<Item> GetBrandsByFloor()
        {
            string floorId = GetFloorId(out floorId).ToString();
            var allBrands = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.ShoppingItem, ItemGuids.T14ShopDetailsTemplate);
            var brandsByFloor = allBrands.Where(p => p.Fields["Floor"].ToString() == floorId).ToList();
            return brandsByFloor;
        }
    }
}
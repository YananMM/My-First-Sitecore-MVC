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
using Sitecore.Data.Query;
using Sitecore.Links;
using Sitecore.Mvc.Pipelines.Response.RenderRendering;
using Sitecore.Web.UI.HtmlControls;

namespace Landmark.Helper
{
    public class ShoppingHelper
    {
        private Database _webDb = Factory.GetDatabase("web");
        private Item _parentItem = null;
        public bool isShop = true;
        public bool isDining = false;

        public ShoppingHelper()
        {
            if (Sitecore.Context.Item != null)
            {
                if (Sitecore.Context.Item.Paths.Path.StartsWith("/sitecore/content/Home/Landmark/Shopping") ||
                Sitecore.Context.Item.Paths.Path.StartsWith("/sitecore/content/Home/Landmark/Dining"))
                {
                    _parentItem = Sitecore.Context.Item.Parent;
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

        }

        /// <summary>
        /// Gets the brand models.
        /// </summary>
        /// <returns>List{LandmarkBrandModel}.</returns>
        public List<LandmarkBrandModel> GetBrandModels(Item parentItem)
        {
            List<LandmarkBrandModel> brandModels = new List<LandmarkBrandModel>();
            List<Item> brandsItems = null;
            if (parentItem.ID.ToString() == ItemGuids.ShoppingItem)
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
            List<Item> allCategories = null;
            string currentTag = String.Empty;
            if (isShop)
                allCategories = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.ShoppingCategory, ItemGuids.CategoryObjectTemplate);
            if (isDining)
                allCategories = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.DiningCategory, ItemGuids.CategoryObjectTemplate);
            if (grandParentItem.ID.ToString() == ItemGuids.DiningItem)
            {
                allCategories = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.DiningCategory, ItemGuids.CategoryObjectTemplate);
                Item currentCategory = allCategories.FirstOrDefault(i => i.DisplayName.Replace(i.Parent.DisplayName + "-", "") == parentItem.DisplayName);
                if (currentCategory != null)
                {
                    currentTag = currentCategory.ID.ToString();
                }
                return currentTag;
            }
            var subCategories = LandmarkHelper.GetItemByTemplate(parentItem, ItemGuids.ShoppingSubCategoryPageObject);
            if (subCategories == null || !subCategories.Any())
            {
                Item currentCategory = allCategories.FirstOrDefault(i => i.DisplayName.Replace(i.Parent.DisplayName + "-", "") == parentItem.DisplayName);
                if (currentCategory != null)
                {
                    currentTag = currentCategory.ID.ToString();
                }
                return currentTag;
            }

            var grandParentCategorys = allCategories.SingleOrDefault(p => p.DisplayName == grandParentItem.DisplayName);
            var parentCategorys = LandmarkHelper.GetItemsByRootAndTemplate(grandParentCategorys.ID.ToString(), ItemGuids.CategoryObjectTemplate);

            foreach (var item in parentCategorys)
            {
                if (item.DisplayName.Contains(parentItem.DisplayName))
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
        public List<TextValue> GetFirstCategory(string id)
        {
            var currentItem = Factory.GetDatabase("web").GetItem(id);
            Item parentItem = currentItem.Parent;
            Item brandCategory;
            Item brand;
            //if (parentItem.ID.ToString() == ItemGuids.DiningItem)
            //{
            //    brandCategory = Sitecore.Context.Database.GetItem(ItemGuids.DiningCategory);
            //    brand = Sitecore.Context.Database.GetItem(ItemGuids.DiningItem);
            //    var diningQueryCategory = string.Format("fast:{0}//*[{1}]", brandCategory.Paths.FullPath, "@@TemplateId='" + ItemGuids.CategoryObjectTemplate + "'");
            //    List<TextValue> diningFirstCategory = (from category in _webDb.SelectItems(diningQueryCategory).ToList()
            //                                     from Item item in brand.Children
            //                                     where item.DisplayName == category.DisplayName && item.TemplateID.ToString() == ItemGuids.T11PageTemplate
            //                                     select new TextValue
            //                                     {
            //                                         text = category.DisplayName,
            //                                         value = item.ID.ToString()
            //                                     }).ToList();
            //    foreach (var item in diningFirstCategory)
            //    {
            //        var subCategoriess = Sitecore.Context.Database.GetItem(item.value).Children.Where(i => i.TemplateID.ToString() == ItemGuids.ShoppingSubCategoryPageObject).ToList();
            //        List<TextValue> children =
            //            subCategoriess.Select(p => new TextValue
            //            {
            //                text = p.DisplayName,
            //                value = p.ID.ToString()
            //            }).ToList();
            //        item.children = children;
            //    }
            //}
            while (!parentItem.ID.ToString().Equals(ItemGuids.ShoppingItem) && !parentItem.ID.ToString().Equals(ItemGuids.DiningItem))
            {
                parentItem = parentItem.Parent;
            }

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
                                             where item.DisplayName == category.DisplayName && item.TemplateID.ToString() == ItemGuids.T11PageTemplate
                                             select new TextValue
                                             {
                                                 text = category["Tag Name"],
                                                 DisplayName = category.DisplayName,
                                                 value = category.ID.ToString()
                                             }).OrderBy(p => p.DisplayName).ToList();
            foreach (var item in firstCategory)
            {
                var subCategoriess = Sitecore.Context.Database.GetItem(item.value).Children.Where(i => i.TemplateID.ToString() == ItemGuids.CategoryObjectTemplate).ToList();
                List<TextValue> children =
                    subCategoriess.Select(p => new TextValue
                    {
                        text = p["Tag Name"],
                        DisplayName = p.DisplayName.Replace(p.Parent.DisplayName + "-", ""),
                        value = p.ID.ToString()
                    }).OrderBy(p => p.DisplayName).ToList();
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
                MultilistField floorField = brand.Fields["Floor"];
                if (floorField != null && floorField.TargetIDs.Any())
                {
                    var floorid = floorField.TargetIDs.First().ToString();
                    var buildingItem = Factory.GetDatabase("web").GetItem(floorid).Parent;
                    if (buildingItem.ID.Guid == buildingId.Guid)
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
            if (isShop)
                item = Sitecore.Context.Database.GetItem(ItemGuids.ShoppingItem);
            if (isDining)
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
                        MultilistField floorField = brand.Fields["Floor"];
                        if (floorField != null && floorField.TargetIDs.Any())
                        {
                            var floorid = floorField.TargetIDs.First().ToString();
                            var buildingItem = Factory.GetDatabase("web").GetItem(floorid).Parent;
                            if (!buildingsByCategory.Contains(buildingItem))
                            {
                                buildingsByCategory.Add(buildingItem);
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
            Item floor = null;
            var floorField = GetFloorId(shopItem, out floorId);
            if (floorField != null)
            {
                if (!string.IsNullOrEmpty(floorId))
                {
                    floor = Sitecore.Context.Database.GetItem(floorId);
                }
                return floor;
            }
            return null;
        }

        private static MultilistField GetFloorId(Item shopItem, out string floorId)
        {
            MultilistField floorField = shopItem.Fields["Floor"];
            floorId = string.Empty;
            if (floorField.TargetIDs.Any())
            {
                floorId = floorField.TargetIDs.First().ToString();
            }
            return floorField;
        }

        private static MultilistField GetFloorId(out string floorId)
        {
            Item shop = Sitecore.Context.Item;
            MultilistField floorField = shop.Fields["Floor"];
            floorId = string.Empty;
            if (floorField.TargetIDs.Any())
            {
                floorId = floorField.TargetIDs.First().ToString();
            }
            return floorField;
        }

        /// <summary>
        /// Gets the brands by floo
        /// </summary>
        /// <returns>List{Item}.</returns>
        public List<Item> GetBrandsByFloor()
        {
            string floorId = string.Empty;
            string floorIdField = GetFloorId(out floorId).ToString();
            List<Item> allBrands = null;
            List<Item> brandsByFloor = null;

            if (!string.IsNullOrEmpty(floorId))
            {
                if (isShop)
                    allBrands = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.ShoppingItem, ItemGuids.T11PageTemplate);
                if (isDining)
                    allBrands = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.DiningItem, ItemGuids.T11PageTemplate);
                var brandsWithFloor = allBrands.Where(p => p.Fields["Floor"] != null);
                brandsByFloor = brandsWithFloor.Where(p => ((MultilistField)p.Fields["Floor"]).TargetIDs.First().ToString() == floorId).ToList();
            }
            return brandsByFloor;
        }

        public List<Item> GetBrandsByFloor(Item floor)
        {
            List<Item> allBrands = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.LandmarkHomeItem, ItemGuids.T14ShopDetailsTemplate);
            var brandsWithFloor = allBrands.Where(p => p.Fields["Floor"] != null);
            var brandsByFloor = brandsWithFloor.Where(p => p.Fields["Floor"].ToString() == floor.ID.ToString()).ToList();
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

        public string GetCategoryPageUrl(Item item)
        {
            Item target = item.Children.FirstOrDefault(i => i.DisplayName == "By Brands");
            if (target != null)
            {
                return LandmarkHelper.TranslateUrl(LinkManager.GetItemUrl(target));
            }
            else
            {
                return LandmarkHelper.GetItemUrl(item);
            }
        }

        public List<Item> GetRandomCategory()
        {
            Item context = Sitecore.Context.Item;
            Guid[] templatesid = new[]
            {
                new Guid(ItemGuids.T4PageTemplate), 
                new Guid(ItemGuids.T27PageTemplate),
                new Guid(ItemGuids.T23PageABTemplate),
                new Guid(ItemGuids.T23PageCDTemplate), 
                new Guid(ItemGuids.T25PageTemplate)
            };
            List<Item> allCategories = null;
            List<Item> randomArticles = null;
            if (isShop)
                allCategories = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.ShoppingCategory, ItemGuids.CategoryObjectTemplate);
            if (isDining)
                allCategories = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.DiningCategory, ItemGuids.CategoryObjectTemplate);
            Item currentCategory = allCategories.Where(i => i.DisplayName == context.DisplayName).FirstOrDefault();
            if (currentCategory != null)
            {
                List<Item> articles = LandmarkHelper.GetItemsByItemsTemplates(Sitecore.Context.Database.GetItem(ItemGuids.LandmarkHomeItem), templatesid);
                articles = articles.Where(item => item.Fields["Tags"] != null).ToList();
                articles = articles.Where(item => ((MultilistField)item.Fields["Tags"]).TargetIDs.Any()).ToList();
                articles = (from article in articles
                            from tagid in ((MultilistField)article.Fields["Tags"]).TargetIDs
                            where Sitecore.Context.Database.GetItem(tagid).Parent.ID.ToString() == currentCategory.ID.ToString()
                                  || tagid.ToString() == currentCategory.ID.ToString()
                            select article).Distinct().ToList();

                if (articles.Count > 3)
                {
                    Random random = new Random();
                    int num1 = random.Next(0, articles.Count);
                    int num2 = random.Next(0, num1);
                    int num3 = random.Next(num1, articles.Count);

                    randomArticles.Add(articles[num1]);
                    randomArticles.Add(articles[num2]);
                    randomArticles.Add(articles[num3]);
                }
                else
                {
                    return articles;
                }
            }

            return randomArticles;
        }


    }
}
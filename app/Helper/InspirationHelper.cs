using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using Landmark.Classes;
using Landmark.Models;
using Sitecore.Collections;
using Sitecore.Configuration;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Landmark.Helper
{
    public class InspirationHelper
    {
        private Database _webDb = Factory.GetDatabase("web");
        public List<Item> GetMonthlyExclusives(string category = null)
        {
            Item exclusiveItem = Sitecore.Context.Database.GetItem(ItemGuids.MonthlyExclusivePage);
            List<Item> articles = new List<Item>();
            if (category == null)
                articles = exclusiveItem.Children.ToList().Where(item => item.TemplateID.ToString() == ItemGuids.T27Page).ToList();
            else
            {
                articles = exclusiveItem.Children.ToList().Where(item => item.TemplateID.ToString() == ItemGuids.T27Page
                && ((MultilistField)item.Fields["Tags"]).TargetIDs.Contains(new ID(category))).ToList();
            }
            return articles;
        }

        public List<Item> GetDetailsMonthlyExclusives(string category = null)
        {
            Item exclusiveItem = Sitecore.Context.Database.GetItem(ItemGuids.MonthlyExclusivePage);
            List<Item> articles = new List<Item>();
            if (category == null)
                articles = exclusiveItem.Children.ToList().Where(item => item.TemplateID.ToString() == ItemGuids.T27Page && ((CheckboxField)item.Fields["Has Detailed"]).Checked).ToList();
            else
            {
                articles = exclusiveItem.Children.ToList().Where(item => item.TemplateID.ToString() == ItemGuids.T27Page && ((CheckboxField)item.Fields["Has Detailed"]).Checked
                && ((MultilistField)item.Fields["Tags"]).TargetIDs.Contains(new ID(category))).ToList();
            }
            return articles;
        }

        public List<Item> GetDetailedExclusives(string page = null)
        {
            List<Item> results = null;
            int pagenumber;
            pagenumber = page != null ? Int32.Parse(page) : 1;
            results = GetMonthlyExclusives().Where(item => ((CheckboxField)item.Fields["Has Detailed"]).Checked).ToList();
            return results.Skip((pagenumber - 1) * 8).Take(8).ToList();
        }

        public List<Item> GetNotDetailedExclusives(string page = null)
        {
            List<Item> results = null;
            int pagenumber;
            pagenumber = page != null ? Int32.Parse(page) : 1;
            List<Item> detailedItems = GetDetailedExclusives(page); 
            results = GetMonthlyExclusives().Where(item => !((CheckboxField)item.Fields["Has Detailed"]).Checked).ToList();
            if (detailedItems.Count() > pagenumber * 8)
            {
                return null;
            }
            else
            {
                int detailsPageNumber = detailedItems.Count()/8+1;
                int lastPageDetailsNumber = detailedItems.Count%8;
                if (detailsPageNumber <= pagenumber)
                {
                    return results.Skip((pagenumber-detailsPageNumber)*8).Take(8 - lastPageDetailsNumber).ToList();
                }
                else
                {
                    return results.Skip(8 - lastPageDetailsNumber + (pagenumber - detailsPageNumber) * 8).Take(8).ToList();
                }
                //return GetMonthlyExclusives().Skip((pagenumber - 1) * 8 - detailedItems.Count()).Take(8 - detailedItems.Count()).ToList();
            }
        }

        public bool IfBrandsAlphabetValid(string s)
        {
            List<Item> articles = GetMonthlyExclusives();
            List<Item> brands = new List<Item>();
            foreach (var article in articles)
            {
                MultilistField brandField = ((MultilistField)article.Fields["Brand"]);
                if (brandField != null && brandField.TargetIDs!=null && brandField.TargetIDs.Length>0)
                {
                    var brand = brandField.TargetIDs.FirstOrDefault();
                    if (_webDb.GetItem(brand) != null)
                        brands.Add(_webDb.GetItem(brand));
                }
            }

            foreach (Item brand in brands)
            {
                if (brand.Fields["Brand Title"].Value.ToLower().StartsWith(s))
                {
                    return true;
                }
            }
            return false;
        }

        public List<Item> GetCategories()
        {
            //List<Item> categories = new List<Item>();
            //List<Item> articles = GetMonthlyExclusives();
            //foreach (var article in articles)
            //{
            //    var tagField = ((MultilistField)article.Fields["Tags"]);
            //    if (tagField != null)
            //    {
            //        foreach (var id in tagField.TargetIDs)
            //        {
            //            bool has = false;
            //            foreach (var category in categories)
            //            {
            //                if (category.ID.ToString() == id.ToString())
            //                {
            //                    has = true;
            //                    break;
            //                }
            //            }
            //            if (!has)
            //                categories.Add(_webDb.GetItem(id));
                        
            //        }
            //    }
            //}
            List<Item> categories = Sitecore.Context.Database.GetItem(ItemGuids.ExlusivesCategoryFolder).Children.ToList();
            return categories;
        }

        public string GetTagsFilter(Item item)
        {
            string tagsClass = string.Empty;
            var tagsField = (MultilistField) item.Fields["Tags"];

            if (tagsField != null && tagsField.TargetIDs.Count() > 0)
            {
                foreach (var tag in tagsField.TargetIDs)
                {
                    tagsClass += " gdf-" +_webDb.GetItem(tag).DisplayName.ToLower().Replace(" ","");
                }
            }
            
            return tagsClass;
        }

        public string GetAlphabet(ID id)
        {
            string alphabetFilter = string.Empty;
            if (!id.IsNull)
            {
                Item brand = _webDb.GetItem(id);
                alphabetFilter += "gdf-" + brand.Fields["Brand Title"].Value.ToLower()[0];
            }

            return alphabetFilter;
        }
        /// <summary>
        /// Gets the related stories.
        /// </summary>
        /// <returns>List{RelatedItem}.</returns>
        public List<RelatedItem> GetRelatedItems(string type)
        {
            List<RelatedItem> relatedItems = new List<RelatedItem>();
            var currentItem = Sitecore.Context.Item;
            List<Item> allItems = new ItemList();
            if (type == "story")
            {
                var t23PagesAB = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.LandmarkMaganizePage,
                    ItemGuids.T23PageTemplate);
                var t23PageCD = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.LandmarkMaganizePage,
                    ItemGuids.T23PageCDTemplate);

                allItems = t23PagesAB.Union(t23PageCD).ToList();
            }
            else if (type == "brands")
            {
                var brands = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.ShoppingItem,
                    ItemGuids.T14ShopDetailsTemplate);
                allItems = brands;
            }
            var currentTagsField = currentItem.Fields["tags"];
            var itemTags = currentTagsField.ToString().Split('|').ToList();

            foreach (var item in allItems)
            {
                var itemTagsField = item.Fields["Tags"];
                var storyTags = itemTagsField.ToString().Split('|').ToList();
                var tags = storyTags.Intersect(itemTags).ToList();
                if (tags.Count != 0)
                {
                    RelatedItem relatedItem = new RelatedItem
                    {
                        Item = item,
                        TagCount = tags.Count()
                    };
                    relatedItems.Add(relatedItem);
                }
            }
            return relatedItems.OrderBy(p => p.TagCount).ToList();
        }
    }
}
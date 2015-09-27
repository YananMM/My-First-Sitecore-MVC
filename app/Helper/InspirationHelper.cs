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
    public class InspirationHelper
    {
        private Database _webDb = Factory.GetDatabase("web");
        public List<Item> GetMonthlyExclusives(string category=null,string brand=null)
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
            if(brand!=null)
                articles = articles.Where(item => ((MultilistField)item.Fields["Tags"]).TargetIDs.Contains(new ID(brand))).ToList();
            return articles;
        }

        public bool IfBrandsAlphabetValid(string s)
        {
            List<Item> articles = GetMonthlyExclusives();
            List<Item> brands = new List<Item>();
            foreach (var article in articles)
            {
                var brand = ((MultilistField)article.Fields["Brand"]).TargetIDs.FirstOrDefault();
                if (_webDb.GetItem(brand)!= null)
                    brands.Add(_webDb.GetItem(brand));
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
            List<Item> categories = new List<Item>();
            List<Item> articles = GetMonthlyExclusives();
            foreach (var article in articles)
            {
                var tagField = ((MultilistField)article.Fields["Tags"]);
                if (tagField != null)
                {
                    foreach (var id in tagField.TargetIDs)
                    {
                        
                            if (!categories.Contains(_webDb.GetItem(id)))
                                categories.Add(_webDb.GetItem(id));
                        
                    }
                }
            }
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
                    tagsClass += " gdf-" + tag.ToString().ToLower();
                }
            }
            
            return tagsClass;
        }

        public string GetAlphabet(ID id)
        {
            string alphabetFilter = string.Empty;
            Item brand = _webDb.GetItem(id);
            alphabetFilter += "gdf-" + brand.Fields["Brand Title"].Value.ToLower()[0];
            return alphabetFilter;
        }
    }
}
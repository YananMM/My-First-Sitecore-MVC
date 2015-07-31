using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;

namespace Landmark.Models
{
    public class LandmarkModels
    {
    }

    public class LandmarkBrandModel
    {
        public string Group { get; set; }

        public string Tags { get; set; }

        public Item BrandItem { get; set; }
    }

    public class CategoryModel
    {
        public string Value { get; set; }

        public string Text { get; set; }
    }

    public class LandmarkCategoryJsonModel
    {
        public CategoryModel Category { get; set; }

        public List<CategoryModel> SubCategories { get; set; }
    }
}
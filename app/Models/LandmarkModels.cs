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

    public class TagsTree
    {
        public string ID { get; set; }

        public string DisplayName { get; set; }

        public Item CurrentItem { get; set; }

        public List<TagsTree> Children { get; set; }
    }
}
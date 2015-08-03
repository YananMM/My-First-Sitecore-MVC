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

}
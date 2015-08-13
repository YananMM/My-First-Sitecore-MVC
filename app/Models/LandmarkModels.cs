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
        public Item CurrentItem { get; set; }

        public List<TagsTree> Children { get; set; }
    }


    public class ArtPieceModel
    {
        public Item Artist { get; set; }

        public Item Building { get; set; }

        public List<Item> ArtPieces { get; set; } 
    }

    public class FloorPlan
    {
        public string mapheight;
        public string mapwidth;
        public List<Floor> categories;
        public List<Level> levels;
    }

    public class Floor
    {
        public string id;
        public string title;
        public string color;
        public string show;
    }

    public class Level
    {
        public string id;
        public string title;
        public string map;
        public string minimap;
        public List<Location> locations;
    }

    public class Location
    {
        public string category;
        public string id;
        public string title;
        public string area;
        public string description;
        public string pin;
        public string x;
        public string y;
    }

}
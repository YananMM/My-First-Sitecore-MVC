// ***********************************************************************
// Assembly         : Landmark
// Author           : Jiang Lili
// Created          : 07-30-2015
//
// Last Modified By : Jiang Lili
// Last Modified On : 08-12-2015
// ***********************************************************************
// <copyright file="LandmarkModels.cs" company="Gruden">
//     Copyright (c) Gruden. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;

// <summary>
// The Models namespace.
// </summary>
namespace Landmark.Models
{
    /// <summary>
    /// Class LandmarkModels.
    /// </summary>
    public class LandmarkModels
    {
    }

    /// <summary>
    /// Class LandmarkBrandModel.
    /// </summary>
    public class LandmarkBrandModel
    {
        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        /// <value>The group.</value>
        public string Group { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>The tags.</value>
        public string Tags { get; set; }

        /// <summary>
        /// Gets or sets the brand item.
        /// </summary>
        /// <value>The brand item.</value>
        public Item BrandItem { get; set; }
    }

    /// <summary>
    /// Class TagsTree.
    /// </summary>
    public class TagsTree
    {
        /// <summary>
        /// Gets or sets the current item.
        /// </summary>
        /// <value>The current item.</value>
        public Item CurrentItem { get; set; }

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>The children.</value>
        public List<TagsTree> Children { get; set; }

    }


    /// <summary>
    /// Class ArtPieceModel.
    /// </summary>
    public class ArtPieceModel
    {
        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        /// <value>The artist.</value>
        public Item Artist { get; set; }

        /// <summary>
        /// Gets or sets the building.
        /// </summary>
        /// <value>The building.</value>
        public Item Building { get; set; }

        /// <summary>
        /// Gets or sets the art pieces.
        /// </summary>
        /// <value>The art pieces.</value>
        public List<Item> ArtPieces { get; set; }
    }

    /// <summary>
    /// Class Work.
    /// </summary>
    public class Work
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string des { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public string url { get; set; }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>The link.</value>
        public string link { get; set; }
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
        public string workdayhours;
        public string weekendhours;
        public string wherelocation;
        public string address;
        public string href;
    }

    /// <summary>
    /// Class MaganizeGroup.
    /// </summary>
    public class MaganizeGroup
    {
        public Item MagazineCategory { get; set; }

        public List<Item> Stories { get; set; }
    }

    public enum StorySetting
    {
        StyleA,
        StyleB,
        StyleCd
    }

    public class RelatedItem
    {
        public Item Item { get; set; }

        public int TagCount { get; set; }
    }
}
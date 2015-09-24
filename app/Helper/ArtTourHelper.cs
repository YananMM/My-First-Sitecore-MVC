// ***********************************************************************
// Assembly         : Landmark
// Author           : Jiang Lili
// Created          : 08-11-2015
//
// Last Modified By : Jiang Lili
// Last Modified On : 08-11-2015
// ***********************************************************************
// <copyright file="ArtTourHelper.cs" company="Gruden">
//     Copyright (c) Gruden. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Landmark.Classes;
using Landmark.Models;
using Sitecore.Collections;
using Sitecore.ContentSearch.LuceneProvider;
using Sitecore.ContentSearch.Utilities;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc.Helpers;
using Sitecore.Web.UI.XslControls;

// <summary>
// The Helper namespace.
// </summary>
namespace Landmark.Helper
{
    /// <summary>
    /// Class ArtTourHelper.
    /// </summary>
    public class ArtTourHelper
    {
        /// <summary>
        /// Gets the artist by art.
        /// </summary>
        /// <param name="art">The art.</param>
        /// <returns>Item.</returns>
        public Item GetArtistByArt(Item art)
        {
            GroupedDroplinkField artistField = art.Fields["Artist"];
            if (artistField != null)
            {
                return artistField.TargetItem;
            }
            return null;
        }


        /// <summary>
        /// Gets the floor.
        /// </summary>
        /// <param name="art">The art.</param>
        /// <returns>Item.</returns>
        public Item GetFloor(Item art)
        {
            GroupedDroplistField floorField = art.Fields["Floor and Building"];
            if (floorField != null)
            {
                var item = Sitecore.Context.Database.GetItem(floorField.Value);
                return item;
            }
            return null;
        }


        /// <summary>
        /// Gets the aet by building.
        /// </summary>
        /// <param name="buildingId">The building unique identifier.</param>
        /// <returns>List{Item}.</returns>
        public List<Item> GetArtByBuilding(string buildingId)
        {
            List<Item> artPieces = new ItemList();
            List<Item> allArtPieces = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.LandmarkArtTourItem, ItemGuids.T29Template);
            if (allArtPieces != null && allArtPieces.Count != 0)
            {
                foreach (var item in allArtPieces)
                {
                    var floor = Sitecore.Context.Database.GetItem(item.Fields["Floor and Building"].Value);
                    if (floor != null)
                    {
                        if (floor.Parent.ID.ToString() == buildingId)
                        {
                            artPieces.Add(item);
                        }
                    }
                }
            }
            return artPieces;
        }

        /// <summary>
        /// Gets the art by artist.
        /// </summary>
        /// <param name="artistId">The artist unique identifier.</param>
        /// <returns>List{Item}.</returns>
        public List<Item> GetArtByArtist(string artistId)
        {
            List<Item> artPieces = new ItemList();
            List<Item> allArtPieces = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.LandmarkArtTourItem, ItemGuids.T29Template);
            if (allArtPieces != null && allArtPieces.Count != 0)
            {
                foreach (var item in allArtPieces)
                {
                    GroupedDroplinkField artistField = item.Fields["Artist"];
                    var artist = artistField.TargetItem;
                    if (artist != null && artist.ID.ToString() == artistId)
                    {
                        artPieces.Add(item);
                    }
                }
            }
            return artPieces;
        }
        
        /// <summary>
        /// Gets the art piece json by building.
        /// </summary>
        public List<Item> GetArtPieceByBuilding(string buildingId)
        {
            List<Item> allArtPieces = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.LandmarkArtTourItem, ItemGuids.T29Template);
            List<Item> artPiecesByBuilding = (from art in allArtPieces
                                              where Sitecore.Context.Database.GetItem(art.Fields["Floor and Building"].Value).Parent.ID.ToString() == buildingId
                                              select art).ToList();
            return artPiecesByBuilding;
        }

        /// <summary>
        /// Gets all artists.
        /// </summary>
        /// <returns>List{Item}.</returns>
        public List<Item> GetAllArtists()
        {
            List<Item> allArtists = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.LandmarkArtTourItem,
                ItemGuids.T30Template).ToList();
            if (allArtists != null)
            {
                return allArtists.OrderBy(p => p["Artist Name"].ToString()).ToList();
            }
            return new List<Item>();
        }

        public Item GetCurrentBuilding(string buildId = null)
        {
            buildId = buildId == null
                ? LandmarkHelper.GetBuildings().FirstOrDefault().ID.ToString()
                : buildId;
            return Sitecore.Context.Database.GetItem(buildId);
        }
    }
}
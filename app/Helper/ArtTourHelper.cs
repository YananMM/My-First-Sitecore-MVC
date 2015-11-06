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
using System.Web.UI;
using Landmark.Classes;
using Landmark.Models;
using Sitecore.Collections;
using Sitecore.ContentSearch.LuceneProvider;
using Sitecore.ContentSearch.Utilities;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Query;
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
        public Item GetArtistByArt(ID artId)
        {
            var art = Sitecore.Context.Database.GetItem(artId);
            GroupedDroplinkField artistField = art.Fields["Artist"];
            if (artistField != null)
            {
                return artistField.TargetItem;
            }
            return GetAllArtists().FirstOrDefault();
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
        public List<Item> GetArtPieceByBuildingSvgId(string id, string page = null)
        {

            var buildings = LandmarkHelper.GetBuildings();
            Item buildingItem =
                buildings.Where(b => b.Fields["Building Svg Id"].ToString() == id).ToList().FirstOrDefault();
            List<Item> allArtPieces = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.LandmarkArtTourItem,
                ItemGuids.T29Template);
            List<Item> artPiecesByBuilding = new List<Item>();
            if (buildingItem != null)
            {
                foreach (var art in allArtPieces)
                {
                    var floorBuilding = art.Fields["Floor and Building"].Value;
                    if (!string.IsNullOrEmpty(floorBuilding))
                    {
                        if (Sitecore.Context.Database.GetItem(floorBuilding).Parent.ID.ToString() ==
                            buildingItem.ID.ToString())
                        {
                            artPiecesByBuilding.Add(art);
                        }
                    }
                }
            }
            int intPage = Int32.Parse(page ?? "1");
            return artPiecesByBuilding.Skip((intPage - 1) * 10).Take(10).ToList();
        }

        /// <summary>
        /// Gets all artists.
        /// </summary>
        /// <returns>List{Item}.</returns>
        public List<Item> GetAllArtists()
        {
            List<Item> allArtists = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.LandmarkArtTourItem,
                ItemGuids.T30Template).ToList();
            if (allArtists.Count != 0)
            {
                return allArtists.OrderBy(p => p["Artist Name"].ToString()).ToList();
            }
            return new List<Item>();
        }

        public Item GetCurrentBuilding(string buildId = null)
        {
            Item building = buildId == null
                ? LandmarkHelper.GetBuildings().FirstOrDefault()
                : LandmarkHelper.GetBuildings().Where(b => b.Fields["Building Svg Id"].Value == buildId).ToList().FirstOrDefault(); ;
            return building;
        }

        public List<Item> GetArtsByArtistPager(string artistId, int page = 1)
        {
            var arts = GetArtByArtist(artistId);
            return arts.Skip((page - 1) * 2).Take(2).ToList();
        }

        public List<ArtPieceModel> GetArtistArtsByPager(int page = 1)
        {
            var pageSizeField = Sitecore.Context.Item.Fields["Page Size"];

            List<ArtPieceModel> models = new List<ArtPieceModel>();
            var artists = GetAllArtists();
            if (artists != null)
            {
                foreach (var item in artists)
                {
                    ArtPieceModel model = new ArtPieceModel();
                    var atrs = GetArtByArtist(item.ID.ToString());
                    model.Artist = item;
                    model.ArtPieces = GetArtsByArtistPager(item.ID.ToString());
                    models.Add(model);
                }
            }
            return models.Skip((page - 1) * Convert.ToInt32(pageSizeField.Value))
                        .Take(Convert.ToInt32(pageSizeField.Value)).ToList();
        }
    }
}
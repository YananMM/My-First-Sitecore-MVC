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
                    if (artist.ID.ToString() == artistId)
                    {
                        artPieces.Add(item);
                    }
                }
            }
            return artPieces;
        }

        /// <summary>
        /// Gets the artist models.
        /// </summary>
        /// <returns>List{ArtistModel}.</returns>
        public List<ArtPieceByArtistJson> GetArtPieceJsonByArtist()
        {
            List<ArtPieceByArtistJson> models = new List<ArtPieceByArtistJson>();
            List<Item> allArtists = GetAllArtists();
            if (allArtists != null && allArtists.Count != 0)
            {
                foreach (var item in allArtists)
                {
                    var artPieces = GetArtByArtist(item.ID.ToString());
                    ArtPieceByArtistJson model = new ArtPieceByArtistJson()
                    {
                        link = Sitecore.Links.LinkManager.GetItemUrl(item),
                        avatar = SitecoreFieldHelper.ImageFieldSrc("Artist Photo", item),
                        date = item.Fields["Artist Birthday Label"] + " " + item.Fields["Artist Birthday Text"],
                        name = item.Fields["Artist Name"].ToString(),
                        work = new List<Work>()
                    };
                    foreach (var piece in artPieces)
                    {
                        Work work = new Work()
                        {
                            link = Sitecore.Links.LinkManager.GetItemUrl(piece),
                            url = SitecoreFieldHelper.ImageFieldSrc("Art Image", piece),
                            title = piece.Fields["Art Title"].ToString(),
                            des = piece.Fields["Art Key"] + " " + piece.Fields["Art Size"]
                        };
                        model.work.Add(work);
                    }
                    model.work = model.work.OrderBy(p => p.title).ToList();
                    models.Add(model);
                }
            }
            //models = models.OrderBy(p => p.name).ToList();
            //if (models.Count > 1)
            //{
            //    models = models.GetRange(1, models.Count - 1);
            //} 
            return models;
        }

        /// <summary>
        /// Gets the first art piece model.
        /// </summary>
        /// <returns>ArtPieceModel.</returns>
        public ArtPieceModel GetFirstArtPieceModel()
        {
            ArtPieceModel model = new ArtPieceModel();
            var firstArtist = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.LandmarkArtTourItem,
                ItemGuids.T30Template).OrderBy(p => p.Fields["Artist Name"].ToString()).First();
            var artPieces = GetArtByArtist(firstArtist.ID.ToString());
            model.ArtPieces = artPieces.Count > 2 ? artPieces.GetRange(0, 2) : artPieces;
            model.Artist = firstArtist;
            return model;
        }

        /// <summary>
        /// Gets the art piece json by building.
        /// </summary>
        /// <returns>List{List{ArtPieceByBuildingJson}}.</returns>
        public List<List<ArtPieceByBuildingJson>> GetArtPieceJsonByBuilding(string buildingId)
        {
            List<List<ArtPieceByBuildingJson>> list = new List<List<ArtPieceByBuildingJson>>();
            List<ArtPieceByBuildingJson> models = new List<ArtPieceByBuildingJson>();
            List<Item> allArtPieces = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.LandmarkArtTourItem, ItemGuids.T29Template);
            List<Item> artPiecesByBuilding = (from art in allArtPieces
                                             where Sitecore.Context.Database.GetItem(art.Fields["Floor and Building"].Value).Parent.ID.ToString() == buildingId
                                             select art).ToList();
            if (allArtPieces != null && artPiecesByBuilding.Count != 0)
            {
                foreach (var item in artPiecesByBuilding)
                {
                    ArtPieceByBuildingJson model = new ArtPieceByBuildingJson()
                    {
                        title = item.Fields["Art Title"].ToString(),
                        src = SitecoreFieldHelper.ImageFieldSrc("Art Image", item),
                        link = Sitecore.Links.LinkManager.GetItemUrl(item),
                    };
                    GroupedDroplinkField artistField = item.Fields["Artist"];
                    model.des = artistField.TargetItem.Fields["Artist Name"].ToString();
                    models.Add(model);
                }
            }
            int j = 4;
            for (int i = 0; i < models.Count; i += 4)
            {
                List<ArtPieceByBuildingJson> cList = new List<ArtPieceByBuildingJson>();
                cList = models.Take(j).Skip(i).ToList();
                j += 4;
                list.Add(cList);
            }
            return list;
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
    }
}
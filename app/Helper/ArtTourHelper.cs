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
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

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
            Sitecore.Data.Fields.GroupedDroplinkField artistField = art.Fields["Artist"];
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
            List<Item> allArtPieces = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.LandmarkArtTourItem, Sitecore.Context.Item.TemplateID.ToString());
            if (allArtPieces != null && allArtPieces.Count != 0)
            {
                foreach (var item in allArtPieces)
                {
                    var building = Sitecore.Context.Database.GetItem(item.Fields["Floor and Building"].Value).Parent;
                    if (building.ID.ToString() == buildingId)
                    {
                        artPieces.Add(item);
                    }
                }
            }
            return artPieces;
        }
    }
}
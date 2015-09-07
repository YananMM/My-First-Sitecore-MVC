// ***********************************************************************
// Assembly         : Landmark
// Author           : Jiang Lili
// Created          : 09-07-2015
//
// Last Modified By : Jiang Lili
// Last Modified On : 09-07-2015
// ***********************************************************************
// <copyright file="AroundCentralHelper.cs" company="Gruden">
//     Copyright (c) Gruden. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Landmark.Classes;
using Sitecore.Collections;

namespace Landmark.Helper
{
    /// <summary>
    /// Class AroundCentralHelper.
    /// </summary>
    public class AroundCentralHelper
    {
        /// <summary>
        /// Gets the hotel slides.
        /// </summary>
        /// <returns>List{Item}.</returns>
        public List<Item> GetHotelSlides()
        {
            List<Item> items = new List<Item>();
            Item item = Sitecore.Context.Item;
            var root = item.Children.SingleOrDefault(p => p.TemplateID.ToString() == "{ECC8D83D-3EDD-46DB-A4AB-0E22806BBE3A}");
            items = root.Children.ToList();
            return items;
        }

        /// <summary>
        /// Gets the attractions by expercience.
        /// </summary>
        /// <param name="expercienceType">Type of the expercience.</param>
        /// <returns>List{Item}.</returns>
        public List<Item> GetAttractionsByExpercience(string expercienceType)
        {
            List<Item> attractions = new ItemList();
            var allAttractions = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.ExperienceCentral,
                ItemGuids.T25PageTemplate);
            if (!string.IsNullOrEmpty(expercienceType))
            {
                if (allAttractions != null && allAttractions.Count != 0)
                {
                    attractions =
                        allAttractions.Where(p => p.Fields["Experience Type"].ToString().Contains(expercienceType))
                            .ToList();
                }
                return attractions;
            }
            return allAttractions;
        } 
    }
}
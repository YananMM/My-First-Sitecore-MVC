// ***********************************************************************
// Assembly         : Landmark
// Author           : Jiang Lili
// Created          : 09-09-2015
//
// Last Modified By : Jiang Lili
// Last Modified On : 09-09-2015
// ***********************************************************************
// <copyright file="MagezineHelper.cs" company="Gruden">
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
using Sitecore.Data.Items;

namespace Landmark.Helper
{
    /// <summary>
    /// Class MagezineHelper.
    /// </summary>
    public class MagezineHelper
    {
        /// <summary>
        /// Gets all maganize categories.
        /// </summary>
        /// <returns>List{Item}.</returns>
        public List<Item> GetAllMaganizeCategories()
        {
            List<Item> categories = new ItemList();
            categories = Sitecore.Context.Database.GetItem(ItemGuids.LandmarkMagazineFolder).Children.ToList();
            return categories;
        }

        /// <summary>
        /// Gets all stories.
        /// </summary>
        /// <returns>List{Item}.</returns>
        public List<Item> GetAllStories()
        {
            List<Item> stories = new ItemList();
            stories = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.LandmarkMaganizePage, ItemGuids.T23PageTemplate);
            return stories;
        }

        /// <summary>
        /// Gets the stories by category.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <returns>List{Item}.</returns>
        public List<Item> GetStoriesByCategory(string categoryID) 
        {
            List<Item> stories = new ItemList();
            var allstories = GetAllStories();
            stories = allstories.Where(p => p.Fields["Magazine Category"].ToString().Contains(categoryID)).ToList();
            return stories;
        }

        /// <summary>
        /// Gets the maganize groups.
        /// </summary>
        /// <returns>List{MaganizeGroup}.</returns>
        public List<MaganizeGroup> GetMaganizeGroups()
        {
            List<MaganizeGroup> maganizeGroups = new List<MaganizeGroup>();

            //var categories = GetAllMaganizeCategories().OrderByDescending(p => p.Fields[Sitecore.FieldIDs.ArchiveDate]);
            var categories = GetAllMaganizeCategories();
            foreach (var item in categories)
            {
                MaganizeGroup maganizeGroup = new MaganizeGroup()
                {
                    Stories = GetStoriesByCategory(item.ID.ToString()),
                    MaganizeCategory = item
                };
                maganizeGroups.Add(maganizeGroup);
            }
            return maganizeGroups;
        }
    }
}
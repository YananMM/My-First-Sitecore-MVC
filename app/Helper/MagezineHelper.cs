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
using System.Web.UI.WebControls;
using Landmark.Classes;
using Landmark.Models;
using Sitecore.Collections;
using Sitecore.Data.Items;
using Sitecore.Data.Fields;

namespace Landmark.Helper
{
    /// <summary>
    /// Class MagezineHelper.
    /// </summary>
    public class MagezineHelper
    {
        /// <summary>
        /// The _page size
        /// </summary>
        private int _pageSize = 6;

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
        public List<Item> GetStoriesByCategory(string type)
        {
            List<Item> stories = new ItemList();
            var allstories = GetAllStories();
            if (!string.IsNullOrEmpty(type))
            {
                stories =
                    allstories.Where(p => p.Fields["Magazine Category"].ToString().Contains(type))
                        .OrderByDescending(p => p.Fields["Article Date"].ToString())
                        .ToList();
                return stories;
            }
            return allstories;
        }

        /// <summary>
        /// Gets the maganize groups.
        /// </summary>
        /// <returns>List{MaganizeGroup}.</returns>
        public List<MaganizeGroup> GetMagazineGroups()
        {
            List<MaganizeGroup> maganizeGroups = new List<MaganizeGroup>();

            //var categories = GetAllMaganizeCategories().OrderByDescending(p => p.Fields[Sitecore.FieldIDs.ArchiveDate]);
            var categories = GetAllMaganizeCategories();
            foreach (var item in categories)
            {
                MaganizeGroup maganizeGroup = new MaganizeGroup()
                {
                    Stories = GetStoriesByCategory(item.ID.ToString()),
                    MagazineCategory = item
                };
                maganizeGroups.Add(maganizeGroup);
            }
            return maganizeGroups;
        }

        /// <summary>
        /// Gets the stories by pager.
        /// </summary>
        /// <param name="categoryID">The category unique identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns>List{Item}.</returns>
        public List<Item> GetStoriesByPager(string type, int page = 1)
        {
            List<Item> stories = new ItemList();
            var allStories = GetStoriesByCategory(type);

            if (allStories != null && allStories.Count != 0)
            {
                if (page == 1)
                {
                    _pageSize = 7;
                }
                stories =
                    allStories.Skip((page - 1) * _pageSize)
                        .Take(_pageSize).ToList();
            }
            return stories;
        }

        /// <summary>
        /// Gets the story setting.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>StorySetting.</returns>
        public StorySetting GetStorySetting(Item item)
        {
            CheckboxField a = item.Fields["Slider On Top but not Left"];
            CheckboxField b = item.Fields["Slider Fill the Screen"];
            CheckboxField c = item.Fields["Show Big Cover not Small Cover"];
            StorySetting setting = new StorySetting
            {
                SliderOnTopNotLeft = a.Checked,
                SliderFilltheScreen = b.Checked,
                ShowBigCoverNotSmallCover = c.Checked,
            };
            return setting;
        }
    }
}

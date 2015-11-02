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
using Sitecore.ContentSearch.Utilities;
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
            var storiesAB = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.LandmarkMaganizePage, ItemGuids.T23PageTemplate);
            var storiesCD = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.LandmarkMaganizePage, ItemGuids.T23PageCDTemplate);
            stories.AddRange(storiesAB);
            stories.AddRange(storiesCD);
            return stories.OrderByDescending(p => p.Fields["Article Date"].ToString()).ToList();
        }

        /// <summary>
        /// Gets the top10 stories.
        /// </summary>
        /// <returns>List{Item}.</returns>
        public List<Item> GetTop10Stories()
        {
            var allStories = GetAllStories();
            var top10Stories = allStories.Count >= 10 ? allStories.Take(10) : allStories;
            return top10Stories.ToList();
        }

        /// <summary>
        /// Gets the random4 list.
        /// </summary>
        /// <returns>List{Item}.</returns>
        public List<Item> GetRandom4List()
        {
            var top10Stories = GetTop10Stories();
            Random random = new Random();
            List<Item> randomList = new ItemList();
            foreach (var item in top10Stories)
            {
                randomList.Insert(random.Next(randomList.Count), item);
            }
            var random4 = randomList.Count >= 4 ? randomList.Take(4).ToList() : randomList;
            return random4;
        }

        /// <summary>
        /// Gets the stories by category.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <returns>List{Item}.</returns>
        public List<Item> GetStoriesByCategory(string type, List<Item> random4List)
        {
            List<Item> stories = new ItemList();
            var allstories = GetAllStories();
            foreach (var item in random4List)
            {
                allstories = allstories.RemoveWhere(p=>p.ID == item.ID).ToList();
            }
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
        public List<Item> GetStoriesByCategory(string type)
        {
            List<Item> stories = new ItemList();
            var allstories = GetAllStories().ToList();
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
        public MaganizeGroupByRandom GetMagazineGroupsByRandom()
        {
            MaganizeGroupByRandom maganizeGroupsByRandom = new MaganizeGroupByRandom();
            var random4List = GetRandom4List();
            maganizeGroupsByRandom.Random4Stories = random4List;
            List<MaganizeGroup> random4Stories = new List<MaganizeGroup>();
            var categories = GetAllMaganizeCategories();
            foreach (var item in categories)
            {
                MaganizeGroup maganizeGroup = new MaganizeGroup()
                {
                    Stories = GetStoriesByCategory(item.ID.ToString(), random4List),
                    MagazineCategory = item
                };
                random4Stories.Add(maganizeGroup);
            }
            maganizeGroupsByRandom.MaganizeGroups = random4Stories;
            return maganizeGroupsByRandom;
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
            var style = item.Fields["Page Style"];
            if (style != null)
            {
                if (style.ToString() == "Style A")
                {
                    return StorySetting.StyleA;
                }
                else if (style.ToString() == "Style B")
                {
                    return StorySetting.StyleB;
                }
            }
            return StorySetting.StyleCd;
        }

        public List<Item> GetRelatedBrands()
        {
            List<Item> brands = new ItemList();



            return brands;
        }
    }
}

using Landmark.Classes;
using Landmark.Models;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.ContentSearch.Utilities;
using Sitecore.Rules.Conditions.DateTimeConditions;

namespace Landmark.Helper
{
    public class MagazineHelper
    {
        /// <summary>
        /// The _page size
        /// </summary>
        private int _pageSize = 6;

        public List<Item> GetAllMaganizeCategories(Item item)
        {
            List<Item> categories = new List<Item>();
            categories = item.Children.ToList();
            return categories;
        }

        public List<Item> GetAllStories()
        {
            List<Item> stories = new List<Item>();
            var storiesAB = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.LandmarkMaganizePage, ItemGuids.T23PageABTemplate);
            var storiesCD = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.LandmarkMaganizePage, ItemGuids.T23PageCTemplate);
            stories.AddRange(storiesAB);
            stories.AddRange(storiesCD);
            return stories;
        }
        public List<Item> GetAllStoriesByDate()
        {
            var allStories = GetAllStories();
            return allStories.OrderByDescending(p => p.Fields["Article Date"].ToString()).ToList();
        }

        public List<Item> GetTop10Stories()
        {
            var allStories = GetAllStoriesByDate();
            var top10Stories = allStories.Count >= 10 ? allStories.Take(10) : allStories;
            return top10Stories.ToList();
        }

        public List<Item> GetRandom4List()
        {
            var top10Stories = GetTop10Stories();
            Random random = new Random();
            List<Item> randomList = new List<Item>();
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
        /// <param name="categoryItem">The category item.</param>
        /// <param name="random4List">The random4 list.</param>
        /// <returns>List{Item}.</returns>
        public List<Item> GetStoriesByCategory(Item categoryItem, List<Item> random4List)
        {
            List<Item> stories = new List<Item>();
            var allstories = GetAllStories();
            allstories = random4List.Aggregate(allstories, (current, item) => current.RemoveWhere(p => p.ID == item.ID).ToList());
            List<Item> intersectList = new List<Item>();
            if (categoryItem != null)
            {
                var storieByCategory = categoryItem.Children.ToList();

                foreach (var item in random4List)
                {
                    foreach (var story in storieByCategory)
                    {
                        if (item.ID.ToString() == story.ID.ToString())
                        {
                            intersectList.Add(item);
                            storieByCategory.Remove(item);
                        }
                    }
                }
                stories = intersectList.Aggregate(storieByCategory, (current, item) => current.RemoveWhere(p => p.ID == item.ID).ToList());
            }
            return stories;
        }

        /// <summary>
        /// Gets the stories by category.T36用的
        /// </summary>
        /// <param name="categoryItem">The category item.</param>
        /// <returns>List{Item}.</returns>
        public List<Item> GetStoriesByCategory(Item categoryItem)
        {
            List<Item> stories = new List<Item>();
            //stories = categoryItem.Children.OrderByDescending(p => p.Fields["Article Date"].ToString()).ToList();
            stories =
                categoryItem.Children.ToList();
            return stories;
        }

        public MaganizeGroupByRandom GetMagazineGroupsByRandom()
        {
            MaganizeGroupByRandom maganizeGroupsByRandom = new MaganizeGroupByRandom();
            var random4List = GetRandom4List();
            maganizeGroupsByRandom.Random4Stories = random4List;
            List<MaganizeGroup> random4Stories = new List<MaganizeGroup>();
            var categories = GetAllMaganizeCategories(Sitecore.Context.Item);
            foreach (var item in categories)
            {
                MaganizeGroup maganizeGroup = new MaganizeGroup()
                {
                    Stories = GetStoriesByCategory(item, random4List),
                    MagazineCategory = item
                };
                random4Stories.Add(maganizeGroup);
            }
            maganizeGroupsByRandom.MaganizeGroups = random4Stories;
            return maganizeGroupsByRandom;
        }

        /// <summary>
        /// Gets the stories by pager. T37用的
        /// </summary>
        /// <param name="categoryItem">The category item.</param>
        /// <param name="page">The page.</param>
        /// <returns>List{Item}.</returns>
        public List<Item> GetStoriesByPager(Item categoryItem, int page = 1)
        {
            List<Item> stories = new List<Item>();
            var allStories = GetStoriesByCategory(categoryItem);

            if (allStories != null && allStories.Count != 0)
            {
                if (page == 1 || page == 0)
                {
                    stories =
                        allStories.Skip((page - 1) * 7)
                            .Take(7).ToList();
                }
                else
                {
                    stories =
                        allStories.Skip(((page - 1) * _pageSize) + 1)
                            .Take(_pageSize).ToList();
                }
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
    }
}
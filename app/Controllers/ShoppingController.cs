// ***********************************************************************
// Assembly         : Landmark
// Author           : Jiang Lili
// Created          : 07-30-2015
//
// Last Modified By : Jiang Lili
// Last Modified On : 08-10-2015
// ***********************************************************************
// <copyright file="ShoppingController.cs" company="Gruden">
//     Copyright (c) Gruden. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Landmark.Classes;
using Landmark.Helper;
using Landmark.Models;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;

// <summary>
// The Controllers namespace.
// </summary>
namespace Landmark.Controllers
{
    /// <summary>
    /// Class ShoppingController.
    /// </summary>
    public class ShoppingController : Controller
    {
        /// <summary>
        /// Goes the automatic.
        /// </summary>
        /// <param name="childcategory">The childcategory.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult GoTo(string category, string childcategory = null)
        {
            Session["category"] = category;
            Session["childcategory"] = childcategory;
            Item target = Sitecore.Context.Item;
            var categoryTag = Sitecore.Context.Database.GetItem(category);

            var childcategoryTag = Sitecore.Context.Database.GetItem(childcategory);

            var categoryDisplayName = categoryTag.DisplayName;

            if (categoryTag.Parent.ID.ToString() == ItemGuids.DiningCategory)
            {
                var diningPage = Sitecore.Context.Database.GetItem(ItemGuids.DiningItem);
                var diningItemPages = LandmarkHelper.GetItemByTemplate(diningPage, ItemGuids.T11PageTemplate);
                foreach (var item in diningItemPages)
                {
                    if (item.DisplayName == categoryDisplayName)
                    {
                        target = item;
                    }
                }
            }
            else if (categoryTag.Parent.ID.ToString() == ItemGuids.ShoppingItem)
            {
                var childcategoryDisplayName = childcategoryTag.DisplayName.Replace(categoryTag.Parent.DisplayName + "-", "");
                var shopPage = Sitecore.Context.Database.GetItem(ItemGuids.ShoppingItem);
                var shopItemPages = LandmarkHelper.GetItemByTemplate(shopPage, ItemGuids.T11PageTemplate);

                foreach (var item in shopItemPages)
                {
                    if (item.DisplayName == categoryDisplayName)
                    {
                        var subShopPages = item.Children.ToList();
                        if (subShopPages.Count > 0)
                        {
                            foreach (var subPage in subShopPages)
                            {
                                if (subPage.DisplayName == childcategoryDisplayName)
                                {
                                    target = subPage;
                                }
                            }
                        }
                    }
                }
            }
            return Redirect(LandmarkHelper.TranslateUrl(Sitecore.Links.LinkManager.GetItemUrl(target)));
        }

    }

}
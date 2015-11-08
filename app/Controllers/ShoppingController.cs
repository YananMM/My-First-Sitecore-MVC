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
        public ActionResult GoTo(string category=null, string childcategory=null)
        {
            Item shoppingCategory = Sitecore.Context.Database.GetItem(category);
            Session["category"] = category;
            if (!string.IsNullOrEmpty(childcategory))
            {
                shoppingCategory = Sitecore.Context.Database.GetItem(childcategory);
                Session["childcategory"] = childcategory;
                return Redirect(LandmarkHelper.TranslateUrl(Sitecore.Links.LinkManager.GetItemUrl(shoppingCategory.Children.First())));
            }
            else
            {
                return Redirect(LandmarkHelper.TranslateUrl(Sitecore.Links.LinkManager.GetItemUrl(shoppingCategory)));
            }
            
        }

    }

}
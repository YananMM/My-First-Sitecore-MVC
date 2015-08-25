// ***********************************************************************
// Assembly         : Landmark
// Author           : Jiang Lili
// Created          : 08-10-2015
//
// Last Modified By : Jiang Lili
// Last Modified On : 08-10-2015
// ***********************************************************************
// <copyright file="HomeController.cs" company="Gruden">
//     Copyright (c) Gruden. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Data.Items;
using Landmark.Models;
using Landmark.Helper;

// <summary>
// The Controllers namespace.
// </summary>
namespace Landmark.Controllers
{
    /// <summary>
    /// Class HomeController.
    /// </summary>
    public class HomeController : Controller
    {
        private ArtTourHelper _helper;

        public HomeController()
        {
            _helper = new ArtTourHelper();
        }

        /// <summary>
        /// Buttons the redirect.
        /// </summary>
        /// <param name="targetId">The target unique identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult ButtonRedirect(string targetId)
        {
            Item target = Sitecore.Context.Database.GetItem(targetId);
            return Redirect(Sitecore.Links.LinkManager.GetItemUrl(target));
        }
    }
}
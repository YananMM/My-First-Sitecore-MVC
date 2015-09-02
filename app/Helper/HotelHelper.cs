// ***********************************************************************
// Assembly         : Landmark
// Author           : Jiang Lili
// Created          : 08-31-2015
//
// Last Modified By : Jiang Lili
// Last Modified On : 08-31-2015
// ***********************************************************************
// <copyright file="AboutUsHelper.cs" company="Gruden">
//     Copyright (c) Gruden. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Landmark.Classes;
using Sitecore.Data.Items;
using Sitecore.Data.Fields;

namespace Landmark.Helper
{
    /// <summary>
    /// Class AboutUsHelper.
    /// </summary>
    public class HotelHelper
    {
        public List<Item> GetHotelSlides()
        {
            List<Item> items = new List<Item>();
            Item item = Sitecore.Context.Item;
            var root = item.Children.SingleOrDefault(p => p.TemplateID.ToString() == "{ECC8D83D-3EDD-46DB-A4AB-0E22806BBE3A}");
            items = root.Children.ToList();
            return items;
        }

    }
}
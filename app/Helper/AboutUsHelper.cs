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
    public class AboutUsHelper
    {
        /// <summary>
        /// Gets the related items.
        /// </summary>
        /// <returns>List{Item}.</returns>
        public List<Item> GetRelatedItems()
        {
            List<Item> items = new List<Item>();
            Item item = Sitecore.Context.Item;
            var formInternal = (CheckboxField)item.Fields["Related Articles From Internal Page"];
            if (formInternal != null)
            {
                if (!formInternal.Checked)
                {
                    var root = item.Children.SingleOrDefault(p => p.TemplateID.ToString() == ItemGuids.RelatedItemFolder);
                    items = LandmarkHelper.GetItemsByRootAndTemplate(root.ID.ToString(), ItemGuids.ArticleObject);
                }
            }
            else
            {
                var tags = item.Fields["Tags"];
            }
            return items;
        }
    }
}
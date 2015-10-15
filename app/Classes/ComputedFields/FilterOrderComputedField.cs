using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.ContentSearch;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.ContentSearch.ComputedFields;

namespace Landmark.Classes.ComputedFields
{
    public class FilterOrderComputedField : IComputedIndexField
    {
        public object ComputeFieldValue(IIndexable indexable)
        {
            Item item = indexable as SitecoreIndexableItem;
            if (item == null)
                return null;

            while(item.Parent.ID.ToString() != SitecoreItems.LandmarkHomeItem.ID.ToString())
            {
                item = item.Parent;
            }
            if (item.ID.ToString() == ItemGuids.ShoppingItem)
                return 1;
            if (item.ID.ToString() == ItemGuids.DiningItem)
                return 2;
            if (item.ID.ToString() == ItemGuids.NowAtLandmarkItem)
                return 3;
            if (item.ID.ToString() == ItemGuids.InspirationItem)
                return 4;
            if (item.ID.ToString() == ItemGuids.ServicesItem)
                return 5;
            if (item.ID.ToString() == ItemGuids.AroundCentralItem)
                return 6;

            return null;
        }
        public string FieldName { get; set; }
        public string ReturnType { get; set; }
    }
}
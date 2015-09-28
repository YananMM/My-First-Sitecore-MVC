using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Landmark.Classes.ComputedFields
{
    public class FilterTypeComputedField : IComputedIndexField
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
            var field = item.Fields["Page Title"];

            return field.Value;
        }
        public string FieldName { get; set; }
        public string ReturnType { get; set; }
    }
}
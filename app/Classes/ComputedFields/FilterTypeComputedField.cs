using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Data;
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

            if (item.TemplateID.ToString() == ItemGuids.T14ShopDetailsTemplate)
            {
                return StringConstants.ShoppingType;
            }
            if (item.TemplateID.ToString() == ItemGuids.T4PageTemplate)
            {
                return StringConstants.NowAtLandmarkType;
            }

            return null;
        }
        public string FieldName { get; set; }
        public string ReturnType { get; set; }
    }
}
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

            var field = (ReferenceField) item.Fields["Search Filter Type"];
            if (field.TargetItem == null)
                return null;
            else
            {
                return field.TargetItem.Fields["Value"].Value;
            }

            return null;
        }
        public string FieldName { get; set; }
        public string ReturnType { get; set; }
    }
}
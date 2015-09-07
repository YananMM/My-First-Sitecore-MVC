using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.ContentSearch;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Landmark.Classes.ComputedFields
{
    public class FilterOrderComputedField
    {
        public object ComputeFieldValue(IIndexable indexable)
        {
            Item item = indexable as SitecoreIndexableItem;
            if (item == null)
                return null;

            var field = (ReferenceField)item.Fields["Search Filter Type"];
            if (field.TargetItem == null)
                return null;
            else
            {
                var order = 1000;
                Int32.TryParse(field.TargetItem["__Sortorder"], out order);
                return order;
            }

            return null;
        }
        public string FieldName { get; set; }
        public string ReturnType { get; set; }
    }
}
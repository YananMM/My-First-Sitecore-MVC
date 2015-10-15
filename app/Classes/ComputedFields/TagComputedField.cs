using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.ContentSearch;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Configuration;

namespace Landmark.Classes.ComputedFields
{
    public class TagComputedField : IComputedIndexField
    {
        public object ComputeFieldValue(IIndexable indexable)
        { 
            Item item = indexable as SitecoreIndexableItem;
            if (item == null)
                return null;

            var field = (MultilistField)item.Fields["Tags"];
            if (field != null && field.TargetIDs != null )
            {
                string tags=string.Empty;
                foreach (var id in field.TargetIDs)
                {
                    tags += Factory.GetDatabase("web").GetItem(id).Fields["Tag Name"].Value+",";
                }
                return tags;
            }
            else
            {
                return null;
            }
        }
        public string FieldName { get; set; }
        public string ReturnType { get; set; }
    }
}
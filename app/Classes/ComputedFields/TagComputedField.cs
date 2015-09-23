using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.ContentSearch;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Landmark.Classes.ComputedFields
{
    public class TagComputedField
    {
        public object ComputeFieldValue(IIndexable indexable)
        {
            Item item = indexable as SitecoreIndexableItem;
            if (item == null)
                return null;

            var field = (MultilistField)item.Fields["Tags"];
            if (field != null && field.TargetIDs != null)
            {
                List<string> tags=new List<string>();
                foreach (var id in field.TargetIDs)
                {
                    tags.Add(Sitecore.Context.Database.GetItem(id).Fields["Category Name"].Value);
                }
                return tags;
            }
            else
            {
                return null;
            }

            return null;
        }
        public string FieldName { get; set; }
        public string ReturnType { get; set; }
    }
}
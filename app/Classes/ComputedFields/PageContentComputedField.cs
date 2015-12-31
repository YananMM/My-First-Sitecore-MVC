using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Configuration;
using Sitecore.ContentSearch;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.ContentSearch.ComputedFields;

namespace Landmark.Classes.ComputedFields
{
    public class PageContentComputedField : IComputedIndexField
    {
        public object ComputeFieldValue(IIndexable indexable)
        {
            Item item = indexable as SitecoreIndexableItem;
            if (item == null)
                return null;
            string result = string.Empty;
            if(item.Fields["Page Content"]!=null)
            {
                foreach (var lan in Landmark.Helper.LandmarkHelper.GetLanguages())
                {

                    Item lanItem = Sitecore.Context.Database.GetItem(item.ID, lan);
                    result += lanItem.Fields["Page Content"].Value;
                }
                return result;
            }

            return null;
        }
        public string FieldName { get; set; }
        public string ReturnType { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Configuration;
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
            while(item.Parent.ID.ToString() != Factory.GetDatabase("web").GetItem(ItemGuids.LandmarkHomeItem).ID.ToString())
            {
                item = item.Parent;
            }
            Item filterTypeEn = Factory.GetDatabase("web").GetItem(item.ID, Sitecore.Data.Managers.LanguageManager.GetLanguage("en", Factory.GetDatabase("web")));
            var field = filterTypeEn.Fields["Page Title"];

            return field.Value;
        }
        public string FieldName { get; set; }
        public string ReturnType { get; set; }
    }
}
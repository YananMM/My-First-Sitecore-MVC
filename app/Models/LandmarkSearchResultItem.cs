using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Serialization;

using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Links;

namespace Landmark.Models
{
    
    public class LandmarkSearchResultItem : SearchResultItem
    {
        [IndexField("Page Title")]
        public string PageTitle { get; set; }

        
    }
}
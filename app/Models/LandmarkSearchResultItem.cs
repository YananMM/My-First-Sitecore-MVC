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

        [IndexField("filtertype")]
        public String FilterType { get; set; }

        [IndexField("filterorder")]
        public String FilterOrder { get; set; }

        public string ArticleIntro { get; set; }

        public DateTime ArticleDate { get; set; }

        public string BrandImage { get; set; }

    }
}
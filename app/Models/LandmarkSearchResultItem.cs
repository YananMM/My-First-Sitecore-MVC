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
        [IndexField("pagetitle")]
        public string PageTitle { get; set; }

        [IndexField("pagecontent")]
        public string PageContent { get; set; }

        [IndexField("contenttitle")]
        public string ContentTitle { get; set; }

        [IndexField("contentdescription")]
        public string ContentDescription { get; set; }

        [IndexField("articleheadline")]
        public string ArticleTitle { get; set; }

        [IndexField("articlesubheadline")]
        public string ArticleSubtitle { get; set; }

        [IndexField("articleintro")]
        public string ArticleIntro { get; set; }

        [IndexField("filtertype")]
        public String FilterType { get; set; }

        [IndexField("filterorder")]
        public String FilterOrder { get; set; }

        [IndexField("searchtags")]
        public String Tags { get; set; }
    }
}
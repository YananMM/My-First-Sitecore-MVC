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

        [IndexField("Page Content")]
        public string PageContent { get; set; }

        [IndexField("Content Title")]
        public string ContentTitle { get; set; }

        [IndexField("Content Description")]
        public string ContentDescription { get; set; }

        [IndexField("Article Title")]
        public string ArticleTitle { get; set; }

        [IndexField("Article Subtitle")]
        public string ArticleSubtitle { get; set; }

        [IndexField("Article Intro")]
        public string ArticleIntro { get; set; }

        [IndexField("filtertype")]
        public String FilterType { get; set; }

        [IndexField("filterorder")]
        public String FilterOrder { get; set; }

        [IndexField("tags")]
        public String Tags { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Landmark.Web.Models.sitecore.templates.User_Defined.MyFirstSitecoreMVC;
using Glass.Mapper.Sc.Configuration.Attributes;
using System.Collections;

namespace Landmark.Web.Models
{
    [SitecoreType(AutoMap = true)]
    public class DocumentList
    {
        [SitecoreChildren]
        //[SitecoreQuery("./*[@@templatename='Document Item']", IsRelative = true)]
        public virtual IEnumerable<Document_Item> Documents { get; set; }
    }
}
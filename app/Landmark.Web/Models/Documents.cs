using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Mvc.Presentation;
using Sitecore.Data.Items;
using Glass.Mapper;
using Glass.Mapper.Sc;
using Landmark.Web.Models;
using Landmark.Web.Models.sitecore.templates.User_Defined.MyFirstSitecoreMVC;

namespace Landmark.Web.Models
{
    public class Documents:List<Document_Item>, IRenderingModel
    {
        public void Initialize(Rendering rendering)
        {
            foreach (Item child in rendering.Item.Children)
            {
                this.Add(child.GlassCast<Document_Item>());
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Landmark.Classes;
using Landmark.Helper;
using Landmark.Models;
using Lucene.Net.Highlight;
using Sitecore.Collections;
using Sitecore.Data.Items;

namespace Landmark.Service
{
    /// <summary>
    /// Summary description for GetFloorPlanJson
    /// </summary>
    public class GetFloorPlanJson : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ShoppingHelper helper = new ShoppingHelper();
            ChildList buidings = Sitecore.Context.Database.GetItem(ItemGuids.BuidingsFolder).Children;
            FloorPlan floorplans = new FloorPlan();
            floorplans.mapheight = "525";
            floorplans.mapwidth = "700";
            floorplans.categories = (from Item building in buidings
                from Item floor in building.Children
                select new Floor
                {
                    id = floor.ID.ToString(),
                    color = "#FFFFFF",
                    show = "false",
                    title = floor.Fields["Floor Title"].Value
                }).ToList();
            floorplans.levels = (from Item building in buidings
                                 from Item floor in building.Children
                                 select new Level
                                 {
                                     id = floor.ID.ToString(),
                                     title = floor.Fields["Floor Title"].Value,
                                     map = ImageFieldSrc("Floor Image",floor),
                                     minimap="",
                                     locations = (from location in helper.GetBrandsByFloor(floor)
                                                 select new Location
                                                 {
                                                     area = location.Fields["Area"].Value,
                                                     category = floor.ID.ToString(),
                                                     description = "",
                                                     id = location.ID.ToString(),
                                                     pin="hide",
                                                     x = location.Fields["LocationX"].Value,
                                                     y = location.Fields["LocationY"].Value
                                                 }).ToList()
                                 }).ToList();
            

            JavaScriptSerializer js = new JavaScriptSerializer();

            string strJSON = js.Serialize(floorplans);
            context.Response.Write(strJSON);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private String ImageFieldSrc(string fieldName, Item item)
        {
            string imageURL = string.Empty;
            Sitecore.Data.Fields.ImageField imageField = item.Fields[fieldName];
            if (imageField != null && imageField.MediaItem != null)
            {
                Sitecore.Data.Items.MediaItem image = new Sitecore.Data.Items.MediaItem(imageField.MediaItem);
                imageURL = Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(image));
            }
            return imageURL;
        }
    }
}
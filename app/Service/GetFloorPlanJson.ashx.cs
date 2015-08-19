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
            ChildList buildings = Sitecore.Context.Database.GetItem(ItemGuids.BuidingsFolder).Children;
            Item building = buildings.First();
            FloorPlan floorplans = new FloorPlan();
            floorplans.mapheight = "525";
            floorplans.mapwidth = "700";
            floorplans.categories = 
               ( from Item floor in building.Children
                select new Floor
                {
                    id = "floor-"+floor.ID.ToShortID(),
                    color = "#FFFFFF",
                    show = "false",
                    title = floor.Fields["Floor Title"].Value
                }).ToList();
            floorplans.levels = 
                                 (from Item floor in building.Children
                                 select new Level
                                 {
                                     id = "level-"+floor.ID.ToShortID(),
                                     title = floor.Fields["Floor Title"].Value,
                                     map = LandmarkHelper.FileFieldSrc("Floor Svg File",floor),
                                     minimap="",
                                     locations = (from location in helper.GetBrandsByFloor(floor)
                                                 select new Location
                                                 {
                                                     title = location.Fields["Brand Title"].Value,
                                                     area = location.Fields["Area"].Value,
                                                     category = "floor-" + floor.ID.ToShortID(),
                                                     description = "",
                                                     id = "location-" + location.ID.ToShortID(),
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
    }
}
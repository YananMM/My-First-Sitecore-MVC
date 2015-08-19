using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Landmark.Classes;
using Landmark.Helper;
using Landmark.Models;
using Sitecore.Collections;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Landmark.Service
{
    /// <summary>
    /// Summary description for Landmark
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Landmark : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetCategoryJson(string buildingID)
        {
            ShoppingHelper helper = new ShoppingHelper();
            Item building = Sitecore.Context.Database.GetItem(buildingID);
            FloorPlan floorplans = new FloorPlan();
            floorplans.mapheight = "525";
            floorplans.mapwidth = "700";
            floorplans.categories =
               (from Item floor in building.Children
                select new Floor
                {
                    id = "floor-" + floor.ID.ToShortID(),
                    color = "#FFFFFF",
                    show = "false",
                    title = floor.Fields["Floor Title"].Value
                }).ToList();
            floorplans.levels =
                                 (from Item floor in building.Children
                                  select new Level
                                  {
                                      id = "level-" + floor.ID.ToShortID(),
                                      title = floor.Fields["Floor Title"].Value,
                                      map = LandmarkHelper.FileFieldSrc("Floor Svg File", floor),
                                      minimap = "",
                                      locations = (from location in helper.GetBrandsByFloor(floor)
                                                   select new Location
                                                   {
                                                       title = location.Fields["Brand Title"].Value,
                                                       area = location.Fields["Area"].Value,
                                                       category = "floor-" + floor.ID.ToShortID(),
                                                       description = "",
                                                       id = "location-" + location.ID.ToShortID(),
                                                       pin = "hide",
                                                       x = location.Fields["LocationX"].Value,
                                                       y = location.Fields["LocationY"].Value
                                                   }).ToList()
                                  }).ToList();


            JavaScriptSerializer js = new JavaScriptSerializer();

            string strJSON = js.Serialize(floorplans);

            return strJSON;
        }
    }
}

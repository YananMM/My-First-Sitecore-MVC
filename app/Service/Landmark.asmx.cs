using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.WebPages;
using Landmark.Classes;
using Landmark.Helper;
using Landmark.Models;
using Sitecore.Collections;
using Sitecore.Data.Items;
using Sitecore.IO;
using Sitecore.Links;

namespace Landmark.Service
{
    /// <summary>
    /// Summary description for Landmark
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Landmark : System.Web.Services.WebService
    {
        private ShoppingHelper _shopHelper = new ShoppingHelper();
        private ArtTourHelper _artsHelper = new ArtTourHelper();

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
        public void GetFloorPlanJson(string buildingID)
        {
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
                                  where _shopHelper.GetBrandsByFloor(floor).Count > 0
                                  select new Level
                                  {
                                      id = "level-" + floor.ID.ToShortID(),
                                      title = floor.Fields["Floor Title"].Value,
                                      map = LandmarkHelper.FileFieldSrc("Floor Svg File", floor),
                                      minimap = "",
                                      locations = (from location in _shopHelper.GetBrandsByFloor(floor)
                                                   select new Location
                                                   {
                                                       title = location.Fields["Brand Title"].Value,
                                                       area = location.Fields["Area"].Value,
                                                       category = "floor-" + floor.ID.ToShortID(),
                                                       description = "",
                                                       id = location.Fields["Svg Id"].Value,
                                                       pin = "hide",
                                                       x = location.Fields["LocationX"].Value,
                                                       y = location.Fields["LocationY"].Value,
                                                       workdayhours = location.Fields["Opening Hours"].Value,
                                                       wherelocation = location.Fields["Area"].Value + "," + floor.Fields["Floor Title"].Value + "," + building.Fields["Building Title"].Value,
                                                       address = building.Fields["Building Address"].Value,
                                                       href = LinkManager.GetItemUrl(location)
                                                   }).ToList()
                                  }).ToList();

            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJSON = js.Serialize(floorplans);
            Context.Response.Write(strJSON);
            Context.Response.ContentType = "application/json";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
        public void GetLocationJson()
        {
            List<Item> buildings = LandmarkHelper.GetBuildings();
            Item byLocationPage = Sitecore.Context.Database.GetItem(ItemGuids.ByLocationPage);
            FloorPlan floorplans = new FloorPlan();
            floorplans.mapheight = "300";
            floorplans.mapwidth = "450";
            Level level = new Level();
            level.id = "floorplan";
            level.title = "Building Floorplan";
            level.map = IfBrowserIsIE8()
                ? LandmarkHelper.ImageFieldSrc("Buildings Map Image", byLocationPage)
                : LandmarkHelper.FileFieldSrc("Buildings Map File", byLocationPage);
            level.minimap = "";
            level.locations = (from location in buildings
                select new Location
                {
                    id = location.Fields["Building Svg Id"].Value,
                    pin = "hide",
                    x = location.Fields["LocationX"].Value,
                    y = location.Fields["LocationY"].Value
                }).ToList();
            floorplans.levels = new List<Level>();
            floorplans.levels.Add(level);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJSON = js.Serialize(floorplans);
            Context.Response.Write(strJSON);
            Context.Response.ContentType = "application/json";
        }

        private bool IfBrowserIsIE8()
        {
            bool result = false;
            if(Context.Request.Browser.Type.ToUpper().Contains("InternetExplorer"))
            {
                if (Context.Request.Browser.MajorVersion == 8)
                {
                    result = true;
                }
            }
            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
        public void GetArtsJson(string buildingID)
        {
            Item building = Sitecore.Context.Database.GetItem(buildingID);
            var list = _artsHelper.GetArtPieceJsonByBuilding(buildingID);
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJSON = js.Serialize(list);
            Context.Response.Write(strJSON);
            Context.Response.ContentType = "application/json";
        }

    }
}

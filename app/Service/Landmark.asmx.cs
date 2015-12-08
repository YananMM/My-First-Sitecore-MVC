using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

using Sitecore.Data.Items;
using Sitecore.Links;

using Landmark.Classes;
using Landmark.Helper;
using Landmark.Models;

namespace Landmark.Service
{
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
                where floor.Template.Name.Equals("Floor Object")
                select new Floor
                {
                    id = "floor-" + floor.ID.ToShortID(),
                    color = "#FFFFFF",
                    show = "false",
                    title = floor.Fields["Floor Title"].Value
                }).ToList();
            floorplans.levels =
                (from Item floor in building.Children
                where floor.Template.Name.Equals("Floor Object")
                select new Level
                {
                    id = "level-" + floor.ID.ToShortID(),
                    title = floor.Fields["Floor Title"].Value,
                    map = IfBrowserIsIE8()
                        ? LandmarkHelper.ImageFieldSrc("Floor Image", floor)
                        : LandmarkHelper.FileFieldSrc("Floor Svg File", floor),
                    minimap = "",
                    locations =
                        (from Item shop in floor.Children
                         where (shop.Template.Name.Equals("Shop Location Object") && getShopByName(shop.Name) != null)
                        select new Location
                        {
                            title = getShopByName(shop.Name).Fields["Brand Title"].Value,
                            area = getShopByName(shop.Name).Fields["Address"].Value,
                            category = "floor-" + floor.ID.ToShortID(),
                            description = "",
                            id = shop.Fields["Svg Id"].Value,
                            pin = "hide",
                            x = shop.Fields["LocationX"].Value,
                            y = shop.Fields["LocationY"].Value,
                            workdayhours = getShopByName(shop.Name).Fields["Opening Hours"].Value,
                            wherelocation = getShopByName(shop.Name).Fields["Address"].Value + "," + floor.Fields["Floor Title"].Value + "," + building.Fields["Building Title"].Value,
                            address = building.Fields["Building Address"].Value,
                            href = LandmarkHelper.TranslateUrl(LinkManager.GetItemUrl(getShopByName(shop.Name)))
                        }).ToList()
                }).ToList();

            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJSON = js.Serialize(floorplans);
            Context.Response.Write(strJSON);
            Context.Response.ContentType = "application/json";
        }

        private Item getShopByName(string name)
        {
            Item resultItem = null;
            string pathShopping = "/sitecore/content/home/landmark/shopping/" + name;
            string pathDining = "/sitecore/content/home/landmark/dining/" + name;
            resultItem = Sitecore.Context.Database.GetItem(pathShopping);
            if (resultItem != null) return resultItem;
            resultItem = Sitecore.Context.Database.GetItem(pathDining);
            if (resultItem != null) return resultItem;
            return null;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
        public void GetFloorPlanJsonGruden(string buildingID)
        {
            Item building = Sitecore.Context.Database.GetItem(buildingID);
            FloorPlan floorplans = new FloorPlan();
            floorplans.mapheight = "525";
            floorplans.mapwidth = "700";
            floorplans.categories =
               (from Item floor in building.Children
                where _shopHelper.GetBrandsByFloor(floor).Count > 0
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
                                      map = IfBrowserIsIE8()
                ? LandmarkHelper.ImageFieldSrc("Floor Image", floor)
                : LandmarkHelper.FileFieldSrc("Floor Svg File", floor),
                                      minimap = "",
                                      locations = (from location in _shopHelper.GetBrandsByFloor(floor)
                                                   select new Location
                                                   {
                                                       title = location.Fields["Brand Title"].Value,
                                                       area = location.Fields["Address"].Value,
                                                       category = "floor-" + floor.ID.ToShortID(),
                                                       description = "",
                                                       id = location.Fields["Svg Id"].Value,
                                                       pin = "hide",
                                                       x = location.Fields["LocationX"].Value,
                                                       y = location.Fields["LocationY"].Value,
                                                       workdayhours = location.Fields["Opening Hours"].Value,
                                                       wherelocation = location.Fields["Address"].Value + "," + floor.Fields["Floor Title"].Value + "," + building.Fields["Building Title"].Value,
                                                       address = building.Fields["Building Address"].Value,
                                                       href = LandmarkHelper.TranslateUrl(LinkManager.GetItemUrl(location))
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
            List<Item> buildings = LandmarkHelper.GetAllBuildings();
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
                                   title = IfBrowserIsIE8() ? location.Fields["Building Title"].Value : "",
                                   pin = IfBrowserIsIE8() ? "green" : "hide",
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
            if (Context.Request.Browser.Type.ToLower().Contains("internetexplorer"))
            {
                if (Context.Request.Browser.MajorVersion < 9)
                {
                    result = true;
                }
            }
            if (Context.Request.Browser.Type.ToLower().Contains("ie8"))
            {
                result = true;
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Landmark.Classes;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Shell.Applications.ContentEditor;

namespace Landmark.Service
{
    /// <summary>
    /// Summary description for LandmarkService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class LandmarkService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetShoppingCategories()
        {
            Database webDb = Factory.GetDatabase("web");
            Item shoppingCategory = Sitecore.Context.Database.GetItem(ItemGuids.ShoppingCategory);
            Item shopping = Sitecore.Context.Database.GetItem(ItemGuids.ShoppingItem);
            var queryCategory = string.Format("fast:{0}//*[{1}]", shoppingCategory.Paths.FullPath, "@@TemplateId='" + ItemGuids.CategoryObjectTemplate + "'");
            List<TextValue> firstCategory = (from category in webDb.SelectItems(queryCategory).ToList()
                                             from Item item in shopping.Children
                                             where item.DisplayName == category.DisplayName
                                             select new TextValue(category["Category Name"], item.ID.ToString())).ToList();

            List<Categories> categories = new List<Categories>();

            foreach (var item in firstCategory)
            {

            }


            JavaScriptSerializer js = new JavaScriptSerializer();

            //List<TextValue> subCategories = (from TextValue category in firstCategory
            //                                 from Item subCategory in category.Children
            //                                 select new Categories(category,new List<>()).ToList();


            string strJSON = js.Serialize(firstCategory);



            return strJSON;
        }

    }

    public class TextValue
    {
        public string text;
        public string value;
        public TextValue(string _text, string _value)
        {
            text = _text;
            value = _value;
        }
    }

    public class Categories
    {
        public TextValue Category { get; set; }

        public List<TextValue> SubCategory { get; set; }
    }
}

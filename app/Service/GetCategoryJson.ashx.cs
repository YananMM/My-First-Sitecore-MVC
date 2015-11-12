using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Landmark.Classes;
using Landmark.Helper;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Landmark.Models;

namespace Landmark.Service
{
    /// <summary>
    /// Summary description for GetJson
    /// </summary>
    public class GetJson : IHttpHandler 
    {
        private ShoppingHelper shoppingHelper = new ShoppingHelper();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var id = context.Request["id"];
            List<TextValue> categoryTextValue = shoppingHelper.GetFirstCategory(id);

            JavaScriptSerializer js = new JavaScriptSerializer();

            string textValueJson = js.Serialize(categoryTextValue);
            context.Response.Write(textValueJson);
        }

        public bool IsReusable
        {
            get { return false; }
        }


    }
}
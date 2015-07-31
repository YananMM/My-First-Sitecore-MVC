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

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            List<TextValue> firstCategory = LandmarkHelper.GetFirstCategory();

            JavaScriptSerializer js = new JavaScriptSerializer();

            string strJSON = js.Serialize(firstCategory);
            context.Response.Write(strJSON);
        }

        public bool IsReusable
        {
            get { return false; }
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Landmark.Models;

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
            List<TextValue> firstCategory = shoppingHelper.GetFirstCategory();

            JavaScriptSerializer js = new JavaScriptSerializer();

            string strJSON = js.Serialize(firstCategory);
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
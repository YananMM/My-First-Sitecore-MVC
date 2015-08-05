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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Landmark.Service
{
    /// <summary>
    /// Summary description for CheckCaptcha
    /// </summary>
    public class CheckCaptcha : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //var code = Session["ValidateCode"].ToString();
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
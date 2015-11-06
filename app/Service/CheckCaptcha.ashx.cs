using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Landmark.Service
{
    /// <summary>
    /// Summary description for CheckCaptcha
    /// </summary>
    public class CheckCaptcha : IHttpHandler, IRequiresSessionState 
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var match = context.Request.Params["captcha"] == context.Session["ValidateCode"].ToString();
            context.Response.Write(match ? "true" : "false"); 
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
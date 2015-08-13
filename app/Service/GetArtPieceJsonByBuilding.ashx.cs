using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Landmark.Helper;

namespace Landmark.Service
{
    /// <summary>
    /// Summary description for GetArtPieceJsonByBuilding
    /// </summary>
    public class GetArtPieceJsonByBuilding : IHttpHandler
    {
        private ArtTourHelper _helper;

        public GetArtPieceJsonByBuilding()
        {
            _helper = new ArtTourHelper();
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var list = _helper.GetArtPieceJsonByBuilding();

            string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(list);
            context.Response.Write(json);
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
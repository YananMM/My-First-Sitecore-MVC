using Landmark.Classes;
using Landmark.Helper;
using Landmark.Models;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Landmark.Service
{
    /// <summary>
    /// Summary description for GetArtPieceJsonByArtists
    /// </summary>
    public class GetArtPieceJsonByArtists : IHttpHandler
    {
        private ArtTourHelper _helper;

        public GetArtPieceJsonByArtists()
        {
            _helper = new ArtTourHelper();
        }
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";

                List<ArtPieceJson> models = _helper.GetArtPieceJsonByArtists();
                
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

                string json = javaScriptSerializer.Serialize(models);
                context.Response.Write(json);
            }
            catch (Exception e)
            {
                context.Response.Write(e.Message);
            }
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
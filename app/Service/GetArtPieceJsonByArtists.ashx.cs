// ***********************************************************************
// Assembly         : Landmark
// Author           : Jiang Lili
// Created          : 08-13-2015
//
// Last Modified By : Jiang Lili
// Last Modified On : 08-13-2015
// ***********************************************************************
// <copyright file="GetArtPieceJsonByArtists.ashx.cs" company="Gruden">
//     Copyright (c) Gruden. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Landmark.Classes;
using Landmark.Helper;
using Landmark.Models;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

// <summary>
// The Service namespace.
// </summary>
namespace Landmark.Service
{
    /// <summary>
    /// Summary description for GetArtPieceJsonByArtists
    /// </summary>
    public class GetArtPieceJsonByArtists : IHttpHandler
    {
        /// <summary>
        /// The _helper
        /// </summary>
        private ArtTourHelper _helper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetArtPieceJsonByArtists"/> class.
        /// </summary>
        public GetArtPieceJsonByArtists()
        {
            _helper = new ArtTourHelper();
        }
        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler" /> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
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

        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.
        /// </summary>
        /// <value><c>true</c> if this instance is reusable; otherwise, <c>false</c>.</value>
        /// <returns>true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable; otherwise, false.</returns>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
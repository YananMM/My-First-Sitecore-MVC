using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Landmark.Classes;
using Landmark.Helper;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.SecurityModel;

namespace Landmark.Internal_Handlers
{
    /// <summary>
    /// Summary description for Rendering
    /// </summary>
    public class Rendering : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var languages = Sitecore.Context.Database.GetLanguages();

            Language enLanguage = Sitecore.Globalization.Language.Parse("en");
            Item enHomeItem = Factory.GetDatabase("master").GetItem("{497220CE-7763-4A17-9D3F-4A6DBF1B8CDB}",enLanguage);
            foreach (var lan in languages)
            {
                
                if (lan.Name != enLanguage.Name)
                {
                    Item lanItem = Factory.GetDatabase("master").GetItem("{497220CE-7763-4A17-9D3F-4A6DBF1B8CDB}",lan);
                    using (new SecurityDisabler())
                    {
                        using (new EditContext(lanItem))
                        {
                            lanItem.Fields["__Final Renderings"].Value = enHomeItem.Fields["__Final Renderings"].Value;
                        }
                    }
                }
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
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
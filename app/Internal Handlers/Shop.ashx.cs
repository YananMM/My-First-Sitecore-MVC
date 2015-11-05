using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Landmark.Classes;
using Sitecore.Configuration;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;

namespace Landmark.Internal_Handlers
{
    /// <summary>
    /// Summary description for Shop
    /// </summary>
    public class Shop : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Item shopping = Factory.GetDatabase("master").GetItem(ItemGuids.DiningItem);
            var query = string.Format("fast:{0}//*[{1}]", shopping.Paths.FullPath, "@@TemplateId='" + ItemGuids.T14ShopDetailsTemplate + "'");
            List<Item> brandsItems = Factory.GetDatabase("master").SelectItems(query).ToList();
            foreach (Item brand in brandsItems)
            {
                MultilistField floor = brand.Fields["Floor"];
                if (floor != null && floor.TargetIDs.Any())
                {
                    using (new SecurityDisabler())
                    {
                        using (new EditContext(brand))
                        {
                            brand.Fields["Building"].Value =
                        Factory.GetDatabase("master").GetItem(floor.TargetIDs.FirstOrDefault()).ParentID.ToString();
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
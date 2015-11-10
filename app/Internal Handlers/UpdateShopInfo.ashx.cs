using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Landmark.Classes;
using Landmark.Helper;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;

namespace Landmark.Internal_Handlers
{
    /// <summary>
    /// Summary description for UpdateShopInfo
    /// </summary>
    public class UpdateShopInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            List<Item> allBrands = LandmarkHelper.GetItemsByRootAndTemplate(SitecoreItems.LandmarkHomeItem.ID.ToString(), ItemGuids.T14ShopDetailsTemplate);

            foreach (Item brand in allBrands)
            {
                List<Item> fs = brand.Children.Where(i => i.DisplayName == "Shop Main Page").ToList();
                if (fs != null && fs.Count>0)
                {
                    Item f = fs.First();
                    using (new SecurityDisabler())
                    {
                        using (new EditContext(f))
                        {
                            f.Name = "Slide Object";
                        }
                    }
                }
                break;
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
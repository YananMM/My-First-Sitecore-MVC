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
            Item shopping = Factory.GetDatabase("master").GetItem("{5C03DD0D-4881-4A9B-A58D-5FFA101B3533}");
            var query = string.Format("fast:{0}//*[{1}]", shopping.Paths.FullPath, "@@TemplateId='" + "{ADE0F061-FF28-4CDB-B6C3-14021D75355A}" + "'");
            List<Item> brandsItems = Factory.GetDatabase("master").SelectItems(query).ToList();
            foreach (Item brand in brandsItems)
            {
                if (brand.DisplayName.StartsWith("Textured"))
                {
                    using (new SecurityDisabler())
                    {
                        var itemName = brand.DisplayName + "new";
                        var shopItem =
                                   Factory.GetDatabase("master").CreateItemPath(
                                       brand.Paths.Path+itemName,Factory.GetDatabase("master").GetItem("{ADE0F061-FF28-4CDB-B6C3-14021D75355A}"));

                        foreach (var language in brand.Languages)
                        {
                            var version = Factory.GetDatabase("master").GetItem(shopItem.ID, language);
                            var sourcev = Factory.GetDatabase("master").GetItem(brand.ID, language);

                            if (version.Versions.Count == 0)
                            {
                                version = version.Versions.AddVersion();
                            }

                            using (new EditContext(version))
                            {
                                version.Editing.BeginEdit();
                                version.Fields["Art Title"].SetValue(sourcev.Fields["Art Title"].Value, true);
                                version.Fields["Page Title"].SetValue(sourcev.Fields["Art Title"].Value, true);
                                version.Fields["Art Size"].SetValue(sourcev.Fields["Art Size"].Value, true);
                                version.Fields["Commentary Alt"].SetValue(sourcev.Fields["Commentary Alt"].Value, true);


                                version.Fields["Art Description"].SetValue(sourcev.Fields["Art Description"].Value, true);

                                version.Fields["Floor and Building"].SetValue(sourcev.Fields["Floor and Building"].Value, true);
                                version.Fields["LocationX"].SetValue(sourcev.Fields["LocationX"].Value, true);
                                version.Fields["LocationY"].SetValue(sourcev.Fields["LocationY"].Value, true);
                                version.Fields["Artist"].SetValue(sourcev.Fields["Artist"].Value, true);
                            }
                        }
                    }
                    string title = brand.Fields["Art Title"].Value;
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
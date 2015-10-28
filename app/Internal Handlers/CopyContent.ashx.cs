using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Landmark.Helper;
using Sitecore.Configuration;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc.Extensions;
using Sitecore.SecurityModel;

namespace Landmark.Internal_Handlers
{
    /// <summary>
    /// Summary description for CopyLanguage
    /// </summary>
    public class CopyLanguage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            List<Item> items = LandmarkHelper.GetItemByTemplate(Factory.GetDatabase("master").GetItem("{923DBE58-71DE-4B27-B0BA-C5DEF3D301B2}"),
                "{A46D7A20-B29F-4C4F-8922-0B70555CD6BC}").Where(item => !((CheckboxField)item.Fields["Has Detailed"]).Checked).ToList();
            foreach (Item i in items)
            {
                Item enLanguageItem = Factory.GetDatabase("master").GetItem(i.ID, Sitecore.Globalization.Language.Parse("en"));
                //Item scLanguageItem = Factory.GetDatabase("master").GetItem(i.ID, Sitecore.Globalization.Language.Parse("zh-CN"));
                //Item tcLanguageItem = Factory.GetDatabase("master").GetItem(i.ID, Sitecore.Globalization.Language.Parse("zh-HK"));
                using (new SecurityDisabler())
                {
                    /*tcLanguageItem.Editing.BeginEdit();
                    tcLanguageItem.Fields["Article Headline"].Value = scLanguageItem.Fields["Article Sub Headline"].Value;
                    tcLanguageItem.Editing.AcceptChanges();


                    scLanguageItem.Editing.BeginEdit();
                    scLanguageItem.Fields["Article Headline"].Value = scLanguageItem.Fields["Article Sub Headline"].Value;
                    scLanguageItem.Fields["Article Sub Headline"].Value = "";
                    scLanguageItem.Editing.AcceptChanges();*/

                    enLanguageItem.Editing.BeginEdit();
                    enLanguageItem.Fields["Article Headline"].Value = enLanguageItem.Fields["Article Sub Headline"].Value;
                    enLanguageItem.Editing.AcceptChanges();
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
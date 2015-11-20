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

            List<Item> items = LandmarkHelper.GetItemByTemplate(Factory.GetDatabase("master").GetItem("{F0A68E40-FB4F-4B85-969D-A6ADEE24DE6B}"),
                "{4CDF5CD9-40F0-4F0B-A095-7773CE6CF582}").Where(item => !string.IsNullOrEmpty(item.Fields["Article Content"].Value)).ToList();
            foreach (Item i in items)
            {
                Item enLanguageItem = Factory.GetDatabase("master").GetItem(i.ID, Sitecore.Globalization.Language.Parse("en"));
                Item scLanguageItem = Factory.GetDatabase("master").GetItem(i.ID, Sitecore.Globalization.Language.Parse("zh-CN"));
                Item tcLanguageItem = Factory.GetDatabase("master").GetItem(i.ID, Sitecore.Globalization.Language.Parse("zh-HK"));
                using (new SecurityDisabler())
                {
                    /*tcLanguageItem.Editing.BeginEdit();
                    tcLanguageItem.Fields["Article Headline"].Value = scLanguageItem.Fields["Image Title"].Value;
                    tcLanguageItem.Editing.AcceptChanges();


                    scLanguageItem.Editing.BeginEdit();
                    scLanguageItem.Fields["Article Headline"].Value = scLanguageItem.Fields["Image Title"].Value;
                    scLanguageItem.Fields["Image Title"].Value = "";
                    scLanguageItem.Editing.AcceptChanges();*/

                    enLanguageItem.Editing.BeginEdit();
                    enLanguageItem.Fields["Article Content2"].Value = enLanguageItem.Fields["Article Content"].Value;
                    enLanguageItem.Editing.AcceptChanges();

                    tcLanguageItem.Editing.BeginEdit();
                    tcLanguageItem.Fields["Article Content2"].Value = tcLanguageItem.Fields["Article Content"].Value;
                    tcLanguageItem.Editing.AcceptChanges();

                    scLanguageItem.Editing.BeginEdit();
                    scLanguageItem.Fields["Article Content2"].Value = scLanguageItem.Fields["Article Content"].Value;
                    scLanguageItem.Editing.AcceptChanges();
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
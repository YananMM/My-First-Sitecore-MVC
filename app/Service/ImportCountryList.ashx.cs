using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Publishing;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Landmark.Classes;
using Sitecore.WordOCX.Extensions;

namespace Landmark.Service
{
    /// <summary>
    /// Summary description for ImportCountryList
    /// </summary>
    public class ImportCountryList : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                using (new SecurityDisabler())
                {
                    Database master = Sitecore.Configuration.Factory.GetDatabase("master");
                    Item countryFolder = master.GetItem("/sitecore/content/Home/Landmark/Email Signup/Countries");
                    //Item countryFolder = Sitecore.Context.Database.GetItem("{C759C4E7-A105-4B63-8266-00B67445987A}");
                    var TextObjectTempalte = (TemplateItem)Sitecore.Context.Database.GetItem(ItemGuids.TextObject);
                    var countryList = Resources.Landmark.CountryList.Split('\r').ToArray();
                    foreach (var line in countryList)
                    {
                        var textValue = line.Trim().Split('-').ToArray();
                        string countryValue = textValue[0].Trim();
                        string countryText = textValue[1].Trim();
                        string countryName = textValue[2].Trim();
                        try
                        {
                            Item countryItem = countryFolder.Add(countryName, TextObjectTempalte);

                            if (countryItem != null)
                            {
                                countryItem.Editing.BeginEdit();
                                countryItem.Fields["Value"].Value = countryValue;
                                countryItem.Fields["Text"].Value = countryText;
                                countryItem.Editing.EndEdit();
                                context.Response.WriteLine("Add Success");
                            }
                            if (countryItem == null)
                            {
                                context.Response.WriteLine("Error");
                            }
                            
                        }
                        catch (Exception e)
                        {
                            context.Response.Write(e.Message);
                        }

                    }
                }
            }
            catch (Exception e)
            {
                context.Response.WriteLine(e.Message);
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
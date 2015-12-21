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
                    var TextObjectTempalte = (TemplateItem)Sitecore.Context.Database.GetItem(ItemGuids.TextObject);
                    var countryList = Resources.Landmark.CountryList.Split('\r').ToArray();
                    foreach (var line in countryList)
                    {
                        var textValue = line.Trim().Split('-').ToArray();
                        string countryValue = textValue[0].Trim();
                        string enText = textValue[1].Trim();
                        string tcText = textValue[2].Trim();
                        string scText = textValue[3].Trim();

                        string countryName = textValue[2].Trim();
                        try
                        {
                            var countryItem = countryFolder.Children.SingleOrDefault(p => p.Fields["Value"].ToString() == countryValue);
                            if (countryItem == null)
                            {
                                countryItem = countryFolder.Add(countryValue, TextObjectTempalte);
                                context.Response.WriteLine("Add a new item");
                            }
                            else
                            {
                                countryItem.Editing.BeginEdit();
                                countryItem.Fields["Value"].Value = countryValue;
                                foreach (var language in countryItem.Languages)
                                {
                                    if (language.Name == "en")
                                    {
                                        countryItem.Fields["Text"].Value = enText;
                                        context.Response.WriteLine(enText);
                                    }
                                    else if (language.Name == "zh-CN")
                                    {
                                        countryItem.Fields["Text"].Value = scText;
                                    }
                                    else if (language.Name == "zh-HK")
                                    {
                                        countryItem.Fields["Text"].Value = tcText;
                                    }
                                }
                                countryItem.Editing.EndEdit();
                                context.Response.WriteLine("Edit item");
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
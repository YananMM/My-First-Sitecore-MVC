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

                    //if (countryFolder.Children.Count != 0)
                    //{
                    //    countryFolder.DeleteChildren();
                    //}

                    var TextObjectTempalte = (TemplateItem)Sitecore.Context.Database.GetItem(ItemGuids.TextObject);
                    var countryList = Resources.Landmark.CountryList.Split('\r').ToArray();
                    foreach (var line in countryList)
                    {
                        var textValue = line.Trim().Split('-').ToArray();
                        string countryValue = textValue[0].Trim();
                        string enText = textValue[1].Trim();
                        string tcText = textValue[2].Trim();
                        string scText = textValue[3].Trim();

                        //string itemName = Regex.Replace(enText, "[^0-9A-Za-z]", "");
                        string itemName = textValue[4].Trim();

                        try
                        {
                            var countryItem = countryFolder.Children.SingleOrDefault(p => p.DisplayName == itemName);
                            if (countryItem == null)
                            {
                                countryItem = countryFolder.Add(itemName, TextObjectTempalte);
                                countryItem.Editing.BeginEdit();
                                countryItem.Fields["Value"].Value = countryValue;
                                foreach (var language in countryItem.Languages)
                                {
                                    Item langItem = countryItem.Database.GetItem(countryItem.ID, language);
                                    if (langItem.Language.Name == "en")
                                    {
                                        langItem = langItem.Versions.AddVersion();
                                        langItem.Editing.BeginEdit();
                                        langItem.Fields["Text"].Value = enText;
                                        langItem.Editing.EndEdit();
                                    }
                                    else if (langItem.Language.Name == "zh-CN")
                                    {
                                        langItem = langItem.Versions.AddVersion();
                                        langItem.Editing.BeginEdit();
                                        langItem.Fields["Text"].Value = scText;
                                        langItem.Editing.EndEdit();
                                    }
                                    else if (langItem.Language.Name == "zh-HK")
                                    {
                                        langItem = langItem.Versions.AddVersion();
                                        langItem.Editing.BeginEdit();
                                        langItem.Fields["Text"].Value = tcText;
                                        langItem.Editing.EndEdit();
                                    }
                                }
                                countryItem.Editing.EndEdit();
                                context.Response.WriteLine("Add " + enText);
                            }
                            else
                            {
                                countryItem.Editing.BeginEdit();
                                countryItem.Fields["Value"].Value = countryValue;
                                foreach (var language in countryItem.Languages)
                                {
                                    Item langItem = countryItem.Database.GetItem(countryItem.ID, language);
                                    if (langItem.Language.Name == "en")
                                    {
                                        langItem = langItem.Versions.AddVersion();
                                        langItem.Editing.BeginEdit();
                                        langItem.Fields["Text"].Value = enText;
                                        langItem.Editing.EndEdit();
                                    }
                                    else if (langItem.Language.Name == "zh-CN")
                                    {
                                        langItem = langItem.Versions.AddVersion();
                                        langItem.Editing.BeginEdit();
                                        langItem.Fields["Text"].Value = scText;
                                        langItem.Editing.EndEdit();
                                    }
                                    else if (langItem.Language.Name == "zh-HK")
                                    {
                                        langItem = langItem.Versions.AddVersion();
                                        langItem.Editing.BeginEdit();
                                        langItem.Fields["Text"].Value = tcText;
                                        langItem.Editing.EndEdit();
                                    }
                                }
                                countryItem.Editing.EndEdit();
                                context.Response.WriteLine("Edit " + itemName);
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
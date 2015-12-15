using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Landmark.Classes;
using Landmark.Helper;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Data.Fields;
using Sitecore.Extensions;
using Sitecore.Globalization;
using Sitecore.StringExtensions;
using System.Web.Caching;
using System.Web.Mvc;
using Sitecore.Configuration;
using Sitecore.Web;

namespace Landmark.Pipelines.HttpRequest
{
    public class NotFoundProcessor: Sitecore.Pipelines.HttpRequest.HttpRequestProcessor
    {
        public override void Process(Sitecore.Pipelines.HttpRequest.HttpRequestArgs args)
        {

            if ((Sitecore.Context.Item != null && !(new LayoutField(Sitecore.Context.Item).Value).IsNullOrEmpty()) || Sitecore.Context.Site == null || Sitecore.Context.Database == null
                || Sitecore.Context.Database != Sitecore.Configuration.Factory.GetDatabase("web") || Sitecore.Context.IsAdministrator
                || args.Url.FilePath.ToLower().StartsWith("/service/") || args.Url.FilePath.ToLower().StartsWith("/sitecore") ||
                args.Url.FilePath.ToLower().StartsWith("/applications") || args.Url.FilePath.ToLower().StartsWith("/internal") 
                )
            {
                return;
            }

            if (Sitecore.Context.Item != null)
            {
                if (Sitecore.Context.Item.Children.Any())
                {
                    Item byBrands = Sitecore.Context.Item.Children.Where(i => i.DisplayName == "By Brands").ToList().FirstOrDefault();
                    if (byBrands != null)
                    {
                        LandmarkHelper.RedirectPermanent(LandmarkHelper.TranslateUrl(LinkManager.GetItemUrl(byBrands)),301);
                    }
                }
            }
            
            //Item findItem = Sitecore.Context.Database.GetItem(SitecoreItemGuids.FindPropertiesOffersItem.ToString());
            //string path = args.StartPath + "/" + findItem.Name + args.LocalPath.Replace("-", " ");
            //Item context = Sitecore.Context.Database.GetItem(path);
            //Sitecore.Data.Items.Item item = Sitecore.Context.Database.Items[path];
            //if (context != null & Sitecore.Context.Database == Sitecore.Configuration.Factory.GetDatabase("web"))
            //{
            //    Sitecore.Context.Item = context;
            //}

            // TODO: redirect to page not found page
            Item pageNotFoundItem = Sitecore.Context.Database.GetItem(ItemGuids.PageNotFoundItem.ToString());
            UrlOptions options = new UrlOptions();
            options.AlwaysIncludeServerUrl = true;
            options.LanguageEmbedding = LanguageEmbedding.Always;
            options.LowercaseUrls = true;
            string url = LandmarkHelper.TranslateUrl(LinkManager.GetItemUrl(pageNotFoundItem, options).Replace(" ","-"));
            string content = Sitecore.Web.WebUtil.ExecuteWebPage(url);
            // Send the NotFound page content to the client with a 404 status code
            HttpContext.Current.Response.TrySkipIisCustomErrors = true;
            HttpContext.Current.Response.StatusCode = 404;
            HttpContext.Current.Response.Write(content);
            HttpContext.Current.Response.End();

        }
    }
}
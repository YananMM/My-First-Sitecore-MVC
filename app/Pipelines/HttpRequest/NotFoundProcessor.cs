using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Landmark.Classes;
using Landmark.Helper;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Data.Fields;
using Sitecore.Extensions;
using Sitecore.StringExtensions;

namespace Landmark.Pipelines.HttpRequest
{
    public class NotFoundProcessor: Sitecore.Pipelines.HttpRequest.HttpRequestProcessor
    {
        public override void Process(Sitecore.Pipelines.HttpRequest.HttpRequestArgs args)
        {
            if ((Sitecore.Context.Item != null) || Sitecore.Context.Site == null || Sitecore.Context.Database == null
                || Sitecore.Context.Database != Sitecore.Configuration.Factory.GetDatabase("web") || Sitecore.Context.IsAdministrator
                || args.Url.FilePath.ToLower().StartsWith("/service/") || args.Url.FilePath.ToLower().StartsWith("/sitecore") ||
                args.Url.FilePath.ToLower().StartsWith("/applications") || args.Url.FilePath.ToLower().StartsWith("/internal") 
                )
            {
                return;
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
            HttpContext.Current.Response.StatusCode = 404;
            HttpContext.Current.Response.Redirect(url);

        }
    }
}
﻿using System;
using Sitecore.Data.Items;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using Landmark.Classes;
using Landmark.Models;
using Lucene.Net.Highlight;
using Sitecore.Collections;
using Sitecore.Configuration;
using Sitecore.ContentSearch.Utilities;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Managers;
using Sitecore.Globalization;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using Sitecore.Shell.Applications.ContentManager.ReturnFieldEditorValues;
using Sitecore.StringExtensions;
using Sitecore.Web.UI;
using Attachment = Sitecore.Shell.Applications.ContentEditor.Attachment;
using DateTime = System.DateTime;

namespace Landmark.Helper
{
    public static class LandmarkHelper
    {
        private static Database _webDb = Factory.GetDatabase("web");

        public static int NumberInOnePage = Factory.GetDatabase("web").GetItem(ItemGuids.LandmarkConfigItem).Fields["Page"] == null
                ? 10
                : Int32.Parse(Factory.GetDatabase("web").GetItem(ItemGuids.LandmarkConfigItem).Fields["Page"].Value);

        public static bool HasValidVersion()
        {
            bool hasValidVersion = false;
            LanguageCollection collection = ItemManager.GetContentLanguages(Sitecore.Context.Item);
            foreach (var lang in collection)
            {
                var itm = Sitecore.Context.Database.GetItem(Sitecore.Context.Item.ID, lang);
                if (itm.Versions.Count > 0)
                {
                    hasValidVersion = true;
                }
            }
            return hasValidVersion;
        }

        public static bool HasValidVersion(string language)
        {
            Item item =
                Sitecore.Context.Database.Items[
                    Sitecore.Context.Item.ID, Sitecore.Globalization.Language.Parse(language)];
            Item validVersion = null;
            if (item != null)
            {
                validVersion = item.Publishing.GetValidVersion(DateTime.Now, true);
            }

            return validVersion != null && validVersion.Versions.Count > 0;
        }

        public static string GetUrlByLanguage(Item item, string language)
        {
            var options = Sitecore.Links.LinkManager.GetDefaultUrlOptions();
            options.Language = LanguageManager.GetLanguage(language);
            var returnUrl = LandmarkHelper.TranslateUrl(LinkManager.GetItemUrl(item, options));

            if (Sitecore.Context.Item.ID.ToString() == ItemGuids.SearchResultsPage)
            {
                string rawUrl = System.Web.HttpContext.Current.Request.RawUrl.Replace("/en/", "/").Replace("/zh-cn/", "/").Replace("/zh-hk/", "/").Replace("/sc/", "/").Replace("/tc/", "/");
                if (language.ToLower() == "en")
                    return "/en" + rawUrl;
                if (language.ToLower() == "zh-cn")
                    return "/sc" + rawUrl;
                if (language.ToLower() == "zh-hk")
                    return "/tc" + rawUrl;
            }


            return TranslateUrl(returnUrl);
        }

        public static string TranslateUrl(string url)
        {
            string result = url;
            //Translate language codes
            result = result.Replace("/zh-hk/", "/tc/");
            result = result.Replace("/zh-cn/", "/sc/");
            result = result.Replace("/zh-hk", "/tc/");
            result = result.Replace("/zh-cn", "/sc/");

            return result;
        }

        public static bool IsShownInNavigation(Item item)
        {
            bool isShown = false;
            CheckboxField field = (CheckboxField)item.Fields["Is Shown In Navigation"];
            if (field != null)
            {
                isShown = field.Checked;
            }
            return isShown;
        }

        public static bool IsShownInBreadcrumb(Item item)
        {
            bool isShown = false;
            CheckboxField field = item.Fields["Is Shown In Breadcrumb"];
            if (field != null)
            {
                isShown = field.Checked;
            }
            return isShown;
        }
        public static List<Item> GetChildrenPageInNavigation(Item parentItem)
        {
            List<Item> resultsList = new List<Item>();
            int i = 0;
            foreach (Item child in parentItem.Children)
            {
                if (LandmarkHelper.IsShownInNavigation(child))
                {
                    resultsList.Insert(i, child);
                    i++;
                }
            }
            return resultsList;
        }

        public static string GetPageBodyClass()
        {
            string bodyClass = string.Empty;
            bodyClass = Sitecore.Context.Item.TemplateName.Split(' ')[0].ToLower();
            if (Sitecore.Context.Item.ID.ToString() == ItemGuids.ByArtistPage)
            {
                bodyClass = bodyClass + " " + "t22-is";
            }
            if (Sitecore.Context.Item.ID.ToString() == ItemGuids.ByLocationPage)
            {
                bodyClass = bodyClass + " " + "t22-svg";
            }
            return bodyClass;
        }

        public static string GetSlideClass(Item item)
        {
            string styleClass = string.Empty;
            //var multilistField = (MultilistField)Sitecore.Context.Item.Fields["MultilistFieldName"];
            var mainStyleField = (ReferenceField)item.Fields["Main Version Slide Style"];
            var subStyleField = (ReferenceField)item.Fields["Sub Version Slide Style"];
            if (mainStyleField.Value.Equals(ItemGuids.FullSizePhotoOverlayText) &
                subStyleField.Value.Equals(ItemGuids.TwoNumbersTwoCaptionsTextLink))
            {
                styleClass = "panel-3";
            }
            if (mainStyleField.Value.Equals(ItemGuids.LeftPhotoRightText) &
                subStyleField.Value.Equals(ItemGuids.TwoNumbersTwoCaptionsTextLink))
            {
                styleClass = "panel-1";
            }
            if (mainStyleField.Value.Equals(ItemGuids.LeftPhotoRightText) &
                subStyleField.Value.Equals(ItemGuids.OneTitleOneTextLink))
            {
                styleClass = "panel-2";
            }
            if (mainStyleField.Value.Equals(ItemGuids.FullSizePhotoOverlayText) &
                subStyleField.Value.Equals(ItemGuids.OneTitleOneTextLink))
            {
                styleClass = "panel-3";
            }
            return styleClass;
        }

        /// <summary>
        /// Gets the items by root and template.
        /// </summary>
        /// <param name="rootItemId">The root item.</param>
        /// <param name="templateItemId">The template item.</param>
        /// <returns>List{Item}.</returns>
        public static List<Item> GetItemsByRootAndTemplate(string rootItemId, string templateItemId)
        {
            Item home = Sitecore.Context.Database.GetItem(rootItemId);
            var query = string.Format("fast:{0}//*[{1}]", ToValidPath(home.Paths.FullPath),
                "@@TemplateId='" + templateItemId + "'");
            return _webDb.SelectItems(query).ToList();
        }

        public static List<Item> GetItemByTemplate(Item parent, string templateId)
        {
            var query = string.Format("fast:{0}//*[{1}]", ToValidPath(parent.Paths.FullPath), "@@TemplateId='" + templateId + "'");
            List<Item> slidesItems = Sitecore.Context.Database.SelectItems(query).OrderBy(i => i.DisplayName).ToList();
            return slidesItems;
        }

        public static string ToValidPath(string pathstring)
        {
            string[] paths = pathstring.Split('/');
            string[] newpaths = new string[paths.Length];
            for (int i = 0; i < paths.Length; i++)
            {
                if (paths[i].Contains(" "))
                    newpaths[i] = "#" + paths[i] + "#";
                else
                {
                    newpaths[i] = paths[i];
                }
            }
            return string.Join("/", newpaths);
        }

        public static List<Item> GetItemsByItemsTemplates(Item parent, Guid[] templateIds)
        {
            var queryTemplateArguments = templateIds.Select(tId => "@@TemplateId='{" + tId.ToString().ToUpper() + "}'").ToArray();
            var query = string.Format("fast:{0}//*[{1}]", parent.Paths.FullPath, string.Join(" or ", queryTemplateArguments));
            return parent.Database.SelectItems(query).ToList();
        }

        public static String FileFieldSrc(string fieldName, Item item)
        {
            string fileURL = string.Empty;
            FileField fileField = item.Fields[fieldName];
            if (fileField != null && fileField.MediaItem != null)
            {
                MediaItem file = new MediaItem(fileField.MediaItem);
                fileURL = Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(file));
            }
            return fileURL;
        }

        public static String ImageFieldSrc(string fieldName, Item item)
        {
            string imageURL = string.Empty;
            ImageField imageField = item.Fields[fieldName];
            if (imageField != null && imageField.MediaItem != null)
            {
                MediaItem image = new MediaItem(imageField.MediaItem);
                imageURL = Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(image));
            }
            return imageURL;
        }

        public static DateTime ToValidDateTime(DateField datetime)
        {
            return datetime.DateTime.AddHours(8);
        }

        public static List<Item> GetItemsByTags(Item item)
        {
            var tagsField = (MultilistField)item.Fields["Tags"];
            List<Item> relatedItems = new List<Item>();
            if (tagsField != null)
            {
                var parentItem = item.Parent;
                foreach (Item child in GetItemByTemplate(parentItem, ItemGuids.T4PageTemplate))
                {
                    if (child.ID.ToString() != item.ID.ToString())
                    {
                        var tags = (MultilistField)child.Fields["Tags"];
                        if (tags != null)
                        {
                            if (tags.TargetIDs.Any(id => tagsField.TargetIDs.Contains(id)))
                            {
                                relatedItems.Add(child);
                            }
                        }
                    }
                }
            }
            return relatedItems.Where(LandmarkHelper.IsShownInNavigation).ToList();
        }

        public static string GetFileName(string fieldName, Item item)
        {
            string name = string.Empty;
            FileField fileField = item.Fields[fieldName];
            if (fileField != null && fileField.MediaItem != null)
            {
                MediaItem file = new MediaItem(fileField.MediaItem);
                name = file.Name;
            }
            return name;
        }

        public static List<Item> GetBuildings()
        {
            return Sitecore.Context.Database.GetItem(ItemGuids.BuidingsFolder).Children.Where(building => ((CheckboxField)building.Fields["Is Landmark"]).Checked).OrderBy(p => p.DisplayName).ToList();
        }
        public static List<Item> GetAllBuildings()
        {
            return Sitecore.Context.Database.GetItem(ItemGuids.BuidingsFolder).Children.OrderBy(p => p.Fields["Building Index For Art"].Value).ToList();
        }

        /// <summary>
        /// Gets all articles.
        /// </summary>
        /// <returns>List{Item}.</returns>
        public static List<Item> GetAllArticles()
        {
            var t4Pages = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.NowAtLandmarkItem, ItemGuids.T4PageTemplate);
            var t27Pages = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.MonthlyExclusivePage, ItemGuids.T27Page);
            var t23PagesAB = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.LandmarkMaganizePage,
                ItemGuids.T23PageABTemplate);
            var t23PageCD = LandmarkHelper.GetItemsByRootAndTemplate(ItemGuids.LandmarkMaganizePage,
                ItemGuids.T23PageCTemplate);
            var t25pages = GetItemsByRootAndTemplate(ItemGuids.AroundCentralItem, ItemGuids.T25PageTemplate);
            var allArticles = t4Pages.Union(t27Pages).Union(t23PagesAB).Union(t23PageCD).Union(t25pages).ToList();
            return allArticles;
        }

        /// <summary>
        /// Gets the related items.
        /// </summary>
        /// <returns>List{Item}.</returns>
        public static List<Item> GetRelatedItems()
        {
            List<Item> allRelatedItems = new List<Item>();
            List<Item> relatedItems = new List<Item>();

            List<RelatedItem> relatedArticle = new List<RelatedItem>();
            Item currentItem = Sitecore.Context.Item;
            var relatedItemFolder = currentItem.Children.SingleOrDefault(p => p.TemplateID.ToString() == ItemGuids.RelatedItemFolder);
            if (relatedItemFolder != null)
            {
                relatedItems = relatedItemFolder.Children.Where(p => p.Publishing.IsPublishable(DateTime.Now, true)).ToList();
            }

            var allArticles = GetAllArticles();
            var itemTagsField = currentItem.Fields["Tags"];
            var attractionTags = itemTagsField.ToString().Split('|').ToList();
            foreach (var article in allArticles)
            {
                var articleTagsField = article.Fields["Tags"];
                var articleTags = articleTagsField.ToString().Split('|').ToList();
                var tags = articleTags.Intersect(attractionTags).ToList();

                if (tags.Count() != 0)
                {
                    RelatedItem relatedItem = new RelatedItem
                    {
                        Item = article,
                        TagCount = tags.Count()
                    };
                    relatedArticle.Add(relatedItem);
                    relatedArticle = relatedArticle.OrderBy(p => p.TagCount).ToList();
                }
            }
            if (relatedItems.Count > 0 && relatedArticle.Count > 0)
            {
                allRelatedItems.Add(relatedItems.FirstOrDefault());
                allRelatedItems.Add(relatedArticle.FirstOrDefault().Item);
            }
            if (relatedItems.Count == 0 && relatedArticle.Count > 0)
            {
                var relatedArticles = relatedArticle.Count > 2 ? relatedArticle.Take(2).ToList() : relatedArticle;

                allRelatedItems.AddRange(relatedArticles.Select(p => p.Item).ToList());
            }
            return allRelatedItems.Where(LandmarkHelper.IsShownInNavigation).ToList();
        }

        /// <summary>
        /// Gets the related pages.
        /// </summary>
        /// <returns>List{Item}.</returns>
        public static List<Item> GetRelatedPages()
        {
            List<Item> items = new List<Item>();
            Item currentItem = Sitecore.Context.Item;
            List<Item> relatedItems = new List<Item>();
            if (currentItem.ID.ToString() != ItemGuids.ThankYouPage)
            {
                var relatedItemFolder =
                    currentItem.Children.SingleOrDefault(p => p.TemplateID.ToString() == ItemGuids.RelatedItemFolder);
                if (relatedItemFolder != null)
                {
                    relatedItems = relatedItemFolder.Children.Where(i => LandmarkHelper.IsShownInNavigation(i)).ToList();
                }
            }
            var relatedPagesField = currentItem.Fields["Related Page"];
            if (relatedPagesField != null)
            {
                var relatedPagesIds = !string.IsNullOrEmpty(relatedPagesField.ToString()) ? relatedPagesField.ToString().Split('|').ToList() : new List<string>();
                if (relatedPagesIds.Count != 0)
                {
                    items.AddRange(relatedPagesIds.Select(pageId => Sitecore.Context.Database.GetItem(pageId)).Where(i => LandmarkHelper.IsShownInNavigation(i)));
                }
            }
            items.AddRange(relatedItems);
            return items.Where(IsShownInNavigation).ToList();
        }

        public static List<TagsTree> GetTagsTree()
        {
            List<TagsTree> tagsTrees = new List<TagsTree>();
            List<Item> allSubTags = new ItemList();
            MultilistField tagsField = Sitecore.Context.Item.Fields["Tags"];
            if (tagsField != null)
            {
                allSubTags = tagsField.GetItems().ToList();
                var tagGroups = allSubTags.GroupBy(p => p.ParentID).Select(p => new { id = p.Key, children = p }).ToList();
                foreach (var item in tagGroups)
                {
                    TagsTree tagsTree = new TagsTree()
                    {
                        CurrentItem = Sitecore.Context.Database.GetItem(item.id.ToString()),
                        Children = item.children.Select(p => new TagsTree()
                        {
                            CurrentItem = Sitecore.Context.Database.GetItem(p.ID.ToString()),
                        }).ToList()
                    };
                    tagsTrees.Add(tagsTree);
                }
            }
            return tagsTrees;
        }

        public static MailMessage ConstructEmailMessage(string emailBody, string subject, string mailTo, string from, string fromname)
        {
            var mailMessage = new MailMessage
            {
                Subject = subject,
                IsBodyHtml = true,
                Body = emailBody,
                From = new MailAddress(from, fromname)
            };

            if (mailTo.Length > 0)
                mailMessage.To.Add(mailTo);

            return mailMessage;
        }

        public static string GetCallOutImage(Item item)
        {
            string imageURL = "";
            ImageField imageField = item.Fields["Article Callout Image"];
            if (imageField != null)
            {
                if (imageField.MediaItem != null)
                {
                    MediaItem image = new MediaItem(imageField.MediaItem);
                    imageURL = Sitecore.StringUtil.EnsurePrefix('/',
                        Sitecore.Resources.Media.MediaManager.GetMediaUrl(image));
                    return imageURL;
                }
            }
            var sliders = GetItemByTemplate(item, ItemGuids.SlideObjectTemplate);
            if (sliders != null)
            {
                if (sliders.Any())
                    imageURL = ImageFieldSrc("Slide Image", sliders.FirstOrDefault());
            }
            return imageURL;
        }


        public static string GetCallOutImageForWaterfall(Item item)
        {
            string imageURL = "";
            ImageField imageField = item.Fields["Article Callout Image"];
            if (imageField != null)
            {
                if (imageField.MediaItem != null)
                {
                    MediaItem image = new MediaItem(imageField.MediaItem);
                    imageURL = Sitecore.StringUtil.EnsurePrefix('/',
                        Sitecore.Resources.Media.MediaManager.GetMediaUrl(image));
                    return imageURL;
                }
            }
            ImageField imagePortraitField = item.Fields["Article Portrait Callout Image"];
            if (imagePortraitField != null)
            {
                if (imagePortraitField.MediaItem != null)
                {
                    MediaItem image = new MediaItem(imagePortraitField.MediaItem);
                    imageURL = Sitecore.StringUtil.EnsurePrefix('/',
                        Sitecore.Resources.Media.MediaManager.GetMediaUrl(image));
                    return imageURL;
                }
            }
            var sliders = GetItemByTemplate(item, ItemGuids.SlideObjectTemplate);
            if (sliders != null)
            {
                if (sliders.Any())
                    imageURL = ImageFieldSrc("Slide Image", sliders.FirstOrDefault());
            }
            return imageURL;
        }

        public static string GetCurrentItemUrl()
        {
            string host = System.Web.HttpContext.Current.Request.Url.Scheme +
                          Uri.SchemeDelimiter +
                          System.Web.HttpContext.Current.Request.Url.Host;
            string language = Sitecore.Context.Language.ToString();
            return host + System.Web.HttpContext.Current.Request.RawUrl;
        }

        public static string GetImageItemAbsoluteUrl(ID imageid)
        {
            string imageUrl = string.Empty;
            Sitecore.Data.Items.MediaItem image = new Sitecore.Data.Items.MediaItem(Sitecore.Context.Database.GetItem(imageid));
            imageUrl = System.Web.HttpContext.Current.Request.Url.Scheme +
                              Uri.SchemeDelimiter +
                              System.Web.HttpContext.Current.Request.Url.Host + "/" + Sitecore.Context.Language.ToString()
                              + Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(image));
            return imageUrl;
        }

        public static String GetTargetString(Item item, string field)
        {
            String target = "_self";
            LinkField linkField = item.Fields[field];
            if (linkField.LinkType == "external" || linkField.Target == "New Browser")
            {
                target = "_blank";
            }
            return target;
        }

        public static string GetValueFromCurrentRenderingParameters(string parameterName)
        {
            var rc = RenderingContext.CurrentOrNull;
            if (rc == null || rc.Rendering == null) return (string)null;
            var parametersAsString = rc.Rendering.Properties["Parameters"];
            var parameters = HttpUtility.ParseQueryString(parametersAsString);
            return parameters[parameterName];
        }

        public static bool IsFalsePage(Item item)
        {
            if (item.Template.ID.ToString() == ItemGuids.PageObject || item.Template.ID.ToString() == ItemGuids.ShoppingPageObject)
            {
                return true;
            }
            return false;
        }

        public static string GetItemUrl(Item item)
        {
            string url = LandmarkHelper.TranslateUrl(LinkManager.GetItemUrl(item));
            if (IsFalsePage(item))
            {
                if ((new LayoutField(item).Value).IsNullOrEmpty())
                    url = "#";
            }
            return url;
        }

        public static IEnumerable<Language> GetLanguages()
        {
            LanguageCollection languagesCollection = Sitecore.Context.Database.GetLanguages();
            List<CultureInfo> cultureInfos = new List<CultureInfo>();
            List<Language> languages = new List<Language>();
            foreach (Language culture in languagesCollection)
            {
                Language sourceLanguage = LanguageManager.GetLanguage(culture.Name, Factory.GetDatabase("web"));
                if (!Sitecore.Context.Culture.EnglishName.Contains(culture.CultureInfo.EnglishName))
                    languages.Add(sourceLanguage);
            }
            return languages;
        }

        public static string GetLinkTarget(Item item, string linkName)
        {
            string target = "_self";
            LinkField linkField = item.Fields[linkName];
            if (linkField != null)
            {
                if (linkField.Target == "New Browser" || linkField.Target == "_blank")
                    target = "_blank";
            }
            return target;
        }

        public static string GetLinkUrl(Item item, string linkname)
        {
            string href = string.Empty;
            LinkField linkField = item.Fields[linkname];
            if (linkField != null)
            {
                if (linkField.LinkType == "external")
                    href = linkField.Url;
                if (linkField.IsInternal)
                {
                    if (linkField.TargetItem != null)
                        href = GetItemUrl(linkField.TargetItem);
                }
            }
            return href;
        }


        public static string GetPageSocialTitle()
        {
            string socialTitle = string.Empty;
            socialTitle = Sitecore.Context.Item.Fields["Social Share Title"].Value;
            if (string.IsNullOrEmpty(socialTitle))
                socialTitle = Sitecore.Context.Item.Fields["Meta Title"].Value;
            return socialTitle;
        }

        public static string GetPageSocialDescription()
        {
            string socialTitle = string.Empty;
            socialTitle = Sitecore.Context.Item.Fields["Social Share Description"].Value;
            if (string.IsNullOrEmpty(socialTitle))
                socialTitle = Sitecore.Context.Item.Fields["Meta Description"].Value;
            return socialTitle;
        }

        public static string SvgIdToShopId(this string svgId)
        {
            return new Regex("-").Replace(svgId.Replace("_x5F_", " & ").ToUpper(), " ", 1);
        }

        public static void RedirectPermanent(string newPath, int statuscode)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.StatusCode = statuscode;
            HttpContext.Current.Response.AddHeader("Location", newPath);
            HttpContext.Current.Response.End();
        }

    }
}
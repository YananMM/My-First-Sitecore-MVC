using System;
using Sitecore.Data.Items;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Landmark.Classes;
using Landmark.Models;
using Sitecore.Collections;
using Sitecore.Configuration;
using Sitecore.ContentSearch.Utilities;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Managers;
using Sitecore.Globalization;
using Sitecore.Links;
using Sitecore.Shell.Applications.ContentManager.ReturnFieldEditorValues;
using Sitecore.Web.UI;
using Attachment = Sitecore.Shell.Applications.ContentEditor.Attachment;
using DateTime = System.DateTime;

namespace Landmark.Helper
{
    public static class LandmarkHelper
    {
        private static Database _webDb = Factory.GetDatabase("web");

        public static int NumberInOnePage = ItemGuids.LandmarkConfigItem.Fields["Page"] == null
                ? 10
                : Int32.Parse(ItemGuids.LandmarkConfigItem.Fields["Page"].Value);

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
            var returnUrl = LinkManager.GetItemUrl(item, options);

            return TranslateUrl(returnUrl);
        }

        private static string TranslateUrl(string url)
        {
            string result = url;
            //Translate language codes
            result = result.Replace("/zh-HK/", "/tc/");
            result = result.Replace("/zh-CN/", "/sc/");
            //Fix root paths
            result = result.Replace("/en.aspx", "/en/default.aspx");
            result = result.Replace("/tc.aspx", "/tc/default.aspx");
            result = result.Replace("/zh-HK.aspx", "/tc/default.aspx");
            result = result.Replace("/sc.aspx", "/sc/default.aspx");
            result = result.Replace("/zh-CN.aspx", "/sc/default.aspx");
            //Fix double prefixes
            result = result.Replace("/en/en/", "/en/");
            result = result.Replace("/sc/sc/", "/sc/");
            result = result.Replace("/tc/tc/", "/tc/");
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
            Item shopping = Sitecore.Context.Database.GetItem(rootItemId);
            var query = string.Format("fast:{0}//*[{1}]", shopping.Paths.FullPath,
                "@@TemplateId='" + templateItemId + "'");
            return _webDb.SelectItems(query).ToList();
        }

        public static List<Item> GetItemByTemplate(Item parent,string templateId)
        {
            var query = string.Format("fast:{0}//*[{1}]", parent.Paths.FullPath, "@@TemplateId='" + templateId + "'");
            List<Item> slidesItems = _webDb.SelectItems(query).OrderBy(i => i.DisplayName).ToList();
            return slidesItems;
        }

        public static String FileFieldSrc(string fieldName, Item item)
        {
            string fileURL = string.Empty;
            Sitecore.Data.Fields.FileField fileField = item.Fields[fieldName];
            if (fileField != null && fileField.MediaItem != null)
            {
                Sitecore.Data.Items.MediaItem file = new Sitecore.Data.Items.MediaItem(fileField.MediaItem);
                fileURL = Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(file));
            }
            return fileURL;
        }

        public static String ImageFieldSrc(string fieldName, Item item)
        {
            string imageURL = string.Empty;
            Sitecore.Data.Fields.ImageField imageField = item.Fields[fieldName];
            if (imageField != null && imageField.MediaItem != null)
            {
                Sitecore.Data.Items.MediaItem image = new Sitecore.Data.Items.MediaItem(imageField.MediaItem);
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
                foreach (Item child in GetItemByTemplate(parentItem,ItemGuids.T4PageTemplate))
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
            return relatedItems;
        }

        public static string GetFileName(string fieldName, Item item)
        {
            string name = string.Empty;
            Sitecore.Data.Fields.FileField fileField = item.Fields[fieldName];
            if (fileField != null && fileField.MediaItem != null)
            {
                Sitecore.Data.Items.MediaItem file = new Sitecore.Data.Items.MediaItem(fileField.MediaItem);
                name = file.Name;
            }
            return name;
        }

        public static List<Item> GetBuildings()
        {
            return Sitecore.Context.Database.GetItem(ItemGuids.BuidingsFolder).Children.ToList();
        }

        /// <summary>
        /// Gets the related items.
        /// </summary>
        /// <returns>List{Item}.</returns>
        public static List<Item> GetRelatedItems()
        {
            List<Item> items = new List<Item>();
            Item item = Sitecore.Context.Item;
            var formInternal = (CheckboxField)item.Fields["Related Articles From Tags not Outside"];
            if (formInternal != null)
            {
                if (!formInternal.Checked)
                {
                    var root = item.Children.SingleOrDefault(p => p.TemplateID.ToString() == ItemGuids.RelatedItemFolder);
                    items = LandmarkHelper.GetItemsByRootAndTemplate(root.ID.ToString(), ItemGuids.ArticleObject);
                }
            }
            else
            {
                var tags = item.Fields["Tags"];
            }
            return items;
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
            string imageURL = string.Empty;
            Sitecore.Data.Fields.ImageField imageField = item.Fields["Article Slider Image"];
            if (imageField != null && imageField.MediaItem != null)
            {
                Sitecore.Data.Items.MediaItem image = new Sitecore.Data.Items.MediaItem(imageField.MediaItem);
                imageURL = Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(image));
            }
            else
            {
                Item slider = LandmarkHelper.GetItemByTemplate(Sitecore.Context.Item, ItemGuids.SlideObjectTemplate).FirstOrDefault();
                imageURL = FileFieldSrc("Slide Image",slider);
            }
            return imageURL;
        }

    }
}
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Publishing;
using Sitecore.SecurityModel;

namespace Landmark.Classes
{
    public class InstagramSocial
    {
        private readonly Database _webDb = Factory.GetDatabase("web");
        private readonly Database _masterDb = Factory.GetDatabase("master");
        private static List<SocialImage> _cache;
        private static DateTime _cacheTime = DateTime.MinValue;
        private readonly TimeSpan _cacheInterval = new TimeSpan(0, 1, 0);

        public List<SocialImage> GetFromInstagram(string userId, bool deep = false)
        {
            if (_cache != null && (DateTime.Now - _cacheTime) <= _cacheInterval )
            {
                return _cache;
            }
            Sitecore.Diagnostics.Log.Info("Update Instagram Feeds: " + userId, this);

            string clientId = Factory.GetDatabase("web").GetItem(ItemGuids.LandmarkConfigItem).Fields["Client Id"].Value;

            if (string.IsNullOrEmpty(userId))
                return new List<SocialImage>();

            // Feed format definition
            var feedPrototype = new
            {
                data = new[]
                {
                    new
                    {
                        user =new
                        {
                            username="",
                            profile_picture=""
                        },
                        created_time = 0,
                        link = "",
                        type = "",
                        id = "",
                        caption = new
                        {
                            text = ""
                        },
                        images = new 
                        {
                            low_resolution = new
                            {
                                url = "",
                                width = 0,
                                height = 0
                            },
                            thumbnail = new
                            {
                                url = "",
                                width = 0,
                                height = 0
                            },
                            standard_resolution = new
                            {
                                url = "",
                                width = 0,
                                height = 0
                            }
                        }
                    }
                }
            };

            var images = new List<SocialImage>();
            var lastIdParam = "";
            do
            {
                var uri = new Uri(string.Format(
                        "https://api.instagram.com/v1/users/{0}/media/recent?client_id={1}", userId, clientId));
                var response = Proxy(uri);

                var json = ReadResponse(response);
                if (json == null)
                    break;

                var feed = JsonConvert.DeserializeAnonymousType(json, feedPrototype);

                if (feed.data.Length <= 0)
                    break;

                // for next page
                lastIdParam = "&max_id=" + feed.data.Last().id;

                images.AddRange((from media in feed.data
                                 where media != null
                                 where media.type == "image"
                                 select new SocialImage
                                 {
                                     Id = media.id,
                                     PublishTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                                        .AddSeconds(media.created_time),
                                     Caption = media.caption != null ? media.caption.text : null,
                                     Url = media.images.standard_resolution.url,
                                     Link = media.link,
                                     User = media.user.username,
                                     ProfilePicture = media.user.profile_picture
                                 }));
            } while (deep);
            _cache = images;
            _cacheTime = DateTime.Now;
            return _cache;
        }

        public HttpWebResponse Proxy(Uri uri, bool useLocalProxy = true, Dictionary<string, string> addtionalHeaders = null, bool isPost = false, string postData = "")
        {
            var request = WebRequest.Create(uri) as HttpWebRequest;
            //if (useLocalProxy && ProxyServerEnabled == "Yes")
            //{
            //    request.Proxy = new WebProxy(ProxyServerAddress);
            //}
            request.Timeout = 15000;
            request.ReadWriteTimeout = 15000;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.16 Safari/537.36";

            if (addtionalHeaders != null)
            {
                foreach (var header in addtionalHeaders)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            if (isPost)
            {
                WriteRequest(request, postData);
            }

            try
            {
                return (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Exception when accessing url: " + uri, ex, this);
                return null;
            }
        }


        private static void WriteRequest(WebRequest request, string postData)
        {
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = "application/x-www-form-urlencoded";

            byte[] postDataBytes = Encoding.UTF8.GetBytes(postData);

            request.ContentLength = postDataBytes.Length;
            try
            {
                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(postDataBytes, 0, postDataBytes.Length);
                    requestStream.Close();
                }
            }
            catch (WebException ex)
            {
                // do nothing
            }
        }

        private static string ReadResponse(HttpWebResponse response)
        {
            if (response == null)
                return null;

            using (var responseStream = response.GetResponseStream())
            {
                try
                {
                    using (var responseReader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        return responseReader.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public void Run()
        {
            using (new SecurityDisabler())
            {
                TemplateItem instagramImageTemplateItem = _webDb.GetItem(ItemGuids.InstagramImageTemplate);
                Item folder = _webDb.GetItem(ItemGuids.InstagramFolder);
                Item folderMaster = _masterDb.GetItem(ItemGuids.InstagramFolder);
                var socialImages = new List<SocialImage>();
                socialImages.AddRange(GetFromInstagram(Factory.GetDatabase("web").GetItem(ItemGuids.LandmarkConfigItem).Fields["User Id"].Value).ToList().Take(10));
                if(socialImages.Count<1)
                    return;
                // insert new items
                var postsAdded = 0;
                //var instagramImages = folderMaster.Children
                //    .Where(item => item.TemplateID.Guid.ToString() == ItemGuids.InstagramImageTemplate);
                //foreach (Item instagramImage in instagramImages)
                //{
                //    _webDb.GetItem(instagramImage.ID).Delete();
                //    instagramImage.Delete();
                //}
                //Item ImageFolder = _masterDb.GetItem("/sitecore/media library/Images/Landmark/Instagram Folder");
                //var images = ImageFolder.Children;
                //foreach (Item instagramImage in images)
                //{
                //    _webDb.GetItem(instagramImage.ID).Delete();
                //    instagramImage.Delete();
                //}
                if (folderMaster!=null)
                    if(folderMaster.Children.Any())
                        folderMaster.DeleteChildren();
                if(folder!=null)
                    if (folder.Children.Any())
                        folder.DeleteChildren();
                foreach (var socialImage in socialImages)
                {
                    Sitecore.Diagnostics.Log.Info(string.Format("Creating Item: {0}, {1}", "Instagram " + socialImage.Id, socialImage.PublishTime), this);

                    var imageItem = SaveImage(socialImage.Url, socialImage.Caption);
                    var profileImageItem = SaveImage(socialImage.ProfilePicture, socialImage.User);

                    if (imageItem == null)
                        continue;
                    //if (_masterDb.GetItem(folderMaster.Paths.Path + socialImage.Id)!= null)
                    //{
                       // _masterDb.GetItem(folderMaster.Paths.Path + socialImage.Id).Delete();
                    //}
                    var newItem = folderMaster.Add(socialImage.Id, instagramImageTemplateItem);
                    
                    using (new EditContext(newItem))
                    {
                        newItem.Editing.BeginEdit();
                        newItem.Fields["Id"].Value = socialImage.Id;
                        newItem.Fields["User"].Value = socialImage.User;
                        newItem.Fields["Caption"].Value = socialImage.Caption;
                        //newItem.Fields["Profile Picture"].Value = socialImage.ProfilePicture;
                        newItem.Fields["Publish Time"].Value = socialImage.PublishTime.ToString("d/MM/yyyy hh:mm:ss tt", DateTimeFormatInfo.InvariantInfo);
                        if (!string.IsNullOrEmpty(socialImage.Link))
                        {
                            ((LinkField)newItem.Fields["Url"]).Url = socialImage.Link;
                        }
                        SetImageFieldValue(imageItem.ID.ToString(), newItem, "Image");
                        SetImageFieldValue(profileImageItem.ID.ToString(), newItem, "Profile Picture");
                        newItem.Editing.EndEdit();
                        newItem.Editing.AcceptChanges();
                    }
                    foreach (var language in newItem.Languages)
                    {
                        var langItem = _masterDb.GetItem(newItem.ID, language);

                        if (langItem.Versions.Count == 0)
                        {
                            langItem = langItem.Versions.AddVersion();
                            using (new EditContext(langItem))
                            {
                                foreach (var fld in newItem.Fields.Where(f => !f.Shared))
                                {
                                    langItem.Fields[fld.Name].Value = fld.Value;
                                }
                            }
                        }
                        Sitecore.Publishing.Pipelines.PublishItem.PublishItemPipeline.Run(
                            langItem.ID,
                            new PublishOptions(
                                _masterDb,
                                _webDb,
                                PublishMode.Smart,
                                language,
                                DateTime.Now
                                ));
                    }
                    postsAdded++;
                }
                foreach (var language in folderMaster.Languages)
                {
                    Sitecore.Publishing.Pipelines.PublishItem.PublishItemPipeline.Run(
                            folderMaster.ID,
                            new PublishOptions(
                                _masterDb,
                                _webDb,
                                PublishMode.Full,
                                language,
                                DateTime.Now
                                ));
                }
                
                Sitecore.Diagnostics.Log.Info(string.Format("Added {0} items", postsAdded), this);

            }
        }

        public MediaItem SaveImage(string url, string alt, bool useLocalProxy = true)
        {
            Sitecore.Diagnostics.Log.Info("Saving Image:" + url, this);

            var fileName = Path.GetFileName(new Uri(url).AbsolutePath);
            var itemName = Path.GetFileNameWithoutExtension(fileName);
            Item item = _masterDb.GetItem("/sitecore/media library/Images/Landmark/Instagram Folder/" + itemName);
            if (item != null)
            {
                return item;
            }
            var options =
                new Sitecore.Resources.Media.MediaCreatorOptions
                {
                    Database = _masterDb,
                    Language = Sitecore.Globalization.Language.Parse(Sitecore.Configuration.Settings.DefaultLanguage),
                    Versioned = true,
                    Destination = "/sitecore/media library/Images/Landmark/Instagram Folder/" + itemName,
                    FileBased = false,
                    IncludeExtensionInItemName = true,
                    AlternateText = alt
                };
            var creator = new Sitecore.Resources.Media.MediaCreator();

            var imgUri = new Uri(url);
            var response = Proxy(imgUri, useLocalProxy);
            try
            {
                using (var reader = new BinaryReader(response.GetResponseStream()))
                {
                    using (var stream = new MemoryStream())
                    {
                        byte[] buffer, file;
                        do
                        {
                            buffer = reader.ReadBytes(1024 * 4);
                            stream.Write(buffer, 0, buffer.Length);
                        } while (buffer.Length > 0);

                        var path = Path.GetTempPath();
                        using (var fileStream = File.Create(path + fileName))
                        {
                            stream.WriteTo(fileStream);
                        }

                        var imageItem = creator.CreateFromFile(path + fileName, options);
                        File.Delete(path + fileName);

                        foreach (var language in ((Item)imageItem).Languages)
                        {
                            var langItem = _masterDb.GetItem(imageItem.ID, language);

                            if (langItem.Versions.Count == 0)
                            {
                                langItem = langItem.Versions.AddVersion();
                                using (new EditContext(langItem))
                                {
                                    foreach (var fld in (((Item)imageItem).Fields).Where(f => !f.Shared))
                                    {
                                        langItem.Fields[fld.Name].Value = fld.Value;
                                    }
                                }
                            }

                            Sitecore.Publishing.Pipelines.PublishItem.PublishItemPipeline.Run(
                                imageItem.ID,
                                new PublishOptions(
                                    _masterDb,
                                    _webDb,
                                    PublishMode.Smart,
                                    language,
                                    DateTime.Now
                                    ));
                        }
                        return imageItem;

                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void SetImageFieldValue(string imageid, Item item, string imageField)
        {
            using (new SecurityDisabler())
            {
                MediaItem imageItem = _masterDb.GetItem(imageid);
                if (imageItem != null)
                {
                    using (new EditContext(item))
                    {
                        Sitecore.Data.Fields.ImageField imagefield = item.Fields[imageField];
                        imagefield.Alt = imageItem.Alt;
                        imagefield.MediaID = imageItem.ID;
                    }
                    //item.Fields[imageField].Value =
                    //    @"<image mediapath="" alt="""+"test"+@" width="" height="" hspace="" vspace="" showineditor="" usethumbnail="" src="" mediaid="+imageid+" />";
                }
            }
        }

    }

    public class SocialImage
    {
        public string Id { get; set; }
        public string Caption { get; set; }
        public DateTime PublishTime { get; set; }
        public string Url { get; set; }
        public string Link { get; set; }
        public string User { get; set; }
        public string ProfilePicture { get; set; }
    }
}
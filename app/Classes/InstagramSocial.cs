using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace Landmark.Classes
{
    public class InstagramSocial
    {
        public List<SocialImage> GetFromInstagram(string userId, bool deep = false)
        {
            Sitecore.Diagnostics.Log.Info("Update Instagram Feeds: " + userId, this);

            string clientId = "54d656cb6fb847b88c8a42f8a92d33d9";

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
                        "https://api.instagram.com/v1/users/{0}/media/recent?count=100{1}&client_id={2}", userId, lastIdParam, clientId));
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
            return images;
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
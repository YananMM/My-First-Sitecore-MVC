using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Landmark.Classes
{
    public class InstagramSocial
    {
        //public List<SocialImage> GetFromInstagram(string userId, bool deep = false)
        //{
        //    Sitecore.Diagnostics.Log.Info("Update Instagram Feeds: " + userId, this);

        //    var clientId = _snsAppSettingsItem.Fields["Instagram Application Client Id"].Value;

        //    if (string.IsNullOrEmpty(userId))
        //        return new List<SocialImage>();

        //    // Feed format definition
        //    var feedPrototype = new
        //    {
        //        data = new[]
        //        {
        //            new
        //            {
        //                created_time = 0,
        //                link = "",
        //                type = "",
        //                id = "",
        //                caption = new
        //                {
        //                    text = ""
        //                },
        //                images = new 
        //                {
        //                    low_resolution = new
        //                    {
        //                        url = "",
        //                        width = 0,
        //                        height = 0
        //                    },
        //                    thumbnail = new
        //                    {
        //                        url = "",
        //                        width = 0,
        //                        height = 0
        //                    },
        //                    standard_resolution = new
        //                    {
        //                        url = "",
        //                        width = 0,
        //                        height = 0
        //                    }
        //                }
        //            }
        //        }
        //    };

        //    var images = new List<SocialImage>();
        //    var lastIdParam = "";
        //    do
        //    {
        //        var uri =
        //            new Uri(string.Format(
        //                "https://api.instagram.com/v1/users/{0}/media/recent?count=100{1}&client_id={2}", userId, lastIdParam, clientId));
        //        var response = Proxy(uri);

        //        var json = ReadResponse(response);
        //        if (json == null)
        //            break;

        //        var feed = JsonConvert.DeserializeAnonymousType(json, feedPrototype);

        //        if (feed.data.Length <= 0)
        //            break;

        //        // for next page
        //        lastIdParam = "&max_id=" + feed.data.Last().id;

        //        images.AddRange((from media in feed.data
        //                         where media != null
        //                         where media.type == "image"
        //                         select new SocialImage
        //                         {
        //                             Id = media.id,
        //                             PublishTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
        //                                .AddSeconds(media.created_time),
        //                             Caption = media.caption != null ? media.caption.text : null,
        //                             Url = media.images.standard_resolution.url,
        //                             Link = media.link
        //                         }));
        //    } while (deep);
        //    return images;
        //}
    }

    public class SocialImage
    {
        public string Id { get; set; }
        public string Caption { get; set; }
        public DateTime PublishTime { get; set; }
        public string Url { get; set; }
        public string Link { get; set; }
    }
}
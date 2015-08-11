using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Landmark.Helper
{
    public class ArtTourHelper
    {
        public Item GetArtistByArt(Item art)
        {
            Sitecore.Data.Fields.GroupedDroplinkField artistField = art.Fields["Artist"];
            if (artistField != null)
            {
                return artistField.TargetItem;
            }
            return null;
        }
    }
}
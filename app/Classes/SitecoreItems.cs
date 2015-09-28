using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Landmark.Classes
{
    public static class SitecoreItems
    {
        private static Database _web = Factory.GetDatabase("web");
        //item 
        public static readonly Item LandmarkHomeItem = _web.GetItem("{497220CE-7763-4A17-9D3F-4A6DBF1B8CDB}");
        public static readonly Item SearchPlaceHolderItem = _web.GetItem("{ED07CD50-68B7-4E7A-BE15-F161360F4E07}");
        public static readonly Item LandmarkConfigItem = _web.GetItem("{EE7EA867-0191-406D-8B84-A28B236CD725}");
        public static readonly Item FooterNavigationItem = _web.GetItem("{5B9F3F5E-A640-4D05-8CF6-3A4591751CA9}");
        public static readonly Item ScrollToBeginGuidItem = _web.GetItem("{6EA65B2A-282C-412A-8033-536E855F630F}");
        public static readonly Item TopGuidItem = _web.GetItem("{07F2749E-EAF3-4ADB-901F-E0454763D0DF}");
        public static readonly Item TraditionalChineseGuidItem = _web.GetItem("{28645F0A-B081-4EB2-912D-691E017B8836}");
        public static readonly Item SimplifiedChineseGuidItem = _web.GetItem("{2FEA334B-684F-40E0-99A4-3CD3E4C704F3}");
        public static readonly Item ShareThisGuidItem = _web.GetItem("{A54A3CE6-2BEC-47CF-A63F-4A2F964C6320}");
        public static readonly Item SeeAllBrandsGuidItem = _web.GetItem("{D380FDA5-C54A-48D8-B246-21571AC525B7}");
        public static readonly Item ListingGuidItem = _web.GetItem("{DAEE85AD-9210-48C8-8E29-F7CADB157ECC}");
        public static readonly Item AlsoInterestedInGuidItem = _web.GetItem("{A6F6E24F-8844-4588-9FE2-0765E5851963}");
        public static readonly Item AllBrandsGuidItem = _web.GetItem("{4E305EBF-9B1D-4EEA-AAA7-59CD1B3DD3D5}");
        public static readonly Item ByBrandsGuidItem = _web.GetItem("{23C8C729-14B1-45B9-9557-404516153453}");
        public static readonly Item ByBuidingsGuidItem = _web.GetItem("{7B87FE4A-D9B2-4451-BB6D-441B01B1462A}");
        public static readonly Item GoGuidItem = _web.GetItem("{EB7EDF9B-07D2-4CD9-8B21-AF7A15800F98}");
        public static readonly Item GoToGuidItem = _web.GetItem("{6A742FAE-6615-4BE5-A8A9-4471DFDC60E5}");
        public static readonly Item SeeAlsoGuidItem = _web.GetItem("{E9CD2E74-E53B-4711-9DB6-88D3DEFA14FC}");
        public static readonly Item ContactItem = _web.GetItem("{D6F1BBEB-2483-44F1-A547-68D6AE8E2A51}");
        public static readonly Item WhereItem = _web.GetItem("{C01AD804-C7C9-4791-8CFC-DA35B6D09179}");
        public static readonly Item AlsoOnThisFloorItem = _web.GetItem("{6C2FE71F-0F24-49BD-A345-7C98BDE4B249}");
        public static readonly Item YouMayAlsoLikeItem = _web.GetItem("{23815E63-F3F5-48BE-BBEA-5CA6D8CCFECA}");
        public static readonly Item TagsItem = _web.GetItem("{B05FA79E-B0AC-4D78-9175-ADA8305B5B5B}");
        public static readonly Item FloorPlanItem = _web.GetItem("{B6888C19-B10C-4662-8E41-7AC2CCE8419A}");
        public static readonly Item ByCategoryGuidItem = _web.GetItem("{E7E0A523-FFB0-4842-9DD0-7047B1F313B9}");
        public static readonly Item RealatedArticlesItem = _web.GetItem("{70032C39-DB3A-42F1-91FC-9D85BE84A3F7}");
        public static readonly Item MobileAudioGuideItem = _web.GetItem("{DD2597D1-42CF-4A71-BD93-8A05851B91B9}");
        public static readonly Item ListenToCommentaryItem = _web.GetItem("{55FAB28E-6000-4382-833A-4A29C8CA38B0}");
        public static readonly Item LocationItem = _web.GetItem("{944602F7-94FA-4927-8D72-65326C2F9C32}");
        public static readonly Item AlsoInItem = _web.GetItem("{0F082781-8037-4643-96F6-88104B58539A}");
        public static readonly Item ByThisArtistItem = _web.GetItem("{B8DBA1C5-3950-4A13-A460-7C7A1795B682}");
        public static readonly Item ViewByItem = _web.GetItem("{79222D4A-7956-4441-9D67-B080494D69BC}");
        public static readonly Item ArtistItem = _web.GetItem("{66B7043C-69C4-4212-A719-F4CE6EBCCB19}");
        public static readonly Item MoreItem = _web.GetItem("{30811B28-1ECB-4137-B8BF-B962FF16D7E7}");
        public static readonly Item OfficialWebsiteItem = _web.GetItem("{86B65A99-F8BE-42C1-A50F-A346ACFA9A6D}");
        public static readonly Item OpenningHoursItem = _web.GetItem("{86EDB6E7-348D-4F43-AE03-3F0EA2F937AC}");
        public static readonly Item DateTimeItem = _web.GetItem("{6DABDA46-E90A-4BEA-AF61-1B9A385C31BF}");
        public static readonly Item EnquiryItem = _web.GetItem("{C236AA00-30F1-43BB-A426-C477DE3F264D}");
        public static readonly Item VenueItem = _web.GetItem("{19EF3D69-3DDC-4A84-A1F7-F93E2098929D}");
        public static readonly Item WebsiteItem = _web.GetItem("{AEC15D0C-B92E-47D7-991C-EE96110E609A}");
        public static readonly Item DiscoverMoreItem = _web.GetItem("{30D7A7AF-BF8C-4A89-B226-C621E44E1EFB}");
        public static readonly Item AlsoHappenInLandmarkItem = _web.GetItem("{F00CB9D5-2A9A-4C42-860A-4B0476B41C6F}");
        public static readonly Item FilterByItem = _web.GetItem("{C477E1B6-E381-43A7-9B3F-E5A6F15D9A74}");
        public static readonly Item PhoneItem = _web.GetItem("{4AAF25F5-0A1C-45AA-B81D-B27AD46F37C2}");
        public static readonly Item StoreDetailsItem = _web.GetItem("{58C95FD5-61B2-4DAE-9C5B-6BAE61A2D98D}");
        public static readonly Item AddressItem = _web.GetItem("{86E8B0C2-6C91-45D9-949C-DE5C1CB64ED3}");
        public static readonly Item FindByBrandItem = _web.GetItem("{88028182-7209-4EBB-8CBA-1E6BBE40FCCF}");
        public static readonly Item EmailItem = _web.GetItem("{E3B50ED3-8CDC-41DA-A5D4-176A55CBA63A}");
        public static readonly Item ShowAllItem = _web.GetItem("{2F9D72B2-FF66-4302-8ABB-BB5F7C697FE5}");
        public static readonly Item SeeAllExclusiveItem = _web.GetItem("{4F5C3F3D-9AB5-424B-9C78-41DC7AEECB8F}");
        public static readonly Item SeeAllExclusive = _web.GetItem("{2283A3E4-45AE-4164-8C82-6D9F48D0602C}");
        public static readonly Item ReadMoreItem = _web.GetItem("{738E90D0-A3B0-46A3-8DF4-6FE1825B51D2}");
        public static readonly Item AllTextItem = _web.GetItem("{0E954D78-0836-46D0-938A-29E235F975ED}");
        public static readonly Item ShareItem = _web.GetItem("{2224057F-0DA3-4D7E-9089-99CB8CC77C0F}");
        public static readonly Item LearnMoreItem = _web.GetItem("{3ADB531C-1F86-4963-859D-9525EE4D833A}");
        public static readonly Item ViewMapItem = _web.GetItem("{615AE910-37B4-44C8-B550-32DAA8CCF66B}");
        public static readonly Item FaxItem = _web.GetItem("{4C157DD6-6AF2-4112-B610-AFFC84C6CCF1}");
        public static readonly Item TelephoneItem = _web.GetItem("{0B3DA006-CBA6-44CA-9421-88965D431F2F}");
        public static readonly Item DiscoverLandmarkStoriesItem = _web.GetItem("{5731F621-01CE-4596-9DA3-55A231D0D21F}");
        public static readonly Item AllItem = _web.GetItem("{4ACF0FF2-331F-47A8-994A-A3D709A2524F}");
        public static readonly Item SortAndGoToItem = _web.GetItem("{A67F7267-0402-4356-9EDF-5F1016C67F15}");
        public static readonly Item SeeMoreItem = _web.GetItem("{0BA79670-6D57-473C-B495-1A01DB6CA2CA}");
        public static readonly Item MagezineItem = _web.GetItem("{8B359693-D433-4F44-98F8-9CEE5FA9B72F}");
        public static readonly Item ListenToCommentaryAltItem = _web.GetItem("{36C61E1D-7652-4DD4-8CCA-DDF1067D89D3}");

    }
}
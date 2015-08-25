using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Web.UI;

namespace Landmark.Classes
{
    public abstract class ItemGuids
    {
        //rendering guids
        public static readonly String GlossaryRenderingViewGuid = "{3825AF49-B359-4B8D-85CC-809873CB0FFD}";
        public static readonly String ScriptRenderingViewGuid = "{85E63E0F-B889-4A32-9EAD-DDFE06BC3573}";
        public static readonly String FooterRenderingViewGuid = "{CE360129-0EE2-47FC-A1D9-3A28105B1249}";
        public static readonly String T1PanelNavRenderingViewGuid = "{026C458F-E4BC-490B-A097-DBE0C6DA9459}";
        public static readonly String HeaderRenderingViewGuid = "{38268C3C-71E7-4775-8E91-FB4DF4667C34}";
        public static readonly String TagsRenderingViewGuid = "{5E1A037B-CB78-4FFC-8758-7E66DC14EADC}";
        public static readonly String FooterNavRenderingViewGuid = "{5BBD6426-6C9C-4E73-BD1F-34BC6D63961E}";
        public static readonly String FooterConfigRenderingViewGuid = "{18C07F19-B608-411E-81E2-FDADAE97176D}";
        public static readonly String FooterSocialMediaRenderingGuid = "{5359BCC1-D4EB-4D7B-BBD8-11D1597BAB92}";
        public static readonly String NavigationRenderingViewGuid = "{C72D39E8-5FD2-40F6-B815-83DAD5E92CC3}";
        public static readonly String MobileNavigationRenderingViewGuid = "{61EC411E-0F20-4B97-A0D4-A880A4ACD3E5}";
        //public static readonly String T11BreadcrymbRenderingViewGuid = "{C85A5336-5D9F-47A0-ADBD-DA8F6975E51B}";
        public static readonly String ByBrandsRenderingViewGuid = "{0B02D3EC-0648-4B99-BEB4-8575C48FAE64}";
        public static readonly String ByBuildingsRenderingViewGuid = "{B08E278B-BF3C-4994-A65D-D764193C6447}";
        public static readonly String T13SideBarRenderingViewGuid = "{A88DF21E-E1DB-408B-9BC5-14F36C83513D}";
        public static readonly String T13RelatedCategoryViewGuid = "{E12C109C-E610-4CB3-BA45-003C92C3FF96}";
        //public static readonly String T14ShopDataRenderingViewGuid = "{CCD0CC2A-56DD-4990-9395-560FA0F62D7B}";
        //public static readonly String T14TagsRenderingViewGuid = "{303EA462-99C6-447B-B3AA-C4F4F0441549}";
        public static readonly String T14AlsoOnFloorRenderingViewGuid = "{E4C9F730-0F32-481A-A631-814E9DD01CF0}";
        public static readonly String T14YouMayAlsoLikeRenderingViewGuid = "{3D3C605D-3A95-4D71-8206-C403D66EB91A}";
        public static readonly String T35GetDirectionPage = "{AE130080-B8B6-4D9E-B863-C88DC329B657}";
        public static readonly String T35SurroundingPage = "{54824316-017A-4B94-AD8E-F636473B510A}";

        //item id
        //public static readonly String PanelsFolderItemGuid = "{3BB40D54-8F7F-449B-B9DB-432C65EB4224}";
        //public static readonly String AdvertTemplateItemGuid = "{32677AD3-AB70-49BF-A525-08493045C4FA}";
        public static readonly String FullSizePhotoOverlayText = "{66B59687-427D-4BB3-8CA3-1F2D4971E301}";
        public static readonly String LeftPhotoRightText = "{EDDA98B8-D3CC-4BF0-9DF0-3E4FF82D88E0}";
        public static readonly String OneTitleOneTextLink = "{F2834073-085E-429E-A9A2-40DD9FC9FC97}";
        public static readonly String TwoNumbersTwoCaptionsTextLink = "{BC7C0D97-3B48-4FD9-B25D-AA3F37EE6C90}";
        public static readonly String T11PageTemplate = "{3832F0F6-6342-4394-932C-2DDAB1164C97}";
        public static readonly String ShoppingItem = "{07A37ABC-D676-4737-8B14-79EC18486CE8}";
        public static readonly String BuidingsFolder = "{4731A63F-BFED-4A7B-8650-1ED611731530}";
        public static readonly String T14ShopDetailsTemplate = "{91A88069-9A0E-48B7-9509-BDE830A54D0E}";
        public static readonly String ShoppingCategory = "{FB0C20EF-8DD8-4233-A9E8-3D1536412DB7}";
        public static readonly String ShoppingCategoryObject = "{753658B3-59D8-4DAA-8FC8-CFE38C0EFD20}";
        public static readonly String AllByBrandsPage = "{670F29E6-B282-4234-B139-E8F74AD95295}";
        public static readonly String AllByBuildingsPage = "{3FAF2834-AF45-4CD7-BF22-6EEBCA0894C1}";
        public static readonly String ShoppingSubCategoryPageObject = "{11D18D8C-A8E3-4A15-A295-DAE23894A0AE}";
        public static readonly String CategoryObjectTemplate = "{753658B3-59D8-4DAA-8FC8-CFE38C0EFD20}";
        public static readonly String SlideObjectTemplate = "{FFC1447C-5F42-4419-AEF4-6175FB69507C}";
        public static readonly String ButtomSlideObjectTemplate = "{BCC31564-C738-41D5-ABA5-FD436557DF85}";
        public static readonly String ImageTextObjectTemplate = "{CFE5379F-402F-4FB5-A1C5-0E42CA464BC1}";
        public static readonly String NewsletterSignUpPage = "{5E154158-9B1A-4471-ADE5-B5CC226B1161}";
        public static readonly String FooterSocialMediaFolderItem = "{08BB2BC5-ACFC-4418-AED0-4B8DC6D1B0CA}";
        public static readonly String LandmarkArtTourItem = "{5C03DD0D-4881-4A9B-A58D-5FFA101B3533}";
        public static readonly String T29Template = "{ADE0F061-FF28-4CDB-B6C3-14021D75355A}";
        public static readonly String T30Template = "{EC149566-D74F-4E4F-9F2C-A0745E72988E}";
        public static readonly String BuildingDataObject = "{873FCBBD-5F81-4DD2-B885-DFDCA194A5D1}";
        public static readonly String ByArtistPage = "{5126FB8C-1FB4-4C06-A8AC-23D8B260DF23}";
        public static readonly String ByLocationPage = "{2149D38C-BDE5-404C-A1AB-4E8245B3228C}";
        public static readonly String HotelPageObject = "{E04426CD-2C71-48F8-A1CA-C507C2CAB178}";
        public static readonly String T34Template = "{C08DC2B0-088B-4182-81C4-4D7510A741C0}";
        public static readonly String T34Branch = "{8B38BE26-F07E-42C3-9C4E-6B7D1770BAB1}";
        public static readonly String HotelsPage = "{8A01BBF3-963D-4011-AC05-5DB0A2B4BA73}";
        public static readonly String T34SpecialOfferObject = "{D3130DE9-ABC4-4D10-92C8-75A8E9910245}";
        public static readonly String T35StartingLocationObject = "{3E8C53BC-1805-434C-BD94-65316F9AFC71}";
        public static readonly String T35GetDirectionsPage = "{C4B0C207-D66A-4E2F-9469-6B8FF62A4CB1}";

        //item 
        public static readonly Item LandmarkHomeItem = Sitecore.Context.Database.GetItem("{497220CE-7763-4A17-9D3F-4A6DBF1B8CDB}");
        public static readonly Item SearchPlaceHolderItem = Sitecore.Context.Database.GetItem("{ED07CD50-68B7-4E7A-BE15-F161360F4E07}");
        public static readonly Item LandmarkConfigItem = Sitecore.Context.Database.GetItem("{EE7EA867-0191-406D-8B84-A28B236CD725}");
        public static readonly Item FooterNavigationItem = Sitecore.Context.Database.GetItem("{5B9F3F5E-A640-4D05-8CF6-3A4591751CA9}");
        public static readonly Item ScrollToBeginGuidItem = Sitecore.Context.Database.GetItem("{6EA65B2A-282C-412A-8033-536E855F630F}");
        public static readonly Item TopGuidItem = Sitecore.Context.Database.GetItem("{07F2749E-EAF3-4ADB-901F-E0454763D0DF}");
        public static readonly Item TraditionalChineseGuidItem = Sitecore.Context.Database.GetItem("{28645F0A-B081-4EB2-912D-691E017B8836}");
        public static readonly Item SimplifiedChineseGuidItem = Sitecore.Context.Database.GetItem("{2FEA334B-684F-40E0-99A4-3CD3E4C704F3}");
        public static readonly Item ShareThisGuidItem = Sitecore.Context.Database.GetItem("{A54A3CE6-2BEC-47CF-A63F-4A2F964C6320}");
        public static readonly Item SeeAllBrandsGuidItem = Sitecore.Context.Database.GetItem("{D380FDA5-C54A-48D8-B246-21571AC525B7}");
        public static readonly Item AlsoInterestedInGuidItem = Sitecore.Context.Database.GetItem("{A6F6E24F-8844-4588-9FE2-0765E5851963}");
        public static readonly Item AllBrandsGuidItem = Sitecore.Context.Database.GetItem("{4E305EBF-9B1D-4EEA-AAA7-59CD1B3DD3D5}");
        public static readonly Item ByBrandsGuidItem = Sitecore.Context.Database.GetItem("{23C8C729-14B1-45B9-9557-404516153453}");
        public static readonly Item ByBuidingsGuidItem = Sitecore.Context.Database.GetItem("{7B87FE4A-D9B2-4451-BB6D-441B01B1462A}");
        public static readonly Item GoGuidItem = Sitecore.Context.Database.GetItem("{EB7EDF9B-07D2-4CD9-8B21-AF7A15800F98}");
        public static readonly Item GoToGuidItem = Sitecore.Context.Database.GetItem("{6A742FAE-6615-4BE5-A8A9-4471DFDC60E5}");
        public static readonly Item SeeAlsoGuidItem = Sitecore.Context.Database.GetItem("{E9CD2E74-E53B-4711-9DB6-88D3DEFA14FC}");
        public static readonly Item ContactItem = Sitecore.Context.Database.GetItem("{D6F1BBEB-2483-44F1-A547-68D6AE8E2A51}");
        public static readonly Item WhereItem = Sitecore.Context.Database.GetItem("{C01AD804-C7C9-4791-8CFC-DA35B6D09179}");
        public static readonly Item AlsoOnThisFloorItem = Sitecore.Context.Database.GetItem("{6C2FE71F-0F24-49BD-A345-7C98BDE4B249}");
        public static readonly Item YouMayAlsoLikeItem = Sitecore.Context.Database.GetItem("{23815E63-F3F5-48BE-BBEA-5CA6D8CCFECA}");
        public static readonly Item TagsItem = Sitecore.Context.Database.GetItem("{B05FA79E-B0AC-4D78-9175-ADA8305B5B5B}");
        public static readonly Item FloorPlanItem = Sitecore.Context.Database.GetItem("{B6888C19-B10C-4662-8E41-7AC2CCE8419A}");
        public static readonly Item ByCategoryGuidItem = Sitecore.Context.Database.GetItem("{E7E0A523-FFB0-4842-9DD0-7047B1F313B9}");
        public static readonly Item RealatedArticlesItem = Sitecore.Context.Database.GetItem("{70032C39-DB3A-42F1-91FC-9D85BE84A3F7}");
        public static readonly Item MobileAudioGuideItem = Sitecore.Context.Database.GetItem("{DD2597D1-42CF-4A71-BD93-8A05851B91B9}");
        public static readonly Item ListenToCommentaryItem = Sitecore.Context.Database.GetItem("{55FAB28E-6000-4382-833A-4A29C8CA38B0}");
        public static readonly Item LocationItem = Sitecore.Context.Database.GetItem("{944602F7-94FA-4927-8D72-65326C2F9C32}");
        public static readonly Item AlsoInItem = Sitecore.Context.Database.GetItem("{0F082781-8037-4643-96F6-88104B58539A}");
        public static readonly Item ByThisArtistItem = Sitecore.Context.Database.GetItem("{B8DBA1C5-3950-4A13-A460-7C7A1795B682}");
        public static readonly Item ViewByItem = Sitecore.Context.Database.GetItem("{79222D4A-7956-4441-9D67-B080494D69BC}");
        public static readonly Item ArtistItem = Sitecore.Context.Database.GetItem("{66B7043C-69C4-4212-A719-F4CE6EBCCB19}");
        public static readonly Item MoreItem = Sitecore.Context.Database.GetItem("{30811B28-1ECB-4137-B8BF-B962FF16D7E7}");
        public static readonly Item OfficialWebsiteItem = Sitecore.Context.Database.GetItem("{86B65A99-F8BE-42C1-A50F-A346ACFA9A6D}");
        public static readonly Item OpenningHoursItem = Sitecore.Context.Database.GetItem("{86EDB6E7-348D-4F43-AE03-3F0EA2F937AC}");
        public static readonly Item DateTimeItem = Sitecore.Context.Database.GetItem("{6DABDA46-E90A-4BEA-AF61-1B9A385C31BF}");
        public static readonly Item EnquiryItem = Sitecore.Context.Database.GetItem("{C236AA00-30F1-43BB-A426-C477DE3F264D}");
        public static readonly Item VenueItem = Sitecore.Context.Database.GetItem("{19EF3D69-3DDC-4A84-A1F7-F93E2098929D}");
        public static readonly Item WebsiteItem = Sitecore.Context.Database.GetItem("{AEC15D0C-B92E-47D7-991C-EE96110E609A}");

        public static readonly Item PhoneItem = Sitecore.Context.Database.GetItem("{4AAF25F5-0A1C-45AA-B81D-B27AD46F37C2}");

        public static readonly Item AddressItem = Sitecore.Context.Database.GetItem("{86E8B0C2-6C91-45D9-949C-DE5C1CB64ED3}");

        public static readonly Item EmailItem = Sitecore.Context.Database.GetItem("{E3B50ED3-8CDC-41DA-A5D4-176A55CBA63A}");

        public static readonly Item ViewMapItem = Sitecore.Context.Database.GetItem("{615AE910-37B4-44C8-B550-32DAA8CCF66B}");
        public static readonly Item FaxItem = Sitecore.Context.Database.GetItem("{4C157DD6-6AF2-4112-B610-AFFC84C6CCF1}");
        public static readonly Item TelephoneItem = Sitecore.Context.Database.GetItem("{0B3DA006-CBA6-44CA-9421-88965D431F2F}");
    }
}
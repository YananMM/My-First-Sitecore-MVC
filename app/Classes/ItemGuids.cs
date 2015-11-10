﻿using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Sitecore.Web.UI;

namespace Landmark.Classes
{
    public abstract class ItemGuids
    {
        public static readonly String TextObject = "{55CBD7E3-79B5-4A39-8329-EDD07EBE5ED5}";
        public static readonly String RelatedItemViewGuid = "{19B03C96-4203-4877-8AC0-2D03E76DA38B}";
        public static readonly String ValetParkingPage = "{819779A7-6864-451A-83DA-703DF00AD09D}";
        public static readonly String UnorderedListTemplate = "{17853181-56B2-4240-84FC-EFF772333FEC}";
        public static readonly String TextContentTemplate = "{95710548-2E7F-4FD5-B97D-CAF1396CE80B}";
        public static readonly String ImageFolderTemplate = "{EC3DBE3A-5E40-4B96-A67B-593FE8E67B72}";
        public static readonly String MultipleVisualBlocksTemplate = "{C9307810-5FD7-4B38-9DA0-F50538F83F7D}";
        //item id
        //public static readonly String PanelsFolderItemGuid = "{3BB40D54-8F7F-449B-B9DB-432C65EB4224}";
        //public static readonly String AdvertTemplateItemGuid = "{32677AD3-AB70-49BF-A525-08493045C4FA}";
        public static readonly String FullSizePhotoOverlayText = "{66B59687-427D-4BB3-8CA3-1F2D4971E301}";
        public static readonly String LeftPhotoRightText = "{EDDA98B8-D3CC-4BF0-9DF0-3E4FF82D88E0}";
        public static readonly String OneTitleOneTextLink = "{F2834073-085E-429E-A9A2-40DD9FC9FC97}";
        public static readonly String TwoNumbersTwoCaptionsTextLink = "{BC7C0D97-3B48-4FD9-B25D-AA3F37EE6C90}";
        public static readonly String T11PageTemplate = "{3832F0F6-6342-4394-932C-2DDAB1164C97}";
        public static readonly String ShoppingItem = "{07A37ABC-D676-4737-8B14-79EC18486CE8}";
        public static readonly String DiningItem = "{AA676B4A-E135-4B6F-8889-B9AFBDED5699}";
        public static readonly String NowAtLandmarkItem = "{F0A68E40-FB4F-4B85-969D-A6ADEE24DE6B}";
        public static readonly String InspirationItem = "{C6CBC0CC-7FC3-440A-9294-167C0674E43A}";
        public static readonly String AroundCentralItem = "{57B8DEB0-CDFF-4287-94D6-E19191142B1B}";
        public static readonly String ServicesItem = "{3F10D5CB-044E-4981-AAEB-30903B76C304}";
        public static readonly String BuidingsFolder = "{4731A63F-BFED-4A7B-8650-1ED611731530}";
        public static readonly String T14ShopDetailsTemplate = "{91A88069-9A0E-48B7-9509-BDE830A54D0E}";
        public static readonly String ShoppingCategory = "{FB0C20EF-8DD8-4233-A9E8-3D1536412DB7}";
        public static readonly String DiningCategory = "{26C999E2-4351-4F83-9201-5C5662D9FDC5}";
        public static readonly String ShoppingCategoryObject = "{753658B3-59D8-4DAA-8FC8-CFE38C0EFD20}";
        public static readonly String AllByBrandsPage = "{670F29E6-B282-4234-B139-E8F74AD95295}";
        public static readonly String AllByBuildingsPage = "{3FAF2834-AF45-4CD7-BF22-6EEBCA0894C1}";
        public static readonly String ShoppingSubCategoryPageObject = "{11D18D8C-A8E3-4A15-A295-DAE23894A0AE}";
        public static readonly String CategoryObjectTemplate = "{753658B3-59D8-4DAA-8FC8-CFE38C0EFD20}";
        public static readonly String SlideObjectTemplate = "{FFC1447C-5F42-4419-AEF4-6175FB69507C}";
        public static readonly String ButtomSlideObjectTemplate = "{BCC31564-C738-41D5-ABA5-FD436557DF85}";
        public static readonly String ImageTextObjectTemplate = "{CFE5379F-402F-4FB5-A1C5-0E42CA464BC1}";
        public static readonly String NewsletterSignUpPage = "{0610C5FA-FA5E-415A-8D8E-7DAE9445C1E3}";
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
        public static readonly String ReferenceObjectTemplate = "{7C9B7BB1-1F6A-46A4-B705-7D29296B30DA}";
        public static readonly String T4PageTemplate = "{4CDF5CD9-40F0-4F0B-A095-7773CE6CF582}";
        public static readonly String ArticleObject = "{E0472501-0925-4F7F-A5BC-84F0ECA1B7F8}";
        public static readonly String RelatedItemFolder = "{5451FE7F-09B6-4620-AE8B-5B4A3610851E}";
        public static readonly String T27Page = "{A46D7A20-B29F-4C4F-8922-0B70555CD6BC}";
        public static readonly String T23PageTemplate = "{2EE73BEB-F8DA-4F35-BB46-C1B0B8AC3945}";
        public static readonly String T23PageCDTemplate = "{2F6A4A9C-C7DC-439D-8310-3A2EF0617158}";
        public static readonly String T15PageTemplate = "{D931A074-6489-494D-ABA7-AD15032CF10C}";
        public static readonly String T22PageTemplate = "{7B7A336C-48D6-4CD7-BC35-A4593F16F2D0}";
        public static readonly String PageNotFoundItem = "{74CD14D9-357D-4514-AE09-744348A4F6E0}";
        public static readonly String T35GetDirectionPage = "{AE130080-B8B6-4D9E-B863-C88DC329B657}";
        public static readonly String T35SurroundingPage = "{54824316-017A-4B94-AD8E-F636473B510A}";
        public static readonly String T36PageTemplate = "{C3C40A55-7B62-4BCE-81E8-E097F5F30689}";
        public static readonly String ContactUsPage = "{5EC2DEF7-B8CE-45CD-96CD-9967773B5808}";
        public static readonly String ThankYouPage = "{C6045FFE-5883-49F7-AC6E-653919640DDB}";
        public static readonly String EmailSignUpThankYouPage = "{1CA862DB-1C2C-49BC-8775-C4B44E20D358}";
        public static readonly String ConciergePage = "{7C88DA46-5C88-465B-8F12-BBEC6991D8CB}";
        public static readonly String MonthlyExclusivePage = "{923DBE58-71DE-4B27-B0BA-C5DEF3D301B2}";
        public static readonly String InterestsFolder = "{840B3FE7-8EEA-4D6D-9797-4B799BDDF1A8}";
        //search template
        public static readonly String SearchResultsPage = "{4452D078-3946-43F3-9B42-84000F96BF5A}";
        public static readonly String HotelSlideObject = "{93B5F609-2F05-42E3-ABB2-9D903169CF9D}";
        public static readonly String ExperienceCentral = "{E973BDE0-11BC-4DD9-B949-08501F4D9353}";
        public static readonly String T25PageTemplate = "{6837F6B8-D9D4-4693-85E2-3F1001C77B6A}";
        public static readonly String ShoppingPageObject = "{AA7E76BD-BCAD-4597-9855-0493AABF8DAF}";
        public static readonly String PageObject = "{E04426CD-2C71-48F8-A1CA-C507C2CAB178}";
        public static readonly String ExperienceTypeFolder = "{90A7869E-8AF3-4DEE-BD71-7AE422931D30}";
        public static readonly String ShoppingFilterType = "{3D3F4372-81D0-422E-BA25-5BB9B3A01B96}";
        public static readonly String LandmarkMagazineFolder = "{3316BC5E-C1E1-4D24-AD86-4BC25D214996}";
        public static readonly String MagazineCategoryPage = "{46D8EC6C-7FE1-4568-8AEA-3E6DE52A20F4}";
        public static readonly String LandmarkMaganizePage = "{812D4614-C77D-4B0A-9729-D2FB3D830786}";
        public static readonly String SlideFolderTemplate = "{EEEB8813-EBD2-415E-B4F0-72B52DDEF18E}";

        public static readonly String TwoImageColumns = "{C9F47999-9F13-42F2-9AC5-C5DB6FCF4A69}";
        public static readonly String TextColumn = "{8125C406-0B8B-4A8B-8722-7F6156E285F8}";
        public static readonly String OneImage = "{16F1704D-42DB-450A-829C-193F500489E0}";
        public static readonly String ImageSlider = "{DE5C1439-8A10-4B6F-8FE5-6AA7CD9C5526}";
        public static readonly String Tab = "{8D48118F-0385-48B6-A944-F615A3A610BC}";

        public static readonly String ContactSuccessPage = "{C6045FFE-5883-49F7-AC6E-653919640DDB}";
        public static readonly String EventTypeArticle = "{0799CC76-D623-49FD-8025-8E9358A13B62}";
        public static readonly string AboutHotelFolder = "{ECC8D83D-3EDD-46DB-A4AB-0E22806BBE3A}";
        public static readonly string VideoObject = "{82C6EA0F-D0F2-4F40-BA8A-F5E39A10D5B5}";
        public static readonly string ContactUsFormSocailFolder = "{85E16816-67F6-4F3E-AEB6-6BF577B35486}";
        public static readonly string ExlusivesCategoryFolder = "{9E22AA2A-730D-4DB6-9787-5A16E308A91A}";
        public static readonly string DiningByBrandsItem = "{2A670EF7-757F-4FCB-89A6-973DA8AE7DBF}";
        public static readonly string ConciergeDeskObject = "{459A62DB-7A18-4A16-8337-32D6D50931C8}";

        // Items
        public static readonly string LandmarkHomeItem = "{497220CE-7763-4A17-9D3F-4A6DBF1B8CDB}";
        public static readonly string SearchPlaceHolderItem = "{ED07CD50-68B7-4E7A-BE15-F161360F4E07}";
        public static readonly string LandmarkConfigItem = "{EE7EA867-0191-406D-8B84-A28B236CD725}";
        public static readonly string FooterNavigationItem = "{5B9F3F5E-A640-4D05-8CF6-3A4591751CA9}";
        public static readonly string ScrollToBeginGuidItem = "{6EA65B2A-282C-412A-8033-536E855F630F}";
        public static readonly string TopGuidItem = "{07F2749E-EAF3-4ADB-901F-E0454763D0DF}";
        public static readonly string LanguageFolderGuidItem = "{6BABA812-3842-453E-8324-F863E699A92A}";
        public static readonly string ShareThisGuidItem = "{A54A3CE6-2BEC-47CF-A63F-4A2F964C6320}";
        public static readonly string SeeAllBrandsGuidItem = "{D380FDA5-C54A-48D8-B246-21571AC525B7}";
        public static readonly string ListingGuidItem = "{DAEE85AD-9210-48C8-8E29-F7CADB157ECC}";
        public static readonly string AlsoInterestedInGuidItem = "{A6F6E24F-8844-4588-9FE2-0765E5851963}";
        public static readonly string AllBrandsGuidItem = "{4E305EBF-9B1D-4EEA-AAA7-59CD1B3DD3D5}";
        public static readonly string ByBrandsGuidItem = "{23C8C729-14B1-45B9-9557-404516153453}";
        public static readonly string ByBuidingsGuidItem = "{7B87FE4A-D9B2-4451-BB6D-441B01B1462A}";
        public static readonly string GoGuidItem = "{EB7EDF9B-07D2-4CD9-8B21-AF7A15800F98}";
        public static readonly string GoToGuidItem = "{6A742FAE-6615-4BE5-A8A9-4471DFDC60E5}";
        public static readonly string SeeAlsoGuidItem = "{E9CD2E74-E53B-4711-9DB6-88D3DEFA14FC}";
        public static readonly string ContactItem = "{D6F1BBEB-2483-44F1-A547-68D6AE8E2A51}";
        public static readonly string WhereItem = "{C01AD804-C7C9-4791-8CFC-DA35B6D09179}";
        public static readonly string AlsoOnThisFloorItem = "{6C2FE71F-0F24-49BD-A345-7C98BDE4B249}";
        public static readonly string YouMayAlsoLikeItem = "{23815E63-F3F5-48BE-BBEA-5CA6D8CCFECA}";
        public static readonly string TagsItem = "{B05FA79E-B0AC-4D78-9175-ADA8305B5B5B}";
        public static readonly string FloorPlanItem = "{B6888C19-B10C-4662-8E41-7AC2CCE8419A}";
        public static readonly string ByCategoryGuidItem = "{E7E0A523-FFB0-4842-9DD0-7047B1F313B9}";
        public static readonly string RealatedArticlesItem = "{70032C39-DB3A-42F1-91FC-9D85BE84A3F7}";
        public static readonly string MobileAudioGuideItem = "{DD2597D1-42CF-4A71-BD93-8A05851B91B9}";
        public static readonly string ListenToCommentaryItem = "{55FAB28E-6000-4382-833A-4A29C8CA38B0}";
        public static readonly string LocationItem = "{944602F7-94FA-4927-8D72-65326C2F9C32}";
        public static readonly string AlsoInItem = "{0F082781-8037-4643-96F6-88104B58539A}";
        public static readonly string ByThisArtistItem = "{B8DBA1C5-3950-4A13-A460-7C7A1795B682}";
        public static readonly string ViewByItem = "{79222D4A-7956-4441-9D67-B080494D69BC}";
        public static readonly string ArtistItem = "{66B7043C-69C4-4212-A719-F4CE6EBCCB19}";
        public static readonly string MoreItem = "{30811B28-1ECB-4137-B8BF-B962FF16D7E7}";
        public static readonly string OfficialWebsiteItem = "{86B65A99-F8BE-42C1-A50F-A346ACFA9A6D}";
        public static readonly string OpenningHoursItem = "{86EDB6E7-348D-4F43-AE03-3F0EA2F937AC}";
        public static readonly string DateTimeItem = "{6DABDA46-E90A-4BEA-AF61-1B9A385C31BF}";
        public static readonly string EnquiryItem = "{C236AA00-30F1-43BB-A426-C477DE3F264D}";
        public static readonly string VenueItem = "{19EF3D69-3DDC-4A84-A1F7-F93E2098929D}";
        public static readonly string WebsiteItem = "{AEC15D0C-B92E-47D7-991C-EE96110E609A}";
        public static readonly string DiscoverMoreItem = "{0BA79670-6D57-473C-B495-1A01DB6CA2CA}";
        public static readonly string AlsoHappenInLandmarkItem = "{F00CB9D5-2A9A-4C42-860A-4B0476B41C6F}";
        public static readonly string FilterByItem = "{C477E1B6-E381-43A7-9B3F-E5A6F15D9A74}";
        public static readonly string PhoneItem = "{4AAF25F5-0A1C-45AA-B81D-B27AD46F37C2}";
        public static readonly string StoreDetailsItem = "{58C95FD5-61B2-4DAE-9C5B-6BAE61A2D98D}";
        public static readonly string AddressItem = "{86E8B0C2-6C91-45D9-949C-DE5C1CB64ED3}";
        public static readonly string FindByBrandItem = "{88028182-7209-4EBB-8CBA-1E6BBE40FCCF}";
        public static readonly string EmailItem = "{E3B50ED3-8CDC-41DA-A5D4-176A55CBA63A}";
        public static readonly string ShowAllItem = "{2F9D72B2-FF66-4302-8ABB-BB5F7C697FE5}";
        public static readonly string SeeAllExclusiveItem = "{4F5C3F3D-9AB5-424B-9C78-41DC7AEECB8F}";
        public static readonly string SeeAllExclusive = "{2283A3E4-45AE-4164-8C82-6D9F48D0602C}";
        public static readonly string ReadMoreItem = "{738E90D0-A3B0-46A3-8DF4-6FE1825B51D2}";
        public static readonly string LessItem = "{574B48FD-7EDF-4621-887B-780E276EB093}";
        public static readonly string AllTextItem = "{48098CED-9711-4F65-92B3-BB8810A11806}";
        public static readonly string ShareItem = "{2224057F-0DA3-4D7E-9089-99CB8CC77C0F}";
        public static readonly string LearnMoreItem = "{3ADB531C-1F86-4963-859D-9525EE4D833A}";
        public static readonly string ViewMapItem = "{615AE910-37B4-44C8-B550-32DAA8CCF66B}";
        public static readonly string FaxItem = "{4C157DD6-6AF2-4112-B610-AFFC84C6CCF1}";
        public static readonly string TelephoneItem = "{0B3DA006-CBA6-44CA-9421-88965D431F2F}";
        public static readonly string DiscoverLandmarkStoriesItem = "{5731F621-01CE-4596-9DA3-55A231D0D21F}";
        public static readonly string SortAndGoToItem = "{A67F7267-0402-4356-9EDF-5F1016C67F15}";
        public static readonly string SeeMoreItem = "{0BA79670-6D57-473C-B495-1A01DB6CA2CA}";
        public static readonly string MagezineItem = "{8B359693-D433-4F44-98F8-9CEE5FA9B72F}";
        public static readonly string ListenToCommentaryAltItem = "{36C61E1D-7652-4DD4-8CCA-DDF1067D89D3}";
        public static readonly string RelatedArticleItem = "{9E9393AC-EDD4-4F2B-BDC8-A95A7400FA64}";
        public static readonly string LegalPrivacyItem = "{4F4D0D9B-FE4D-4BFA-A7BC-2F0FE98DFAA1}";
        public static readonly string SeeMoreGlossaryItem = "{4C26DD8F-E35D-4723-A63D-EBC091B5517B}";
        public static readonly string SeeLessGlossaryItem = "{ECE9A631-616F-4180-A886-8DAE0A8C7ECE}";
        public static readonly string PleaseSelectItem = "{F40D2AD4-00D1-49BB-AFF5-EDCF62DA9668}";
        public static readonly string ArtItem = "{AB0C8815-0927-4C82-9430-D9FFC89A5C5E}";
        public static readonly string EmailOnlyItem = "{F21D5148-ECCD-43FC-AE07-184D0847F730}";
        public static readonly string EmailPostalAddressItem = "{45E8443F-D90C-42BB-B34C-F4A6BEF2D17E}";
        public static readonly string RelatedStoriesItem = "{1966B9BC-18BE-4000-BAD8-DDA8C36D304A}";
        public static readonly string RelatedBrandItem = "{ED1B7841-C842-4A32-AA89-45FF75BA8865}";
        public static readonly string SeeAllRestaurantItem = "{57D52748-BF11-4228-83E4-AAF3FFB1F8CF}";

    }
}
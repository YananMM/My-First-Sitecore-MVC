using Sitecore.Data.Items;
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
        public static readonly String OverviewPage = "{7C88DA46-5C88-465B-8F12-BBEC6991D8CB}";
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
    }
}
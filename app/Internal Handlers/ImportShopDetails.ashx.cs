using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LINQtoCSV;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Hosting;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Publishing;
using Sitecore.SecurityModel;
using Landmark.Classes;
using Landmark.Helper;
using Landmark.Models;
using Sitecore.Data.Fields;
using Sitecore.Links;
using Sitecore.Mvc.Extensions;


namespace Landmark.Internal_Handlers
{
    public class CsvShopDetail
    {
        [CsvColumn(FieldIndex = 1)]
        public string NameEn { get; set; }
        [CsvColumn(FieldIndex = 2)]
        public string NameTc { get; set; }
        [CsvColumn(FieldIndex = 3)]
        public string NameSc { get; set; }
        [CsvColumn(FieldIndex = 4)]
        public string HeadingEn { get; set; }
        [CsvColumn(FieldIndex = 5)]
        public string HeadingTc { get; set; }
        [CsvColumn(FieldIndex = 6)]
        public string HeadingSc { get; set; }
        [CsvColumn(FieldIndex = 7)]
        public string BodyEn { get; set; }
        [CsvColumn(FieldIndex = 8)]
        public string BodyTc { get; set; }
        [CsvColumn(FieldIndex = 9)]
        public string BodySc { get; set; }
        [CsvColumn(FieldIndex = 10)]
        public string AddressEn { get; set; }
        [CsvColumn(FieldIndex = 11)]
        public string AddressTc { get; set; }
        [CsvColumn(FieldIndex = 12)]
        public string AddressSc { get; set; }
        [CsvColumn(FieldIndex = 13)]
        public string TelEn { get; set; }
        [CsvColumn(FieldIndex = 14)]
        public string TelTc { get; set; }
        [CsvColumn(FieldIndex = 15)]
        public string TelSc { get; set; }
        [CsvColumn(FieldIndex = 16)]
        public string OpenEn { get; set; }
        [CsvColumn(FieldIndex = 17)]
        public string OpenTc { get; set; }
        [CsvColumn(FieldIndex = 18)]
        public string OpenSc { get; set; }
        [CsvColumn(FieldIndex = 19)]
        public string Keywords { get; set; }
        [CsvColumn(FieldIndex = 20)]
        public string Categories { get; set; }
        [CsvColumn(FieldIndex = 21)]
        public string Logo { get; set; }
        [CsvColumn(FieldIndex = 22)]
        public string Slider { get; set; }
    }

    /*
     *  1 Name
     *  2 Location/Atrium
     *  3 Location/Princes
     *  4 Location/Alexendra
     *  5 Location/Chater
     *  6 Location/Other/Edinburgh Tower
     *  7 Location/Other/Jardine House
     *  8 Location/Other/Mandarin Oriental Hotel	
     *  9 Location/Other/One Exchange Square	
     * 10 Shop/MEN
     * 11 Shop/MEN/Accessories	
     * 12 Shop/MEN/Active wear	
     * 13 Shop/MEN/Bags	
     * 14 Shop/MEN/Fashion	
     * 15 Shop/MEN/Made to measure	
     * 16 Shop/MEN/Shoes	
     * 17 Shop/MEN/VIP Room	
     * 18 Shop/MEN/LANDMARK MEN
     * 20 Shop/WOMEN	
     * 21 Shop/WOMEN/Accessories	
     * 22 Shop/WOMEN/Active Wear	
     * 23 Shop/WOMEN/Bags	
     * 24 Shop/WOMEN/Cocktail dresses Gowns	
     * 25 Shop/WOMEN/Fashion	
     * 26 Shop/WOMEN/Shoes	
     * 27 Shop/WOMEN/Weddings	
     * 28 Shop/WOMEN/VIP Room	
     * 29 Shop/CHILDREN BABIES	
     * 30 Shop/CHILDREN BABIES/Accessories	
     * 31 Shop/CHILDREN BABIES/Clothing	
     * 32 Shop/CHILDREN BABIES/Shoes
     * 33 Shop/CHILDREN BABIES/Toys	
     * 34 Shop/WELLNESS	
     * 35 Shop/WELLNESS/Fragrance	
     * 36 Shop/WELLNESS/Gym Pool	
     * 37 Shop/WELLNESS/Spa Salon
     * 38 Shop/WELLNESS/Sports Equipment	
     * 39 Shop/WELLNESS/Skin Personal Care Makeup	
     * 40 Shop/HOME	
     * 41 Shop/HOME/Bed Bath	
     * 42 Shop/HOME/Decor	
     * 43 Shop/HOME/Florists	
     * 44 Shop/HOME/Gifts Accessories	
     * 45 Shop/HOME/Kitchen Tableware	
     * 46 Shop/FOOD WINE
     * 47 Shop/FOOD WINE/Chocolatiers Patisseries Cafes	
     * 48 Shop/FOOD WINE/Gourmet Market	
     * 49 Shop/FOOD WINE/Tobacconists
     * 50 Shop/FOOD WINE/Wines Spirits
     * 51 Shop/Gadgets
     * 52 Shop/Gadgets/Accessories
     * 53 Shop/Gadgets/Audio Visual	
     * 54 Shop/Gadgets/Photography	
     * 55 Shop/Books Photos	
     * 56 Shop/Books Photos/Books Newspapers Magazines
     * 57 Shop/Books Photos/Photos Printing
     * 58 Shop/Books Photos/Stationary
     * 59 Shop/Art Antiques	
     * 60 Shop/Art Antiques/Art	
     * 61 Shop/Art Antiques/Antiques	
     * 62 Shop/JEWELLERY WATCHES 
     * 63 Shop/EYEWEAR 
     * 64 Shop/Clothing Care 
     * 65 Shop/OTHER
     */
    public class CsvShopTags
    {
        [CsvColumn(FieldIndex = 1)]
        public string Name { get; set; }
        [CsvColumn(FieldIndex = 2)]
        public string Location_Atrium { get; set; }
        [CsvColumn(FieldIndex = 3)]
        public string Location_Princes { get; set; }
        [CsvColumn(FieldIndex = 4)]
        public string Location_Alexendra { get; set; }
        [CsvColumn(FieldIndex = 5)]
        public string Location_Chater { get; set; }
        [CsvColumn(FieldIndex = 6)]
        public string Location_Other_EdinburghTower { get; set; }
        [CsvColumn(FieldIndex = 7)]
        public string Location_Other_JardineHouse { get; set; }
        [CsvColumn(FieldIndex = 8)]
        public string Location_Other_MandarinOrientalHotel { get; set; }
        [CsvColumn(FieldIndex = 9)]
        public string Location_Other_OneExchangeSquare { get; set; }
        [CsvColumn(FieldIndex = 10)]
        public string Shop_MEN { get; set; }
        [CsvColumn(FieldIndex = 11)]
        public string Shop_MEN_Accessories { get; set; }
        [CsvColumn(FieldIndex = 12)]
        public string Shop_MEN_ActiveWear { get; set; }
        [CsvColumn(FieldIndex = 13)]
        public string Shop_MEN_Bags { get; set; }
        [CsvColumn(FieldIndex = 14)]
        public string Shop_MEN_Fashion { get; set; }
        [CsvColumn(FieldIndex = 15)]
        public string Shop_MEN_MadeToMeasure { get; set; }
        [CsvColumn(FieldIndex = 16)]
        public string Shop_MEN_Shoes { get; set; }
        [CsvColumn(FieldIndex = 17)]
        public string Shop_MEN_VIPRoom { get; set; }
        [CsvColumn(FieldIndex = 18)]
        public string Shop_MEN_LANDMARKMEN { get; set; }
        [CsvColumn(FieldIndex = 19)]
        public string Shop_WOMEN { get; set; }
        [CsvColumn(FieldIndex = 20)]
        public string Shop_WOMEN_Accessories { get; set; }
        [CsvColumn(FieldIndex = 21)]
        public string Shop_WOMEN_ActiveWear { get; set; }
        [CsvColumn(FieldIndex = 22)]
        public string Shop_WOMEN_Bags { get; set; }
        [CsvColumn(FieldIndex = 23)]
        public string Shop_WOMEN_CocktailDressesGowns { get; set; }
        [CsvColumn(FieldIndex = 24)]
        public string Shop_WOMEN_Fashion { get; set; }
        [CsvColumn(FieldIndex = 25)]
        public string Shop_WOMEN_Shoes { get; set; }
        [CsvColumn(FieldIndex = 26)]
        public string Shop_WOMEN_Weddings { get; set; }
        [CsvColumn(FieldIndex = 27)]
        public string Shop_WOMEN_VIPRoom { get; set; }
        [CsvColumn(FieldIndex = 28)]
        public string Shop_CHILDRENBABIES { get; set; }
        [CsvColumn(FieldIndex = 29)]
        public string Shop_CHILDRENBABIES_Accessories { get; set; }
        [CsvColumn(FieldIndex = 30)]
        public string Shop_CHILDRENBABIES_Clothing { get; set; }
        [CsvColumn(FieldIndex = 31)]
        public string Shop_CHILDRENBABIES_Shoes { get; set; }
        [CsvColumn(FieldIndex = 32)]
        public string Shop_CHILDRENBABIES_Toys { get; set; }
        [CsvColumn(FieldIndex = 33)]
        public string Shop_WELLNESS { get; set; }
        [CsvColumn(FieldIndex = 34)]
        public string Shop_WELLNESS_Fragrance { get; set; }
        [CsvColumn(FieldIndex = 35)]
        public string Shop_WELLNESS_GymPool { get; set; }
        [CsvColumn(FieldIndex = 36)]
        public string Shop_WELLNESS_SpaSalon { get; set; }
        [CsvColumn(FieldIndex = 37)]
        public string Shop_WELLNESS_SportsEquipment { get; set; }
        [CsvColumn(FieldIndex = 38)]
        public string Shop_WELLNESS_SkinPersonalCareMakeup { get; set; }
        [CsvColumn(FieldIndex = 39)]
        public string Shop_HOME { get; set; }
        [CsvColumn(FieldIndex = 40)]
        public string Shop_HOME_BedBath { get; set; }
        [CsvColumn(FieldIndex = 41)]
        public string Shop_HOME_Decor { get; set; }
        [CsvColumn(FieldIndex = 42)]
        public string Shop_HOME_Florists { get; set; }
        [CsvColumn(FieldIndex = 43)]
        public string Shop_HOME_GiftsAccessories { get; set; }
        [CsvColumn(FieldIndex = 44)]
        public string Shop_HOME_KitchenTableware { get; set; }
        [CsvColumn(FieldIndex = 45)]
        public string Shop_FOODWINE { get; set; }
        [CsvColumn(FieldIndex = 46)]
        public string Shop_FOODWINE_ChocolatiersPatisseriesCafes { get; set; }
        [CsvColumn(FieldIndex = 47)]
        public string Shop_FOODWINE_GourmetMarket { get; set; }
        [CsvColumn(FieldIndex = 48)]
        public string Shop_FOODWINE_Tobacconists { get; set; }
        [CsvColumn(FieldIndex = 49)]
        public string Shop_FOODWINE_WinesSpirits { get; set; }
        [CsvColumn(FieldIndex = 50)]
        public string Shop_Gadgets { get; set; }
        [CsvColumn(FieldIndex = 51)]
        public string Shop_Gadgets_Accessories { get; set; }
        [CsvColumn(FieldIndex = 52)]
        public string Shop_Gadgets_AudioVisual { get; set; }
        [CsvColumn(FieldIndex = 53)]
        public string Shop_Gadgets_Photography { get; set; }
        [CsvColumn(FieldIndex = 54)]
        public string Shop_BooksPhotos { get; set; }
        [CsvColumn(FieldIndex = 55)]
        public string Shop_BooksPhotos_BooksNewspapersMagazines { get; set; }
        [CsvColumn(FieldIndex = 56)]
        public string Shop_BooksPhotos_PhotosPrinting { get; set; }
        [CsvColumn(FieldIndex = 57)]
        public string Shop_BooksPhotos_Stationary { get; set; }
        [CsvColumn(FieldIndex = 58)]
        public string Shop_ArtAntiques { get; set; }
        [CsvColumn(FieldIndex = 59)]
        public string Shop_ArtAntiques_Art { get; set; }
        [CsvColumn(FieldIndex = 60)]
        public string Shop_ArtAntiques_Antiques { get; set; }
        [CsvColumn(FieldIndex = 61)]
        public string Shop_JEWELLERYWATCHES { get; set; }
        [CsvColumn(FieldIndex = 62)]
        public string Shop_EYEWEAR { get; set; }
        [CsvColumn(FieldIndex = 63)]
        public string Shop_ClothingCare { get; set; }
        [CsvColumn(FieldIndex = 64)]
        public string Shop_OTHER { get; set; }
    }

    /*
     *  1 Name
     *  2 Dining/Michelin Star Restaurants	
     *  3 Dining/Chinese	
     *  4 Dining/Japanese	
     *  5 Dining/Asian	
     *  6 Dining/French	
     *  7 Dining/Italian	
     *  8 Dining/British	
     *  9 Dining/Western	
     * 10 Dining/International	
     * 11 Dining/Casual Dining	
     * 12 Dining/Fine Dining	
     * 13 Dining/Cafes	
     * 14 Dining/Afternoon Tea	
     * 15 Dining/Cocktail Bars	
     * 16 Location/Atrium	
     * 17 Location/Princes	
     * 18 Location/Alexendra	
     * 19 Location/Chater	
     * 20 Location/Other/Edinburgh Tower	
     * 21 Location/Other/Jardine House	
     * 22 Location/Other/Mandarin Oriental Hotel	
     * 23 Location/Other/One Exchange Square
     * 24 Location/Other/Two Exchange Square
     */
    public class CsvDineTags
    {
        [CsvColumn(FieldIndex = 1)]
        public string Name { get; set; }
        [CsvColumn(FieldIndex = 2)]
        public string Dining_MichelinStarRestaurants { get; set; }
        [CsvColumn(FieldIndex = 3)]
        public string Dining_Chinese { get; set; }
        [CsvColumn(FieldIndex = 4)]
        public string Dining_Japanese { get; set; }
        [CsvColumn(FieldIndex = 5)]
        public string Dining_Asian { get; set; }
        [CsvColumn(FieldIndex = 6)]
        public string Dining_French { get; set; }
        [CsvColumn(FieldIndex = 7)]
        public string Dining_Italian { get; set; }
        [CsvColumn(FieldIndex = 8)]
        public string Dining_British { get; set; }
        [CsvColumn(FieldIndex = 9)]
        public string Dining_Western { get; set; }
        [CsvColumn(FieldIndex = 10)]
        public string Dining_International { get; set; }
        [CsvColumn(FieldIndex = 11)]
        public string Dining_CasualDining { get; set; }
        [CsvColumn(FieldIndex = 12)]
        public string Dining_FineDining { get; set; }
        [CsvColumn(FieldIndex = 13)]
        public string Dining_Cafes { get; set; }
        [CsvColumn(FieldIndex = 14)]
        public string Dining_AfternoonTea { get; set; }
        [CsvColumn(FieldIndex = 15)]
        public string Dining_CocktailBars { get; set; }
        [CsvColumn(FieldIndex = 16)]
        public string Location_Atrium { get; set; }
        [CsvColumn(FieldIndex = 17)]
        public string Location_Princes { get; set; }
        [CsvColumn(FieldIndex = 18)]
        public string Location_Alexendra { get; set; }
        [CsvColumn(FieldIndex = 19)]
        public string Location_Chater { get; set; }
        [CsvColumn(FieldIndex = 20)]
        public string Location_Other_EdinburghTower { get; set; }
        [CsvColumn(FieldIndex = 21)]
        public string Location_Other_JardineHouse { get; set; }
        [CsvColumn(FieldIndex = 22)]
        public string Location_Other_MandarinOrientalHotel { get; set; }
        [CsvColumn(FieldIndex = 23)]
        public string Location_Other_OneExchangeSquare { get; set; }
        [CsvColumn(FieldIndex = 24)]
        public string Location_Other_TwoExchangeSquare { get; set; }
    }

    public class CsvShopXY
    {
        [CsvColumn(FieldIndex = 1)]
        public string Filename { get; set; }
        [CsvColumn(FieldIndex = 2)]
        public string Name { get; set; }
        [CsvColumn(FieldIndex = 3)]
        public string Address { get; set; }
        [CsvColumn(FieldIndex = 4)]
        public string ShopLoc { get; set; }
        [CsvColumn(FieldIndex = 5)]
        public string MapId { get; set; }
        [CsvColumn(FieldIndex = 6)]
        public string JpgW { get; set; }
        [CsvColumn(FieldIndex = 7)]
        public string JpgH { get; set; }
        [CsvColumn(FieldIndex = 8)]
        public string PX { get; set; }
        [CsvColumn(FieldIndex = 9)]
        public string PY { get; set; }
        [CsvColumn(FieldIndex = 10)]
        public string X { get; set; }
        [CsvColumn(FieldIndex = 11)]
        public string Y { get; set; }
        [CsvColumn(FieldIndex = 12)]
        public string ShopPage { get; set; }
        [CsvColumn(FieldIndex = 13)]
        public string MapInShopPage { get; set; }
        [CsvColumn(FieldIndex = 14)]
        public string FloorGuide { get; set; }
    }

    public class CsvArtXY
    {
        [CsvColumn(FieldIndex = 1)]
        public string Filename { get; set; }
        [CsvColumn(FieldIndex = 2)]
        public string Name { get; set; }
        [CsvColumn(FieldIndex = 3)]
        public string X { get; set; }
        [CsvColumn(FieldIndex = 4)]
        public string Y { get; set; }
    }

    /// <summary>
    /// Summary description for ImportShopDetails
    /// </summary>
    public class ImportShopDetails : IHttpHandler
    {
        const string LogoDir = "/sitecore/media library/Images/Landmark/Brands/Logo/";
        const string SliderDir = "/sitecore/media library/Images/Landmark/Shopping/ShopDetail/Slider/";
        private readonly Database _webDb = Factory.GetDatabase("web");
        private readonly Database _masterDb = Factory.GetDatabase("master");
        private readonly Item _shopTemplate, _folderTemplate, _sliderImageTemplate;
        

        public ImportShopDetails()
        {
            _shopTemplate = _masterDb.GetItem("/sitecore/templates/User Defined/Landmark/Page/T14 Page");
            _folderTemplate = _masterDb.GetItem("/sitecore/templates/User Defined/Landmark/Object/Slide Folder");
            _sliderImageTemplate = _masterDb.GetItem("/sitecore/templates/User Defined/Landmark/Object/Slide Object ");

        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Import Shop Details");

            
            var details =
                new CsvContext().Read<CsvShopDetail>(@SitecoreItems.LandmarkConfigItem.Fields["CSV Folder"].Value + "shop-details.txt", new CsvFileDescription
                {
                    SeparatorChar = '\t',
                    FirstLineHasColumnNames = false,
                    EnforceCsvColumnAttribute = true
                }).Skip(1);
            var shopTags =
            new CsvContext().Read<CsvShopTags>(@SitecoreItems.LandmarkConfigItem.Fields["CSV Folder"].Value + "shop-tags.txt", new CsvFileDescription
            {
                SeparatorChar = '\t',
                FirstLineHasColumnNames = false,
                EnforceCsvColumnAttribute = true
            }).Skip(1).Where(t => !string.IsNullOrEmpty(t.Name)).ToList();
            var dineTags =
            new CsvContext().Read<CsvDineTags>(@SitecoreItems.LandmarkConfigItem.Fields["CSV Folder"].Value + "dine-tags.txt", new CsvFileDescription
            {
                SeparatorChar = '\t',
                FirstLineHasColumnNames = false,
                EnforceCsvColumnAttribute = true
            }).Skip(1).Where(t => !string.IsNullOrEmpty(t.Name)).ToList();
            var shopXYs =
               new CsvContext().Read<CsvShopXY>(@SitecoreItems.LandmarkConfigItem.Fields["CSV Folder"].Value + "shop-xy.txt", new CsvFileDescription
               {
                   SeparatorChar = '\t',
                   FirstLineHasColumnNames = false,
                   EnforceCsvColumnAttribute = true
               }).Skip(1).Where(t => !string.IsNullOrEmpty(t.Name) && !string.IsNullOrEmpty(t.Filename)).ToList();
            var artXYs =
               new CsvContext().Read<CsvArtXY>(@SitecoreItems.LandmarkConfigItem.Fields["CSV Folder"].Value+"artpiece-xy.txt", new CsvFileDescription
               {
                   SeparatorChar = '\t',
                   FirstLineHasColumnNames = false,
                   EnforceCsvColumnAttribute = true
               }).Skip(1).Where(t => !string.IsNullOrEmpty(t.Name) && !string.IsNullOrEmpty(t.Filename)).ToList();

            var shopFolder = _masterDb.GetItem("/sitecore/content/Home/Landmark/Shopping");
            var diningFolder = _masterDb.GetItem("/sitecore/content/Home/Landmark/Dining");
            var artFolder = _masterDb.GetItem("/sitecore/content/Home/Landmark/Around Central/Landmark Art Tour");
            var logoFolder = _masterDb.GetItem(LogoDir);
            var sliderFolder = _masterDb.GetItem(SliderDir);

            foreach (var detail in details)
            {
                using (new SecurityDisabler())
                {
                    var isShop = shopTags.Any(t => t.Name == detail.NameEn);
                    var isDine = dineTags.Any(t => t.Name == detail.NameEn);
                    var itemName = GenItemName(detail.NameEn);
                    var shopItem = shopFolder.Children.FirstOrDefault(i => i.Name == itemName)
                                   ?? diningFolder.Children.FirstOrDefault(i => i.Name == itemName)
                                   ??
                                   _masterDb.CreateItemPath(
                                       "/sitecore/content/Home/Landmark/" + (isShop ? "Shopping/" : "Dining/") +
                                       itemName, _shopTemplate);

                    foreach (var language in shopItem.Languages)
                    {
                        var version = _masterDb.GetItem(shopItem.ID, language);

                        if (version.Versions.Count == 0)
                        {
                            version = version.Versions.AddVersion();
                        }

                        using (new EditContext(version))
                        {
                            version.Editing.BeginEdit();
                            switch (language.Name)
                            {
                                case "en":
                                    version.Fields["Brand Title"].SetValue(detail.NameEn,true);
                                    version.Fields["Page Title"].SetValue( detail.NameEn,true);
                                    version.Fields["Content Title"].SetValue( detail.HeadingEn,true);

                                    version.Fields["Meta Keywords"].SetValue( detail.Keywords,true);
                                    version.Fields["Meta Description"].SetValue( detail.HeadingEn,true);
                                    version.Fields["Social Share Title"].SetValue( detail.NameEn,true);
                                    version.Fields["Social Share Description"].SetValue( detail.HeadingEn,true);

                                    version.Fields["Content Description"].SetValue(  detail.BodyEn,true);
                                    version.Fields["Page Content"].SetValue(  detail.BodyEn,true);

                                    version.Fields["Area"].SetValue(  detail.AddressEn,true);
                                    version.Fields["Description"].SetValue( detail.HeadingEn,true);
                                    version.Fields["Openning Hours"].SetValue(  detail.OpenEn,true);
                                    version.Fields["Contact"].SetValue( detail.TelEn,true);
                                    //version.Fields["Official Website"].Value = ???;
                                    break;
                                case "zh-CN":
                                    version.Fields["Brand Title"].SetValue(  detail.NameSc,true);
                                    version.Fields["Page Title"].SetValue(  detail.NameSc,true);
                                    version.Fields["Content Title"].SetValue(  detail.HeadingSc,true);

                                    version.Fields["Meta Keywords"].SetValue( detail.Keywords,true);
                                    version.Fields["Meta Description"].SetValue(  detail.HeadingSc,true);
                                    version.Fields["Social Share Title"].SetValue(  detail.NameSc,true);
                                    version.Fields["Social Share Description"].SetValue(  detail.HeadingSc,true);

                                    version.Fields["Content Description"].SetValue(  detail.BodySc,true);
                                    version.Fields["Page Content"].SetValue(  detail.BodySc,true);

                                    version.Fields["Area"].SetValue(detail.AddressSc,true);
                                    version.Fields["Description"].SetValue( detail.HeadingSc,true);
                                    version.Fields["Openning Hours"].SetValue(detail.OpenSc,true);
                                    version.Fields["Contact"].SetValue(detail.TelSc,true);
                                    break;
                                case "zh-HK":
                                    version.Fields["Brand Title"].SetValue( detail.NameTc,true);
                                    version.Fields["Page Title"].SetValue( detail.NameTc,true);
                                    version.Fields["Content Title"].SetValue( detail.HeadingTc,true);

                                    version.Fields["Meta Keywords"].SetValue(detail.Keywords,true);
                                    version.Fields["Meta Description"].SetValue( detail.HeadingTc,true);
                                    version.Fields["Social Share Title"].SetValue( detail.NameTc,true);
                                    version.Fields["Social Share Description"].SetValue(detail.HeadingTc,true);

                                    version.Fields["Content Description"].SetValue( detail.BodyTc,true);
                                    version.Fields["Page Content"].SetValue( detail.BodyTc,true);

                                    version.Fields["Area"].SetValue( detail.AddressTc,true);
                                    version.Fields["Description"].SetValue( detail.HeadingTc,true);
                                    version.Fields["Openning Hours"].SetValue( detail.OpenTc,true);
                                    version.Fields["Contact"].SetValue(detail.TelTc, true);
                                    break;
                            }

                            //((CheckboxField) version.Fields["Is Shown In Navigation"]).Checked = true;
                            //((CheckboxField)version.Fields["Is Shown In Breadcrum"]).Checked = true;
                            // Brand Image
                            Item brandImage = logoFolder.GetChildren().FirstOrDefault(i => i.DisplayName == detail.Logo);
                            if (brandImage != null)
                            {
                                SetImageFieldValue(brandImage.ID.ToString(), version, "Brand Image");
                            }
                            // Slider Image
                            var mySliderFolder = version.GetChildren()
                                .FirstOrDefault(i => i.DisplayName == "Shop Main Page") ??
                                                 _masterDb.CreateItemPath(version.Paths.FullPath + "/Shop Main Page",
                                                     _folderTemplate);
                            version.Add("Shop Main Page",
                                new TemplateID(new ID("{EEEB8813-EBD2-415E-B4F0-72B52DDEF18E}")));
                            var sliderImages = sliderFolder.Children.Where(i => i.DisplayName.StartsWith(detail.Slider));
                            foreach (var image in sliderImages)
                            {
                                /*if (mySliderFolder.Children.Any(i => i.DisplayName == image.DisplayName))
                                    continue;*/
                                var newImage = mySliderFolder.Children.FirstOrDefault(i => i.DisplayName == image.DisplayName) ??
                                    _masterDb.CreateItemPath(mySliderFolder.Paths.FullPath + "/" + image.DisplayName,
                                        _sliderImageTemplate);

                                var ImgVersion = _masterDb.GetItem(newImage.ID, language);

                                if (ImgVersion.Versions.Count == 0)
                                {
                                    ImgVersion = ImgVersion.Versions.AddVersion();
                                }
                                if (language.Name == "en")
                                {
                                    SetImageFieldValue(image.ID.ToString(), ImgVersion, "Slide Image");
                                }
                                mySliderFolder.Add(ImgVersion.DisplayName, new TemplateID(new ID("{FFC1447C-5F42-4419-AEF4-6175FB69507C}")));
                            }

                            // following are shared values, fill "en" version only
                            if (language.Name != "en")
                                continue;
                            // Tags
                            var tags = new List<string>();
                            if (isShop)
                            {
                                #region shop tags

                                var shopTag = shopTags.Single(t => t.Name == detail.NameEn);

                                if (shopTag.Location_Atrium != null)
                                {
                                    if (shopTag.Location_Atrium != null && shopTag.Location_Atrium != null && shopTag.Location_Atrium.StartsWith("Y"))
                                    {
                                        // TODO: Location_Atrium
                                        tags.Add("{C034122F-A68F-4E3C-827E-C1898533C08E}");
                                    }
                                }
                                if (shopTag.Location_Princes != null)
                                {
                                    if (shopTag.Location_Princes != null && shopTag.Location_Princes != null && shopTag.Location_Princes.StartsWith("Y"))
                                    {
                                        // TODO: Location_Princes
                                        tags.Add("{7349B461-564F-4A14-B43D-7B4F6FF1E0D6}");
                                    }
                                }
                                if (shopTag.Location_Alexendra != null)
                                {
                                    if (shopTag.Location_Alexendra != null && shopTag.Location_Alexendra.StartsWith("Y"))
                                    {
                                        // TODO: Location_Alexendra
                                        tags.Add("{B597804A-C4D7-44FB-9FAC-490726EEB3D9}");
                                    }
                                }
                                if (shopTag.Location_Chater != null)
                                {
                                    if (shopTag.Location_Chater != null && shopTag.Location_Chater.StartsWith("Y"))
                                    {
                                        // TODO: Location_Chater
                                        tags.Add("{37C1F81A-7605-4539-808B-C4B487FDB8DE}");
                                    }
                                }
                                if (shopTag.Location_Other_EdinburghTower != null)
                                {
                                    if (shopTag.Location_Other_EdinburghTower != null && shopTag.Location_Other_EdinburghTower.StartsWith("Y"))
                                    {
                                        // TODO: Location_Other_EdinburghTower
                                        tags.Add("{132E7671-084F-4DFD-9173-FEFB6A67FBAA}");
                                    }
                                }
                                if (shopTag.Location_Other_JardineHouse != null)
                                {
                                    if (shopTag.Location_Other_JardineHouse != null && shopTag.Location_Other_JardineHouse.StartsWith("Y"))
                                    {
                                        // TODO: Location_Other_JardineHouse
                                        tags.Add("{1DA4C84C-5353-4961-9C01-4A480E81C865}");
                                    }
                                }
                                if (shopTag.Location_Other_MandarinOrientalHotel != null && shopTag.Location_Other_MandarinOrientalHotel.StartsWith("Y"))
                                {
                                    // TODO: Location_Other_MandarinOrientalHotel
                                    tags.Add("{6ADD337A-5273-4504-AD72-8C601D69C6F9}");
                                }
                                
                                if (shopTag.Location_Other_OneExchangeSquare != null)
                                {
                                    if (shopTag.Location_Other_OneExchangeSquare != null && shopTag.Location_Other_OneExchangeSquare.StartsWith("Y"))
                                    {
                                        // TODO: Location_Other_OneExchangeSquare
                                        tags.Add("{838D33D8-9AC8-4369-AB18-9470EADCD49B}");
                                    }
                                }
                                else if (shopTag.Shop_MEN != null && shopTag.Shop_MEN.StartsWith("Y"))
                                {
                                    tags.Add("{385A8E74-CC78-4C89-A66D-1021D757C01C}");
                                }
                                ////
                                else if (shopTag.Shop_MEN != null && shopTag.Shop_MEN.StartsWith("Y"))
                                {
                                    tags.Add("{385A8E74-CC78-4C89-A66D-1021D757C01C}");
                                }
                                else if (shopTag.Shop_MEN != null && shopTag.Shop_MEN.StartsWith("Y"))
                                {
                                    tags.Add("{385A8E74-CC78-4C89-A66D-1021D757C01C}");
                                }
                                else if (shopTag.Shop_MEN != null && shopTag.Shop_MEN.StartsWith("Y"))
                                {
                                    tags.Add("{385A8E74-CC78-4C89-A66D-1021D757C01C}");
                                }
                                else if (shopTag.Shop_MEN != null && shopTag.Shop_MEN.StartsWith("Y"))
                                {
                                    tags.Add("{385A8E74-CC78-4C89-A66D-1021D757C01C}");
                                }
                                else if (shopTag.Shop_MEN != null && shopTag.Shop_MEN.StartsWith("Y"))
                                {
                                    tags.Add("{385A8E74-CC78-4C89-A66D-1021D757C01C}");
                                }
                                else if (shopTag.Shop_MEN != null && shopTag.Shop_MEN.StartsWith("Y"))
                                {
                                    tags.Add("{385A8E74-CC78-4C89-A66D-1021D757C01C}");
                                }
                                else if (shopTag.Shop_MEN != null && shopTag.Shop_MEN.StartsWith("Y"))
                                {
                                    tags.Add("{385A8E74-CC78-4C89-A66D-1021D757C01C}");
                                }
                                else if (shopTag.Shop_MEN != null && shopTag.Shop_MEN.StartsWith("Y"))
                                {
                                    tags.Add("{385A8E74-CC78-4C89-A66D-1021D757C01C}");
                                }
                                else if (shopTag.Shop_MEN != null && shopTag.Shop_MEN.StartsWith("Y"))
                                {
                                    tags.Add("{385A8E74-CC78-4C89-A66D-1021D757C01C}");
                                }
                                else if (shopTag.Shop_MEN_Accessories != null && shopTag.Shop_MEN_Accessories.StartsWith("Y"))
                                {
                                    tags.Add("{2C43F9BE-709E-4AC6-857A-C42ECB81B7E2}");
                                }
                                else if (shopTag.Shop_MEN_ActiveWear != null && shopTag.Shop_MEN_ActiveWear.StartsWith("Y"))
                                {
                                    tags.Add("{39F78176-04AD-4110-8AC4-AA392CC79C2E}");
                                }
                                else if (shopTag.Shop_MEN_Bags != null && shopTag.Shop_MEN_Bags.StartsWith("Y"))
                                {
                                    tags.Add("{8DEEBCDA-AC9C-4B0C-AF81-8E493D219713}");
                                }
                                else if (shopTag.Shop_MEN_Fashion != null && shopTag.Shop_MEN_Fashion.StartsWith("Y"))
                                {
                                    tags.Add("{D85FDAB0-F4B3-43A4-B9D3-00D83CEBEB34}");
                                }
                                else if (shopTag.Shop_MEN_MadeToMeasure != null && shopTag.Shop_MEN_MadeToMeasure.StartsWith("Y"))
                                {
                                    tags.Add("{09D2F71B-EF56-4FA6-969A-53F7DE0ABEA0}");
                                }
                                else if (shopTag.Shop_MEN_Shoes != null && shopTag.Shop_MEN_Shoes.StartsWith("Y"))
                                {
                                    tags.Add("{37DE7516-5325-4534-A035-A908D3D256D9}");
                                }
                                else if (shopTag.Shop_MEN_VIPRoom != null && shopTag.Shop_MEN_VIPRoom.StartsWith("Y"))
                                {
                                    // TODO: Shop_MEN_VIPRoom
                                    tags.Add("{56FED65F-5267-4350-B440-F22DC2B2218F}");
                                }
                                else if (shopTag.Shop_MEN_LANDMARKMEN != null && shopTag.Shop_MEN_LANDMARKMEN.StartsWith("Y"))
                                {
                                    tags.Add("{111013C6-0BCD-4E1C-8CCA-7D7DDC405B0A}");
                                }
                                else if (shopTag.Shop_WOMEN != null && shopTag.Shop_WOMEN.StartsWith("Y"))
                                {
                                    tags.Add("{584512EB-189E-4FEF-9280-3F893DD9A9E1}");
                                }
                                else if (shopTag.Shop_WOMEN_Accessories != null && shopTag.Shop_WOMEN_Accessories.StartsWith("Y"))
                                {
                                    tags.Add("{7895E95B-BA35-4C28-9325-0BB7443E6945}");
                                }
                                else if (shopTag.Shop_WOMEN_ActiveWear != null && shopTag.Shop_WOMEN_ActiveWear.StartsWith("Y"))
                                {
                                    // TODO: Shop_WOMEN_ActiveWear
                                    tags.Add("{BF280597-458A-45C0-97FE-ED4614C75CAF}");
                                }
                                else if (shopTag.Shop_WOMEN_Bags != null && shopTag.Shop_WOMEN_Bags.StartsWith("Y"))
                                {
                                    tags.Add("{1A1800EA-D40A-412C-970A-DBA534DC1F23}");
                                }
                                else if (shopTag.Shop_WOMEN_CocktailDressesGowns != null && shopTag.Shop_WOMEN_CocktailDressesGowns.StartsWith("Y"))
                                {
                                    tags.Add("{585407EB-2AAB-454C-BB2B-2D9868FA53BA}");
                                }
                                else if (shopTag.Shop_WOMEN_Fashion != null && shopTag.Shop_WOMEN_Fashion.StartsWith("Y"))
                                {
                                    tags.Add("{01E67855-9741-4C1C-9FCD-2432982120B6}");
                                }
                                else if (shopTag.Shop_WOMEN_Shoes != null && shopTag.Shop_WOMEN_Shoes.StartsWith("Y"))
                                {
                                    tags.Add("{89884B9A-3DE7-47AD-BC7F-F2500B956EFD}");
                                }
                                else if (shopTag.Shop_WOMEN_VIPRoom != null && shopTag.Shop_WOMEN_VIPRoom.StartsWith("Y"))
                                {
                                    // TODO: Shop_WOMEN_VIPRoom
                                    tags.Add("{6CD9230F-42B3-476C-B074-F2C099B8C515}");
                                }
                                else if (shopTag.Shop_CHILDRENBABIES != null && shopTag.Shop_CHILDRENBABIES.StartsWith("Y"))
                                {
                                    tags.Add("{F1CC2EDE-A165-4FC4-B47A-8C0DF29BDE27}");
                                }
                                else if (shopTag.Shop_CHILDRENBABIES_Accessories != null && shopTag.Shop_CHILDRENBABIES_Accessories.StartsWith("Y"))
                                {
                                    tags.Add("{73D42903-0C38-4D70-A14E-C773BDCFBCC6}");
                                }
                                else if (shopTag.Shop_CHILDRENBABIES_Clothing != null && shopTag.Shop_CHILDRENBABIES_Clothing.StartsWith("Y"))
                                {
                                    tags.Add("{A869505D-8B56-42B6-9460-9928607C3BD3}");
                                }
                                else if (shopTag.Shop_CHILDRENBABIES_Shoes != null && shopTag.Shop_CHILDRENBABIES_Shoes.StartsWith("Y"))
                                {
                                    tags.Add("{83DA7F21-4D35-481C-B865-2A089D47AD10}");
                                }
                                else if (shopTag.Shop_CHILDRENBABIES_Toys != null && shopTag.Shop_CHILDRENBABIES_Toys.StartsWith("Y"))
                                {
                                    tags.Add("{BB0E8159-D1DF-4198-9007-2E5DB5A725BE}");
                                }
                                else if (shopTag.Shop_WELLNESS != null && shopTag.Shop_WELLNESS.StartsWith("Y"))
                                {
                                    tags.Add("{101C4A58-6D7D-4905-AD44-F0F054282440}");
                                }
                                else if (shopTag.Shop_WELLNESS_Fragrance != null && shopTag.Shop_WELLNESS_Fragrance.StartsWith("Y"))
                                {
                                    tags.Add("{30E208D0-F316-47A5-A174-6B83D8C7A47F}");
                                }
                                else if (shopTag.Shop_WELLNESS_GymPool != null && shopTag.Shop_WELLNESS_GymPool.StartsWith("Y"))
                                {
                                    tags.Add("{FBD8F196-12F3-4A12-AD62-DB44CBE512AB}");
                                }
                                else if (shopTag.Shop_WELLNESS_SpaSalon != null && shopTag.Shop_WELLNESS_SpaSalon.StartsWith("Y"))
                                {
                                    tags.Add("{E95D9EB0-E7E3-4CA5-A41F-B1AF769A5F72}");
                                }
                                else if (shopTag.Shop_WELLNESS_SportsEquipment != null && shopTag.Shop_WELLNESS_SportsEquipment.StartsWith("Y"))
                                {
                                    tags.Add("{F0DE64CF-AF62-4867-B6BA-09FF60AFBF06}");
                                }
                                else if (shopTag.Shop_WELLNESS_SkinPersonalCareMakeup != null && shopTag.Shop_WELLNESS_SkinPersonalCareMakeup.StartsWith("Y"))
                                {
                                    tags.Add("{7F1B0BFA-C6FF-4BAD-89B9-25D56D36DA5E}");
                                }
                                else if (shopTag.Shop_HOME != null && shopTag.Shop_HOME.StartsWith("Y"))
                                {
                                    tags.Add("{45EE72B3-0021-4A62-AA7F-55E291FB3979}");
                                }
                                else if (shopTag.Shop_HOME_BedBath != null && shopTag.Shop_HOME_BedBath.StartsWith("Y"))
                                {
                                    tags.Add("{99F41C3C-714B-4A12-8627-A3959AE90310}");
                                }
                                else if (shopTag.Shop_HOME_Decor != null && shopTag.Shop_HOME_Decor.StartsWith("Y"))
                                {
                                    tags.Add("{0D148D44-13B2-49E5-97C6-B748E2BB08D5}");
                                }
                                else if (shopTag.Shop_HOME_Florists != null && shopTag.Shop_HOME_Florists.StartsWith("Y"))
                                {
                                    tags.Add("{2BD4244A-C395-49FB-A414-0F403174458A}");
                                }
                                else if (shopTag.Shop_HOME_GiftsAccessories != null && shopTag.Shop_HOME_GiftsAccessories.StartsWith("Y"))
                                {
                                    tags.Add("{EBC7CBA8-4A32-42F7-8E0C-4B9322F6E859}");
                                }
                                else if (shopTag.Shop_HOME_KitchenTableware != null && shopTag.Shop_HOME_KitchenTableware.StartsWith("Y"))
                                {
                                    tags.Add("{34DEB1F1-7863-44BE-A67A-8E1900EFB90F}");
                                }
                                else if (shopTag.Shop_FOODWINE != null && shopTag.Shop_FOODWINE.StartsWith("Y"))
                                {
                                    tags.Add("{3FBE00DD-2724-453D-8208-D0E3425672F1}");
                                }
                                else if (shopTag.Shop_FOODWINE_ChocolatiersPatisseriesCafes != null && shopTag.Shop_FOODWINE_ChocolatiersPatisseriesCafes.StartsWith("Y"))
                                {
                                    tags.Add("{EBC788C5-2B1A-4853-A671-BCDDF5C2716B}");
                                }
                                else if (shopTag.Shop_FOODWINE_GourmetMarket != null && shopTag.Shop_FOODWINE_GourmetMarket.StartsWith("Y"))
                                {
                                    tags.Add("{B5EE2159-7D90-447E-AED3-20FDDBE05589}");
                                }
                                else if (shopTag.Shop_FOODWINE_Tobacconists != null && shopTag.Shop_FOODWINE_Tobacconists.StartsWith("Y"))
                                {
                                    tags.Add("{B9A9726C-B191-4F45-B394-15511A052704}");
                                }
                                else if (shopTag.Shop_FOODWINE_WinesSpirits != null && shopTag.Shop_FOODWINE_WinesSpirits.StartsWith("Y"))
                                {
                                    tags.Add("{30563DB6-3409-4BF6-AFC9-1050A7240A30}");
                                }
                                else if (shopTag.Shop_Gadgets != null && shopTag.Shop_Gadgets.StartsWith("Y"))
                                {
                                    tags.Add("{4BAF85E9-53CA-435A-89FF-5C51E7F9750C}");
                                }
                                else if (shopTag.Shop_Gadgets_Accessories != null && shopTag.Shop_Gadgets_Accessories.StartsWith("Y"))
                                {
                                    tags.Add("{D509AB0E-0999-43AF-808B-6AEB1FE814E7}");
                                }
                                else if (shopTag.Shop_Gadgets_AudioVisual != null && shopTag.Shop_Gadgets_AudioVisual.StartsWith("Y"))
                                {
                                    tags.Add("{02EB02B5-7016-403C-8407-53E7445DB9BD}");
                                }
                                else if (shopTag.Shop_Gadgets_Photography != null && shopTag.Shop_Gadgets_Photography.StartsWith("Y"))
                                {
                                    tags.Add("{32DD0452-44B5-429E-BC2D-D230597366C0}");
                                }
                                else if (shopTag.Shop_BooksPhotos != null && shopTag.Shop_BooksPhotos.StartsWith("Y"))
                                {
                                    tags.Add("{362A498E-7EAB-47A3-9196-C6D5F3B4DAC3}");
                                }
                                else if (shopTag.Shop_BooksPhotos_BooksNewspapersMagazines != null && shopTag.Shop_BooksPhotos_BooksNewspapersMagazines.StartsWith("Y"))
                                {
                                    tags.Add("{716DD4EF-A6E5-49CF-9C4B-8DD7EDFD441A}");
                                }
                                else if (shopTag.Shop_BooksPhotos_PhotosPrinting != null && shopTag.Shop_BooksPhotos_PhotosPrinting.StartsWith("Y"))
                                {
                                    tags.Add("{9D8452D6-1B63-4674-9379-143BC8D09340}");
                                }
                                else if (shopTag.Shop_BooksPhotos_Stationary != null && shopTag.Shop_BooksPhotos_Stationary.StartsWith("Y"))
                                {
                                    tags.Add("{43B3E7AE-04E4-4228-A4A0-17EC764526CF}");
                                }
                                else if (shopTag.Shop_ArtAntiques != null && shopTag.Shop_ArtAntiques.StartsWith("Y"))
                                {
                                    tags.Add("{FCDCFA36-AC20-495B-A6A3-B1FAEB5B4735}");
                                }
                                else if (shopTag.Shop_ArtAntiques_Art != null && shopTag.Shop_ArtAntiques_Art.StartsWith("Y"))
                                {
                                    tags.Add("{AA3EA496-8DBA-4B9F-8052-4126BF95286E}");
                                }
                                else if (shopTag.Shop_ArtAntiques_Antiques != null && shopTag.Shop_ArtAntiques_Antiques.StartsWith("Y"))
                                {
                                    tags.Add("{35F62E5A-E18A-4BBC-822B-72BBF10FAD99}");
                                }
                                else if (shopTag.Shop_JEWELLERYWATCHES != null && shopTag.Shop_JEWELLERYWATCHES.StartsWith("Y"))
                                {
                                    tags.Add("{C70FEE58-7079-41B5-A53C-F7B7805CE669}");
                                }
                                else if (shopTag.Shop_EYEWEAR != null && shopTag.Shop_EYEWEAR.StartsWith("Y"))
                                {
                                    tags.Add("{26D53B44-8352-4FCC-992A-B01A3C7A2384}");
                                }
                                else if (shopTag.Shop_ClothingCare != null && shopTag.Shop_ClothingCare.StartsWith("Y"))
                                {
                                    tags.Add("{E7DB6523-9921-4A16-A2BD-48A166E5B3F3}");
                                }
                                else if (shopTag.Shop_OTHER != null && shopTag.Shop_OTHER.StartsWith("Y"))
                                {
                                    // Todo: Shop_OTHER
                                    tags.Add("{A8B038A4-45C9-4690-A418-2712B2650DEC}");
                                }

                                #endregion
                            }
                            else if (isDine)
                            {
                                #region dining tags

                                var dineTag = dineTags.Single(t => t.Name == detail.NameEn);
                                if (dineTag.Location_Atrium != null)
                                {

                                }
                                if (dineTag.Location_Atrium != null)
                                {
                                    if (dineTag.Location_Atrium != null && dineTag.Location_Atrium != null && dineTag.Location_Atrium.StartsWith("Y"))
                                    {
                                        // TODO: Location_Atrium
                                        tags.Add("{C034122F-A68F-4E3C-827E-C1898533C08E}");
                                    }
                                }
                                if (dineTag.Location_Princes != null)
                                {
                                    if (dineTag.Location_Princes != null && dineTag.Location_Princes != null && dineTag.Location_Princes.StartsWith("Y"))
                                    {
                                        // TODO: Location_Princes
                                        tags.Add("{7349B461-564F-4A14-B43D-7B4F6FF1E0D6}");
                                    }
                                }
                                if (dineTag.Location_Alexendra != null)
                                {
                                    if (dineTag.Location_Alexendra != null && dineTag.Location_Alexendra != null && dineTag.Location_Alexendra.StartsWith("Y"))
                                    {
                                        // TODO: Location_Alexendra
                                    tags.Add("{B597804A-C4D7-44FB-9FAC-490726EEB3D9}");
                                    }
                                }
                                if (dineTag.Location_Chater != null)
                                {
                                    if (dineTag.Location_Chater != null && dineTag.Location_Chater.StartsWith("Y"))
                                    {
                                        // TODO: Location_Chater
                                        tags.Add("{37C1F81A-7605-4539-808B-C4B487FDB8DE}");
                                    }
                                }
                                if (dineTag.Location_Other_EdinburghTower != null)
                                {
                                    if (dineTag.Location_Other_EdinburghTower != null && dineTag.Location_Other_EdinburghTower.StartsWith("Y"))
                                    {
                                        // TODO: Location_Other_EdinburghTower
                                        tags.Add("{132E7671-084F-4DFD-9173-FEFB6A67FBAA}");
                                    }
                                }
                                if (dineTag.Location_Other_JardineHouse != null)
                                {
                                    if (dineTag.Location_Other_JardineHouse != null && dineTag.Location_Other_JardineHouse.StartsWith("Y"))
                                    {
                                        // TODO: Location_Other_JardineHouse
                                        tags.Add("{1DA4C84C-5353-4961-9C01-4A480E81C865}");
                                    }
                                }
                                if (dineTag.Location_Other_MandarinOrientalHotel != null)
                                {
                                    if (dineTag.Location_Other_MandarinOrientalHotel != null && dineTag.Location_Other_MandarinOrientalHotel.StartsWith("Y"))
                                    {
                                        // TODO: Location_Other_MandarinOrientalHotel
                                        tags.Add("{6ADD337A-5273-4504-AD72-8C601D69C6F9}");
                                    }
                                }
                                if (dineTag.Location_Other_OneExchangeSquare != null)
                                {
                                    if (dineTag.Location_Other_OneExchangeSquare != null && dineTag.Location_Other_OneExchangeSquare.StartsWith("Y"))
                                    {
                                        // TODO: Location_Other_OneExchangeSquare
                                        tags.Add("{838D33D8-9AC8-4369-AB18-9470EADCD49B}");
                                    }
                                }
                                if (dineTag.Location_Other_TwoExchangeSquare != null)
                                {
                                    if (dineTag.Location_Other_TwoExchangeSquare != null && dineTag.Location_Other_TwoExchangeSquare.StartsWith("Y"))
                                    {
                                        // TODO: Location_Other_TwoExchangeSquare
                                        tags.Add("{B7798E58-05C8-4513-9C8C-F953674DA86D}");
                                    }
                                }
                                else if (dineTag.Dining_MichelinStarRestaurants != null && dineTag.Dining_MichelinStarRestaurants.StartsWith("Y"))
                                {
                                    tags.Add("{BC0F4B93-552A-418E-B620-7B7F13506613}");
                                }
                                else if (dineTag.Dining_Chinese != null && dineTag.Dining_Chinese.StartsWith("Y"))
                                {
                                    tags.Add("{50CB2BA4-EF38-46ED-908C-1CAD2F171849}");
                                }
                                else if (dineTag.Dining_Japanese != null && dineTag.Dining_Japanese.StartsWith("Y"))
                                {
                                    tags.Add("{C7F06241-2951-4647-8610-4853F73C07DF}");
                                }
                                else if (dineTag.Dining_Asian != null && dineTag.Dining_Asian.StartsWith("Y"))
                                {
                                    tags.Add("{983E7317-DB51-4EFE-9081-671D78C971C2}");
                                }
                                else if (dineTag.Dining_French != null && dineTag.Dining_French.StartsWith("Y"))
                                {
                                    tags.Add("{23B609A4-BF3A-4CE8-A076-39FA72C6DEBD}");
                                }
                                else if (dineTag.Dining_Italian != null && dineTag.Dining_Italian.StartsWith("Y"))
                                {
                                    tags.Add("{D0DDCCBB-558D-491F-AA49-46E8217BC26B}");
                                }
                                else if (dineTag.Dining_British != null && dineTag.Dining_British.StartsWith("Y"))
                                {
                                    tags.Add("{0F14B930-2EC3-4D43-A588-AB95F82C81E5}");
                                }
                                else if (dineTag.Dining_Western != null && dineTag.Dining_Western.StartsWith("Y"))
                                {
                                    tags.Add("{6B69973D-6FCC-45B2-9C75-ECCBC56F42FC}");
                                }
                                else if (dineTag.Dining_International != null && dineTag.Dining_International.StartsWith("Y"))
                                {
                                    tags.Add("{C0BFF7F2-6271-4B95-9914-19628F56860B}");
                                }
                                else if (dineTag.Dining_CasualDining != null && dineTag.Dining_CasualDining.StartsWith("Y"))
                                {
                                    tags.Add("{410058FD-4835-43BD-9FC0-430DAF3C8381}");
                                }
                                else if (dineTag.Dining_FineDining != null && dineTag.Dining_FineDining.StartsWith("Y"))
                                {
                                    tags.Add("{BA9D8236-ECD5-4824-B120-46B95D7FB13C}");
                                }
                                else if (dineTag.Dining_Cafes != null && dineTag.Dining_Cafes.StartsWith("Y"))
                                {
                                    tags.Add("{D84B1172-716F-4E5A-8CF0-7D22C9A221C1}");
                                }
                                else if (dineTag.Dining_AfternoonTea != null && dineTag.Dining_AfternoonTea.StartsWith("Y"))
                                {
                                    tags.Add("{CE078BA5-479D-42FE-BAB4-50A1459A29AA}");
                                }
                                else if (dineTag.Dining_CocktailBars != null && dineTag.Dining_CocktailBars.StartsWith("Y"))
                                {
                                    tags.Add("{2ED5B361-4D7A-4A48-B63E-8FE23B71DB8E}");
                                }

                                #endregion
                            }
                            using (new EditContext(version))
                            {
                                version.Fields["Tags"].Value = string.Join("|", tags);
                                // Floor & Location
                                var locs = shopXYs.Where(l => GenItemName(l.Name) == itemName)
                                    .Take(1); // we can only handle one location for one shop
                                foreach (var loc in locs)
                                {
                                    version.Fields["Floor"].Value = GetFloorId(loc.Filename);
                                    version.Fields["Svg Id"].Value = loc.MapId;
                                    version.Fields["LocationX"].Value = loc.X;
                                    version.Fields["LocationY"].Value = loc.Y;
                                }
                            }
                        }
                        version.Editing.AcceptChanges();
                    }
                }
            }

            foreach (var artxy in artXYs)
            {
                var title = artxy.Name.Substring(artxy.Name.LastIndexOf("-") + 1).Trim();
                var artpiece = LandmarkHelper.GetItemByTemplate(artFolder, "{ADE0F061-FF28-4CDB-B6C3-14021D75355A}").FirstOrDefault(i => i.Fields["Page Title"].Value == title);
                if (artpiece == null)
                    continue;
                var version = _masterDb.GetItem(artpiece.ID, artpiece.Languages.First());
                using (new SecurityDisabler())
                {
                    using (new EditContext(version))
                    {
                        version.Fields["Floor and Building"].Value = GetFloorId(artxy.Filename);
                        version.Fields["LocationX"].Value = artxy.X;
                        version.Fields["LocationY"].Value = artxy.Y;
                    }
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public static string GenItemName(string s)
        {
            return
                Regex.Replace(Regex.Replace(s.ToLower().Replace("é", "e").Replace("ë", "e"), "[^a-z0-9 ]", ""), " +", " ");
        }

        public static string GetFloorId(string filename)
        {
            string floor = "";
            switch (filename)
            {
                case ("the-forum"):
                    // todo
                    floor = "{1009E80F-6461-43D7-B0F9-C2ACB929C17E}";
                    break;
                case ("1&2exchangesquare-1F"):
                    // todo
                    floor = "{0C3168A1-1A15-4920-9948-5AE01E8D628B}";
                    break;
                case ("1exchangesquare - 3F"):
                    // todo
                    floor = "{1363D861-B152-4162-9005-82872E183741}";
                    break;
                case ("3exchangesquare"):
                    // todo
                    floor = "{3779275D-C296-4BF7-9F73-4ED1F1B9DB7F}";
                    break;
                case ("jardine-house-01-GF"):
                    // todo
                    floor = "{EDD97B8A-24FC-4629-8091-97ED4D907003}";
                    break;
                case ("jardine-house-02-1F"):
                    // todo
                    floor = "{3C2CE7FA-F1B0-42A6-9FC3-53D53BDD6ABF}";
                    break;
                case ("landmark-alexandra-01-BF"):
                    floor = "{6845F4BA-3515-4E03-9A10-373D6A7F144B}";
                    break;
                case ("landmark-alexandra-02-GF"):
                    floor = "{375FC2F4-E801-442F-B1EA-B75EFB86BB39}";
                    break;
                case ("landmark-alexandra-03-1F"):
                    floor = "{C9C5CE15-BBC3-4E18-9348-55E02C9C877B}";
                    break;
                case ("landmark-alexandra-04-2F"):
                    floor = "{F899C775-5799-4BF1-9857-11BC085D1E9E}";
                    break;
                case ("landmark-atrium-01-BF"):
                    floor = "{99A4A960-53BC-4C37-A070-7915F8DC1B69}";
                    break;
                case ("landmark-atrium-02-GF"):
                    floor = "{BDA32531-667F-420C-881F-4550CD6E755E}";
                    break;
                case ("landmark-atrium-03-1F"):
                    floor = "{E4D421D6-DCB0-40F4-B554-A52F83DC40EF}";
                    break;
                case ("landmark-atrium-04-2F"):
                    floor = "{CDB69347-0DE1-4FDC-AF4B-EBEF05042A2D}";
                    break;
                case ("landmark-atrium-05-3F"):
                    floor = "{49472FA6-6333-43F3-84F3-0BF2693466F3}";
                    break;
                case ("landmark-atrium-06-4F"):
                    floor = "{627AC1D8-A661-44CC-9AB0-8C142410902F}";
                    break;
                case ("landmark-chater-01-GF"):
                    floor = "{BA634CEC-AF14-4B7B-BDA1-B2A8511745C3}";
                    break;
                case ("landmark-chater-02-1F"):
                    floor = "{D27D3B22-EC13-4803-B7A3-B79C16BA5A08}";
                    break;
                case ("landmark-chater-03-2F"):
                    floor = "{79EBEDCD-CED4-4C05-993C-D8B6F30BE2E0}";
                    break;
                case ("landmark-prince-01-GF"):
                    floor = "{48DC425C-A1F5-4A36-B9DB-2786025069B4}";
                    break;
                case ("landmark-prince-02-MF"):
                    floor = "{521F9A37-84EE-4F83-8D2C-1ABD8DDADD44}";
                    break;
                case ("landmark-prince-03-1F"):
                    floor = "{C12BDF3C-6B8C-4752-9AA9-468DA724AE6D}";
                    break;
                case ("landmark-prince-04-2F"):
                    floor = "{F2BF84A0-30D0-4B5D-9347-20DAD6082098}";
                    break;
                case ("landmark-prince-05-3F"):
                    floor = "{3F85EF25-5B91-488B-B672-E6DAC79E4C87}";
                    break;
            }
            return floor;
        }

        public void SetImageFieldValue(string imageid, Item item, string imageField)
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
}
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Landmark.Classes
{
    public abstract class ItemGuids
    {
        //rendering guids
        public static readonly String GlossaryRenderingViewGuid = "{8DB7585D-4A38-4687-A72F-B33AF97B6369}";
        public static readonly String ScriptRenderingViewGuid = "{6DB62DA9-0028-4812-BF51-5CC899DC8B66}";
        public static readonly String FooterRenderingViewGuid = "{C34F6069-D5B7-4D4A-97A4-DFEB53AE6F1C}";
        public static readonly String PanelNavRenderingViewGuid = "{BE3E7C0F-02E0-4E61-A62D-13E475CF76B5}";
        public static readonly String HeaderRenderingViewGuid = "{74159145-AA53-4198-9506-0B451AB91E02}";
        public static readonly String FooterNavRenderingViewGuid = "{3BCEEFB7-E383-41B5-B42D-CF08898CF378}";
        public static readonly String FooterConfigRenderingViewGuid = "{59CD0D0B-B8B3-4849-8A2E-2AD9A0D1CA46}";
        public static readonly String NavigationRenderingViewGuid = "{25D2AD09-84E3-4DA8-9720-9DFEECC67A9B}";
        public static readonly String MobileNavigationRenderingViewGuid = "{A45E5400-EC5C-4539-823F-D6E102F8CF6A}";
        public static readonly string T11BreadcrymbRenderingViewGuid = "{C85A5336-5D9F-47A0-ADBD-DA8F6975E51B}";

        
        //item id
        public static readonly string PanelsFolderItemGuid = "{3BB40D54-8F7F-449B-B9DB-432C65EB4224}";
        public static readonly string AdvertTemplateItemGuid = "{32677AD3-AB70-49BF-A525-08493045C4FA}";
        public static readonly string FullSizePhotoOverlayText = "{88EC2664-2C62-4BA7-923A-E62C7FAE7DA8}";
        public static readonly string LeftPhotoRightText = "{92C90E8C-7528-4C7C-B1FA-46205828279E}";
        public static readonly string OneTitleOneTextLink = "{D4D02E60-F4D6-4FA1-A6B9-B4CD0DC3CC40}";
        public static readonly string TwoNumbersTwoCaptionsTextLink = "{85008200-165B-4E4D-B4B9-1CDA97D7A170}";

        //item 
        public static readonly Item LandmarkHomeItem = Sitecore.Context.Database.GetItem("{691A2D60-6CAF-42B7-90ED-7DD1AE99AFF3}");
        public static readonly Item SearchPlaceHolderItem = Sitecore.Context.Database.GetItem("{69752992-6EA7-4FE6-8925-E279B4469B9F}");
        public static readonly Item LandmarkConfigItem = Sitecore.Context.Database.GetItem("{BDB18DFC-7C97-40F8-A80C-658210B41656}");
        public static readonly Item FooterNavigationItem = Sitecore.Context.Database.GetItem("{9B0D8F54-71B1-41A5-B1F9-B1799DB44C6A}");
        public static readonly Item ScrollToBeginGuidItem = Sitecore.Context.Database.GetItem("{A56115E2-9083-4C3C-AC15-3AB1FEB4A90B}");
        public static readonly Item TopGuidItem = Sitecore.Context.Database.GetItem("{95BFC753-B808-40FD-9380-78CED4E3D341}");
        public static readonly Item TraditionalChineseGuidItem = Sitecore.Context.Database.GetItem("{B9A164BF-C9D5-486E-96A5-229F960D6FFD}");
        public static readonly Item SimplifiedChineseGuidItem = Sitecore.Context.Database.GetItem("{C6222FDA-DA27-41EF-A5E4-D5201FF82C64}");
        public static readonly Item ShareThisGuidItem = Sitecore.Context.Database.GetItem("{DE562DBA-71F1-4D62-B19E-59B1BC78C925}");
        public static readonly Item SeeAllBrandsGuidItem = Sitecore.Context.Database.GetItem("{78A6E71A-D03F-471C-9F3F-7CEB8F75A8DC}");
        public static readonly Item AlsoInterestedInGuidItem = Sitecore.Context.Database.GetItem("{DEC9DDE1-89C6-4692-8731-DFFBCBF0E345}");
        public static readonly Item AllBrandsGuidItem = Sitecore.Context.Database.GetItem("{5BF030CD-00E2-49CA-A19F-112D3E2B59E6}");
        public static readonly Item ByBrandsGuidItem = Sitecore.Context.Database.GetItem("{2CAAA2DB-1D12-49CA-BA34-1E0E2731B167}");
        public static readonly Item ByBuidingsGuidItem = Sitecore.Context.Database.GetItem("{323F290C-253A-4FEA-BEC0-3833E7ADBBC4}");
        public static readonly Item GoGuidItem = Sitecore.Context.Database.GetItem("{9059E796-C6C9-433F-964C-607614D5F4B3}");
        public static readonly Item SeeAlsoGuidItem = Sitecore.Context.Database.GetItem("{9173D2DC-5938-4C79-89DB-B77C0A4E2B12}");
        
        
    }
}
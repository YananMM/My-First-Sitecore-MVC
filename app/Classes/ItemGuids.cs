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

        

        //item id guids
        public static readonly String ScrollToBeginItemGuid = "{A56115E2-9083-4C3C-AC15-3AB1FEB4A90B}";
        public static readonly String TopItemGuid = "{95BFC753-B808-40FD-9380-78CED4E3D341}";
        public static readonly String PanelsFolderItemGuid = "{3BB40D54-8F7F-449B-B9DB-432C65EB4224}";
        public static readonly String TraditionalChineseItemGuid = "{B9A164BF-C9D5-486E-96A5-229F960D6FFD}";
        public static readonly String SimplifiedChineseItemGuid = "{C6222FDA-DA27-41EF-A5E4-D5201FF82C64}";



        //item 
        public static readonly Item LandmarkHomeItem = Sitecore.Context.Database.GetItem("{691A2D60-6CAF-42B7-90ED-7DD1AE99AFF3}");
        public static readonly Item SearchPlaceHolderItem = Sitecore.Context.Database.GetItem("{69752992-6EA7-4FE6-8925-E279B4469B9F}");
        public static readonly Item LandmarkConfigItem = Sitecore.Context.Database.GetItem("{BDB18DFC-7C97-40F8-A80C-658210B41656}");
        public static readonly Item FooterNavigationItem = Sitecore.Context.Database.GetItem("{9B0D8F54-71B1-41A5-B1F9-B1799DB44C6A}");

    }
}
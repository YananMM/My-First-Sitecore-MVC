using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Landmark.Models;

namespace Landmark.layouts.Landmark
{
    public partial class EmailSignupExport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ExportBtn_Click(object sender, EventArgs e)
        {
            using (LandmarkSitecore_MasterEntities context = new LandmarkSitecore_MasterEntities())
            {
                
            }
        }
    }
}
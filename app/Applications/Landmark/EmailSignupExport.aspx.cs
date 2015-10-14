using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Landmark.Classes;
using Landmark.Models;
using LINQtoCSV;
using Sitecore.Web;

namespace Landmark.layouts.Landmark
{
    public partial class EmailSignupExport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Sitecore.Context.User.IsAuthenticated)
            {
                string host =System.Web.HttpContext.Current.Request.Url.Scheme +
                          Uri.SchemeDelimiter +
                          System.Web.HttpContext.Current.Request.Url.Host;
                Response.Redirect(host+"/sitecore");
            }
        }

        protected void ExportBtn_Click(object sender, EventArgs e)
        {
            using (LandmarkSitecore_MasterEntities context = new LandmarkSitecore_MasterEntities())
            {
                var results =
                    context.EmailSignups.Where(
                        data => data.CreatedOn < toDate.SelectedDate && data.CreatedOn > fromDate.SelectedDate);
                List<EmailSignUpCsvModel> signups = new List<EmailSignUpCsvModel>();
                foreach (var result in results)
                {
                    
                    signups.Add(new EmailSignUpCsvModel(result));
                }

                CsvFileDescription outputFileDescription = new CsvFileDescription
                {
                    SeparatorChar = '\t', // tab delimited
                    FirstLineHasColumnNames = true, // no column names in first record
                    FileCultureName = "nl-NL" // use formats used in The Netherlands
                };

                CsvContext cc = new CsvContext();
                cc.Write(
                    signups,
                    @SitecoreItems.LandmarkConfigItem.Fields["CSV Folder"].Value + "signups" + toDate.SelectedDate.Date.ToString("yyyyMMdd") + "-" + fromDate.SelectedDate.Date.ToString("yyyyMMdd") + ".csv",
                    outputFileDescription);
            }
        }
    }
}
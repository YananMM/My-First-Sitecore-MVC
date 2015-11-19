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
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Web;

namespace Landmark.layouts.Landmark
{
    public partial class EmailSignupExport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Sitecore.Context.User.IsAuthenticated)
            {
                string host = System.Web.HttpContext.Current.Request.Url.Scheme +
                          Uri.SchemeDelimiter +
                          System.Web.HttpContext.Current.Request.Url.Host;
                Response.Redirect(host + "/sitecore");
            }
        }

        protected void ExportBtn_Click(object sender, EventArgs e)
        {
            using (LandmarkEntities context = new LandmarkEntities())
            {
                var results =
                    context.EmailSignups.Where(
                        data => data.CreatedOn < toDate.SelectedDate && data.CreatedOn > fromDate.SelectedDate);
                if (results.Count() == 0)
                {
                    Response.Write("No data in selected date.");
                }
                List<EmailSignUpCsvModel> signups = new List<EmailSignUpCsvModel>();
                foreach (var result in results)
                {

                    signups.Add(new EmailSignUpCsvModel(result));
                }

                CsvFileDescription outputFileDescription = new CsvFileDescription
                {
                    SeparatorChar = '\t', // tab delimited
                    FirstLineHasColumnNames = true, // no column names in first record
                    // FileCultureName = "nl-NL" // use formats used in The Netherlands
                };

                CsvContext cc = new CsvContext();
                var fileName = Factory.GetDatabase("web").GetItem(ItemGuids.LandmarkConfigItem).Fields["CSV Folder"].Value +
                           "signups" + toDate.SelectedDate.Date.ToString("yyyyMMdd") + "-" +
                           fromDate.SelectedDate.Date.ToString("yyyyMMdd") + ".csv";
                cc.Write(
                    signups, 
                    fileName,
                    outputFileDescription);
                Response.WriteFile(fileName);
            }
        }
    }
}
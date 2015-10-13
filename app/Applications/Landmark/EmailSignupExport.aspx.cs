using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Landmark.Models;
using LINQtoCSV;

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
                var results =
                    context.EmailSignups.Where(
                        data => data.CreatedOn < toDate.SelectedDate && data.CreatedOn > fromDate.SelectedDate);
                List<EmailSignUpCsvModel> signups = new List<EmailSignUpCsvModel>();
                foreach (var result in results)
                {
                    var interests = result.Interest.Split(',');
            // you may want to make this not hard-coded
            var predefined = new[] { 
                "Shoppping", "Fashion", "Home Kids",
                "Books", "Gadgets", "Automobiles", 
                "Arts", "Music", "Movies",
                "Photography", "Sports Fitness", "Travelling",
                "Social Responsibilty"
            };
                    signups.Add(new EmailSignUpCsvModel
                    {
                        Title = result.Title,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email,
            Gender = result.Title == "Mr" ? "Male" : "Female",  // client asked for this
            /* TODO: uncomment these lines
            IpAddress = model.IpAdress;
            OptIn = model.OptIn;*/
            Channel = result.Channel,
            Room = result.Room,
            Building = result.Building,
            Street = result.Street,
            Area = result.Area,
            District = result.District,
            State = result.State,
            City = result.City,
            Postcode = result.Postcode,
            Country = result.Country,
            CreatedOn = result.CreatedOn,

            // ugly conversion codes
            
            InterestShopping    = interests.Contains(predefined[0]) ? "Yes" : "No",
            InterestFashion     = interests.Contains(predefined[1]) ? "Yes" : "No",
            InterestHomeKids    = interests.Contains(predefined[2]) ? "Yes" : "No",
            InterestBooks       = interests.Contains(predefined[3]) ? "Yes" : "No",
            InterestGadgets     = interests.Contains(predefined[4]) ? "Yes" : "No",
            InterestAutomobiles = interests.Contains(predefined[5]) ? "Yes" : "No",
            InterestArts        = interests.Contains(predefined[6]) ? "Yes" : "No",
            InterestMusic       = interests.Contains(predefined[7]) ? "Yes" : "No",
            InterestMovies      = interests.Contains(predefined[8]) ? "Yes" : "No",
            InterestPhotography = interests.Contains(predefined[9]) ? "Yes" : "No",
            InterestSports      = interests.Contains(predefined[10]) ? "Yes" : "No",
            InterestTravelling  = interests.Contains(predefined[11]) ? "Yes" : "No",
            InterestSocialResponsibility = interests.Contains(predefined[12]) ? "Yes" : "No",
            InterestOthers = result.Interest.Split(',').Except(predefined).First(),
                    });
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
                    @ConfigurationManager.AppSettings["CSVFolder"] + "signups" + toDate.SelectedDate.Date.ToString("yyyyMMdd") + "-" + fromDate.SelectedDate.Date.ToString("yyyyMMdd") + ".csv",
                    outputFileDescription);
            }
        }
    }
}
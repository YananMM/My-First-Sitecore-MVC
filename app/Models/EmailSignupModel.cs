// ***********************************************************************
// Assembly         : Landmark
// Author           : Jiang Lili
// Created          : 09-18-2015
//
// Last Modified By : Jiang Lili
// Last Modified On : 09-18-2015
// ***********************************************************************
// <copyright file="EmailSignupModel.cs" company="Gruden">
//     Copyright (c) Gruden. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LINQtoCSV;

namespace Landmark.Models
{
    /// <summary>
    /// Class EmailSignupModel.
    /// </summary>
    public class EmailSignupModel
    {
        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>The unique identifier.</value>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>The channel.</value>
        public string Channel { get; set; }

        /// <summary>
        /// Gets or sets the room.
        /// </summary>
        /// <value>The room.</value>
        public string Room { get; set; }

        /// <summary>
        /// Gets or sets the building.
        /// </summary>
        /// <value>The building.</value>
        public string Building { get; set; }

        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        /// <value>The street.</value>
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the area.
        /// </summary>
        /// <value>The area.</value>
        public string Area { get; set; }

        /// <summary>
        /// Gets or sets the district.
        /// </summary>
        /// <value>The district.</value>
        public string District { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the postcode.
        /// </summary>
        /// <value>The postcode.</value>
        public string Postcode { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>The country.</value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the interest.
        /// </summary>
        /// <value>The interest.</value>
        public string Interests { get; set; }

        /// <summary>
        /// Gets or sets the other interest.
        /// </summary>
        /// <value>The other interest.</value>
        public string Others { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>The gender.</value>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [opt information].
        /// </summary>
        /// <value><c>true</c> if [opt information]; otherwise, <c>false</c>.</value>
        public int OptIn { get; set; }

        /// <summary>
        /// Gets or sets the created configuration.
        /// </summary>
        /// <value>The created configuration.</value>
        public DateTime CreatedOn { get; set; }
    }

    public class EmailSignUpCsvModel {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [CsvColumn(Name = "SALUTATION", FieldIndex = 1)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        [CsvColumn(Name = "FIRSTNAME", FieldIndex = 2)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        [CsvColumn(Name = "LASTNAME", FieldIndex = 3)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [CsvColumn(Name = "EMAIL", FieldIndex = 4)]
        public string Email { get; set; }

        [CsvColumn(Name = "GENDER", FieldIndex = 5)]
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the ip address.
        /// </summary>
        /// <value>The ip address.</value>
        [CsvColumn(Name = "IP_ADDRESS", FieldIndex = 6)]
        public string IpAddress { get; set; }

        [CsvColumn(Name = "USE_DATA", FieldIndex = 7)]
        public string OptIn { get; set; }


        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>The channel.</value>
        [CsvColumn(Name = "PREFERRED_CHANNEL", FieldIndex = 8)]
        public string Channel { get; set; }

        /// <summary>
        /// Gets or sets the room.
        /// </summary>
        /// <value>The room.</value>
        [CsvColumn(Name = "ROOM", FieldIndex = 9)]
        public string Room { get; set; }

        /// <summary>
        /// Gets or sets the building.
        /// </summary>
        /// <value>The building.</value>
        [CsvColumn(Name = "BUILDING", FieldIndex = 10)]
        public string Building { get; set; }

        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        /// <value>The street.</value>
        [CsvColumn(Name = "STREET", FieldIndex = 11)]
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the area.
        /// </summary>
        /// <value>The area.</value>
        [CsvColumn(Name = "AREA", FieldIndex = 12)]
        public string Area { get; set; }

        /// <summary>
        /// Gets or sets the district.
        /// </summary>
        /// <value>The district.</value>
        [CsvColumn(Name = "DISTRICT", FieldIndex = 13)]
        public string District { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        [CsvColumn(Name = "STATE", FieldIndex = 14)]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        [CsvColumn(Name = "CITY", FieldIndex = 15)]
        public string City { get; set; }


        /// <summary>
        /// Gets or sets the postcode.
        /// </summary>
        /// <value>The postcode.</value>
        [CsvColumn(Name = "Postcode", FieldIndex = 16)]
        public string Postcode { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>The country.</value>
        [CsvColumn(Name = "COUNTRY", FieldIndex = 17)]
        public string Country { get; set; }

        [CsvColumn(Name = "INTEREST_SHOPPING", FieldIndex = 18)]
        public string InterestShopping { get; set; }

        [CsvColumn(Name = "INTEREST_FASHION", FieldIndex = 19)]
        public string InterestFashion { get; set; }

        [CsvColumn(Name = "INTEREST_HOME_KIDS", FieldIndex = 20)]
        public string InterestHomeKids { get; set; }

        [CsvColumn(Name = "INTEREST_BOOKS", FieldIndex = 21)]
        public string InterestBooks { get; set; }

        [CsvColumn(Name = "INTEREST_GADGETS", FieldIndex = 22)]
        public string InterestGadgets { get; set; }

        [CsvColumn(Name = "INTEREST_AUTOMOBILES", FieldIndex = 23)]
        public string InterestAutomobiles { get; set; }

        [CsvColumn(Name = "INTEREST_ARTS", FieldIndex = 24)]
        public string InterestArts { get; set; }

        [CsvColumn(Name = "INTEREST_MUSIC", FieldIndex = 25)]
        public string InterestMusic { get; set; }

        [CsvColumn(Name = "INTEREST_MOVIES", FieldIndex = 26)]
        public string InterestMovies { get; set; }

        [CsvColumn(Name = "INTEREST_PHOTOGRAPHY", FieldIndex = 27)]
        public string InterestPhotography { get; set; }

        [CsvColumn(Name = "INTEREST_SPORTS", FieldIndex = 28)]
        public string InterestSports { get; set; }

        [CsvColumn(Name = "INTEREST_TRAVELLING", FieldIndex = 29)]
        public string InterestTravelling { get; set; }

        [CsvColumn(Name = "INTEREST_SOCIAL_RESPONSIBILITY", FieldIndex = 30)]
        public string InterestSocialResponsibility { get; set; }

        [CsvColumn(Name = "INTEREST_OTHERS", FieldIndex = 31)]
        public string InterestOthers { get; set; }

        public DateTime CreatedOn { get; set; }

        public EmailSignUpCsvModel() { }
        public EmailSignUpCsvModel(EmailSignup model)
        {
            Title = model.Title;
            FirstName = model.FirstName;
            LastName = model.LastName;
            Email = model.Email;
            Gender = model.Title == "Mr." ? "Male" : "Female";   // client asked for this
            IpAddress = model.IpAddress;
            if (model.OptIn != null)
            {
                OptIn = model.OptIn.ToString();
                
            }
            else
            {
                OptIn = string.Empty;
            }
            Channel = model.Channel;
            Room = model.Room;
            Building = model.Building;
            Street = model.Street;
            Area = model.Area;
            District = model.District;
            State = model.State;
            City = model.City;
            Postcode = model.Postcode;
            Country = model.Country;
            CreatedOn = model.CreatedOn;

            // ugly conversion codes
            var interests = model.Interest.Split(',');
            // you may want to make this not hard-coded
            var predefined = new[] { 
                "Shoppping", "Fashion", "Home Kids",
                "Books", "Gadgets", "Automobiles", 
                "Arts", "Music", "Movies",
                "Photography", "Sports Fitness", "Travelling",
                "Social Responsibilty"
            };
            InterestShopping    = interests.Contains(predefined[0]) ? "Yes" : "No";
            InterestFashion     = interests.Contains(predefined[1]) ? "Yes" : "No";
            InterestHomeKids    = interests.Contains(predefined[2]) ? "Yes" : "No";
            InterestBooks       = interests.Contains(predefined[3]) ? "Yes" : "No";
            InterestGadgets     = interests.Contains(predefined[4]) ? "Yes" : "No";
            InterestAutomobiles = interests.Contains(predefined[5]) ? "Yes" : "No";
            InterestArts        = interests.Contains(predefined[6]) ? "Yes" : "No";
            InterestMusic       = interests.Contains(predefined[7]) ? "Yes" : "No";
            InterestMovies      = interests.Contains(predefined[8]) ? "Yes" : "No";
            InterestPhotography = interests.Contains(predefined[9]) ? "Yes" : "No";
            InterestSports      = interests.Contains(predefined[10]) ? "Yes" : "No";
            InterestTravelling  = interests.Contains(predefined[11]) ? "Yes" : "No";
            InterestSocialResponsibility = interests.Contains(predefined[12]) ? "Yes" : "No";
            //var others = interests.Except(predefined);
            InterestOthers = model.Other_Interest;
        }
    }
}
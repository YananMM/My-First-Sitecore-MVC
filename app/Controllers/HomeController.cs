// ***********************************************************************
// Assembly         : Landmark
// Author           : Jiang Lili
// Created          : 08-10-2015
//
// Last Modified By : Jiang Lili
// Last Modified On : 08-10-2015
// ***********************************************************************
// <copyright file="HomeController.cs" company="Gruden">
//     Copyright (c) Gruden. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Landmark.Classes;
using Sitecore.Data.Items;
using Landmark.Models;
using Landmark.Helper;
using System.Text.RegularExpressions;
using Sitecore.Configuration;

// <summary>
// The Controllers namespace.
// </summary>
namespace Landmark.Controllers
{
    /// <summary>
    /// Class HomeController.
    /// </summary>
    public class HomeController : Controller
    {
        private ArtTourHelper _helper;

        public HomeController()
        {
            _helper = new ArtTourHelper();
        }

        /// <summary>
        /// Buttons the redirect.
        /// </summary>
        /// <param name="targetId">The target unique identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult ButtonRedirect(string targetId)
        {
            Item target = Sitecore.Context.Database.GetItem(targetId);
            return Redirect(LandmarkHelper.TranslateUrl(Sitecore.Links.LinkManager.GetItemUrl(target)));
        }

        /// <summary>
        /// Buttons the redirect.
        /// </summary>
        /// <param name="targetId">The target unique identifier.</param>
        /// <param name="type">Type of the experience.</param>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        public ActionResult FilterRedirect(string targetId, string type)
        {
            Item target = Sitecore.Context.Database.GetItem(targetId);
            return Redirect(LandmarkHelper.TranslateUrl(LandmarkHelper.TranslateUrl(Sitecore.Links.LinkManager.GetItemUrl(target))) + "?type=" + type + "&page=1");
        }

        public ActionResult DirectByPager(string targetId, int page)
        {
            Item target = Sitecore.Context.Database.GetItem(targetId);
            return Redirect(LandmarkHelper.TranslateUrl(LandmarkHelper.TranslateUrl(Sitecore.Links.LinkManager.GetItemUrl(target))) + "?page=" + page);
        }

        public ActionResult AddCustomerMessage(ContactUsFormModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = AddContact(model);
                    if (result == "true")
                    {
                        return RedirectToAction("ButtonRedirect", new { targetId = ItemGuids.ThankYouPage });
                    }
                    else
                    {
                        return Content(result);
                    }
                }
                catch (Exception e)
                {
                    return Content(e.Message);
                }
            }
            return RedirectToAction("ButtonRedirect", new { targetId = ItemGuids.ContactUsPage });
        }

        public string AddContact(ContactUsFormModel model)
        {
            var code = Session["ValidateCode"].ToString();
            if (string.IsNullOrEmpty(model.Email))
            {
                string strRegex = @"^[a-zA-Z0-9_+.-]+\@([a-zA-Z0-9-]+\.)+[a-zA-Z0-9]{2,4}$";
                Regex re = new Regex(strRegex);
                if (!re.IsMatch(model.Email))
                {
                    return "Please input correct Email";
                }
                return "Email can not be empty";
            }
            else if (string.IsNullOrEmpty(model.LastName))
            {
                return "Last Name can not be empty";
            }
            else if (string.IsNullOrEmpty(model.FirstName))
            {
                return "First Name can not be empty";
            }
            else if (string.IsNullOrEmpty(model.Email))
            {
                return "Emial can not be empty";
            }
            else if (string.IsNullOrEmpty(model.EnquiryType))
            {
                return "Enquiry Type can not be empty";
            }
            else if (string.IsNullOrEmpty(model.Message))
            {
                return "Message can not be empty";
            }
            else
            {
                var emailContent = Resources.Landmark.ContactUsForm;
                Item contactUsFormItem = Sitecore.Context.Database.GetItem(ItemGuids.ContactUsPage);
                var emailTo = contactUsFormItem.Fields["Email To"].Value;
                var emailSubject = contactUsFormItem.Fields["Email Subject"].Value;
                var emailBody = emailContent.Replace("{{Title}}", model.Title)
                    .Replace("{{FirstName}}", model.FirstName)
                    .Replace("{{LastName}}", model.LastName)
                    .Replace("{{Telephone}}", model.Telephone)
                    .Replace("{{Email}}", model.Email)
                    .Replace("{{EnquiryType}}", model.EnquiryType)
                    .Replace("{{Message}}", model.Message);
                var email = LandmarkHelper.ConstructEmailMessage(
                        emailBody,
                        emailSubject,
                        emailTo,
                        model.Email,
                        model.FirstName);
                //Send the message.
                SmtpClient client = new SmtpClient();
                // Add credentials if the SMTP server requires them.
                client.Credentials = CredentialCache.DefaultNetworkCredentials;

                try
                {
                    client.Send(email);
                    return "true";
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught in CreateMessageWithAttachment(): {0}",
                          ex.ToString());
                    return ex.ToString();
                }

            }
        }

        public ActionResult GetValidateCode()
        {
            Random oRnd;
            ValidateCode vCode = new ValidateCode();
            string code = vCode.CreateCode(5, out oRnd);
            Session["ValidateCode"] = code;
            var bytes = vCode.CreateValidateCode(5, 280, 60, 22, code, oRnd);
            return File(bytes, @"image/jpeg");
        }

        public ActionResult CheckCaptcha(string captcha)
        {
            var code = Session["ValidateCode"].ToString();
            if (Request["captcha"] == code)
            {
                return Json(true);
            }
            return Json(false);
        }

        /// <summary>
        /// Adds the email signup.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult AddEmailSignup(EmailSignupModel model)
        {
            try
            {
                var emailOnlyItem = (Item)Factory.GetDatabase("web").GetItem(ItemGuids.EmailOnlyItem);
                var emailAndPostal = (Item)Factory.GetDatabase("web").GetItem(ItemGuids.EmailPostalAddressItem);
                string strRegex = @"^[a-zA-Z0-9_+.-]+\@([a-zA-Z0-9-]+\.)+[a-zA-Z0-9]{2,4}$";
                Regex re = new Regex(strRegex);
                if (!re.IsMatch(model.Email))
                {
                    return Content("Please input correct Email");
                }

                using (LandmarkEntities context = new LandmarkEntities())
                {
                    EmailSignup emailSignup = new EmailSignup
                    {
                        ID = Guid.NewGuid(),
                        Title = model.Title,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Channel = (model.Channel == "0") ? emailOnlyItem.Fields["Glossary Value"].ToString() : emailAndPostal.Fields["Glossary Value"].ToString(),
                        Interest = !string.IsNullOrEmpty(model.Others) ? model.Interests.Replace("," + model.Others, "") : model.Interests,
                        Other_Interest = model.Others,
                        Room = model.Room,
                        Building = model.Building,
                        Street = model.Street,
                        Area = model.Area,
                        State = model.State,
                        City = model.City,
                        Country = model.Country,
                        Postcode = model.Postcode,
                        District = model.District,
                        IpAddress = Request.UserHostAddress,
                        //Gender = model.Gender,
                        OptIn = (model.OptIn == 1) ? true : false,
                        CreatedOn = DateTime.Now
                    };
                    context.EmailSignups.Add(emailSignup);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            return RedirectToAction("ButtonRedirect", new { targetId = ItemGuids.EmailSignUpThankYouPage });
        }

    }
}
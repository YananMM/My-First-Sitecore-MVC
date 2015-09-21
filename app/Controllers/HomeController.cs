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
            return Redirect(Sitecore.Links.LinkManager.GetItemUrl(target));
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
            return Redirect(Sitecore.Links.LinkManager.GetItemUrl(target) + "?type=" + type + "&page=1");
        }

        public ActionResult DirectByPager(string targetId, int page)
        {
            Item target = Sitecore.Context.Database.GetItem(targetId);
            return Redirect(Sitecore.Links.LinkManager.GetItemUrl(target) + "?page=" + page);
        }

        public ActionResult AddCustomerMessage(ContactUsFormModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = AddContact(model);
                    if (result == "1")
                    {
                        return RedirectToAction("ButtonRedirect", new { targetId = ItemGuids.ThankYouPage });
                    }
                    else
                    {
                        return Content(result);
                        //return RedirectToAction("ButtonRedirect", new { targetId = ItemGuids.ContactUsPage });
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
                string strRegex = @"^[_\.0-9a-z-]+@([0-9a-z][0-9a-z-]+\.){1,4}[a-z]{2,3}$";
                Regex re = new Regex(strRegex);
                if (re.IsMatch(model.Email))
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
            else if (code != model.ValidateCode)
            {
                return "Validate code is error";
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
                        "",
                        "");
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
            ViewBag.Code = code;
            var bytes = vCode.CreateValidateCode(5, 280, 60, 22);
            return File(bytes, @"image/jpeg");
        }


        public ActionResult AddEmailSignup(EmailSignupModel model)
        {
            if (ModelState.IsValid)
            {
                using (LandmarkSitecore_MasterEntities context = new LandmarkSitecore_MasterEntities())
                {
                    EmailSignup emailSignup = new EmailSignup
                    {
                        ID = Guid.NewGuid(),
                        Title = model.Title,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Channel = model.Channel,
                        Interest = model.Interests,
                        Room = model.Room,
                        Building = model.Building,
                        Street = model.Street,
                        Area = model.Area,
                        State = model.State,
                        City = model.City,
                        Country = model.Country,
                        Postcode = model.Postcode,
                        District = model.District,
                    };
                    context.EmailSignups.Add(emailSignup);
                    context.SaveChanges();
                }
            }

            return View();
        }

    }
}
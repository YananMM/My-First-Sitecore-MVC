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
using System.Web;
using System.Web.Mvc;
using Landmark.Classes;
using Sitecore.Data.Items;
using Landmark.Models;
using Landmark.Helper;

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
        /// <param name="experienceType">Type of the experience.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult ButtonRedirect(string targetId, string experienceType)
        {
            Item target = Sitecore.Context.Database.GetItem(targetId);
            return Redirect(Sitecore.Links.LinkManager.GetItemUrl(target) + "?experienceType=" + experienceType);
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
                using (LandmarkSitecore_MasterEntities context = new LandmarkSitecore_MasterEntities())
                {
                    ContactUsForm customer = new ContactUsForm()
                    {
                        ID = Guid.NewGuid(),
                        Title = model.Title,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Telephone = model.Telephone,
                        Email = model.Email,
                        EnquiryType = model.EnquiryType,
                        Message = model.Message,
                    };
                    context.ContactUsForms.Add(customer);
                    var result = context.SaveChanges();
                    return result.ToString();
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

       

    }
}
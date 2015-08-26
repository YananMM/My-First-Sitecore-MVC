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

        public ActionResult AddCustomerMessage(ContactUsFormModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (LandmarkSitecore_MasterEntities context = new LandmarkSitecore_MasterEntities())
                    {
                        ContactUsForm customer = new ContactUsForm()
                        {
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
                        return Content(result.ToString());
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
            if (string.IsNullOrEmpty(model.Email))
            {
                return "Email can not be empty";
            }
            else if (string.IsNullOrEmpty(model.LastName))
            {

            }
            using (LandmarkSitecore_MasterEntities context = new LandmarkSitecore_MasterEntities())
            {
                ContactUsForm customer = new ContactUsForm()
                {
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
}
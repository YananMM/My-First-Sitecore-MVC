// ***********************************************************************
// Assembly         : Landmark
// Author           : Jiang Lili
// Created          : 08-26-2015
//
// Last Modified By : Jiang Lili
// Last Modified On : 08-26-2015
// ***********************************************************************
// <copyright file="Customers.cs" company="Gruden">
//     Copyright (c) Gruden. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Landmark.Models
{
    /// <summary>
    /// Class Customers.
    /// </summary>
    public class ContactUsFormModel
    {
        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>The unique identifier.</value>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the telephone.
        /// </summary>
        /// <value>The telephone.</value>
        public string Telephone { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the type of the enquiry.
        /// </summary>
        /// <value>The type of the enquiry.</value>
        [Required]
        public string EnquiryType { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        [Required]
        public string Message { get; set; }
    }
}
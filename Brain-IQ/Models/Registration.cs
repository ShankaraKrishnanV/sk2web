
/// <summary>
/// Registration Model 
/// 
/// </summary>
namespace Brain_IQ.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.ComponentModel.DataAnnotations;
    public class Registration
    {
        /// <summary>
        /// get or set the UID
        /// </summary>
        public int UID { get; set; }

        /// <summary>
        /// get or set the UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// get or set the Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// get or set the FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// get or set the LastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// get or set the LastName
        /// </summary>
        public int SchoolID { get; set; }

        /// <summary>
        /// get or set the PhoneNumber
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// get or set the EmailId
        /// </summary>
        public string EmailId { get; set; }

        /// <summary>
        /// get or set the PostalCode
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// get or set the PostalCode
        /// </summary>
        public int StandardId { get; set; }

        /// <summary>
        /// get or set the QualificationId
        /// </summary>
        public int QualificationId { get; set; }

        /// <summary>
        /// get or set the QualificationOthers
        /// </summary>
        public string QualificationOthers { get; set; }

        /// <summary>
        /// get or set the DOB
        /// </summary>
        public string DOB { get; set; }

        /// <summary>
        /// get or set the UserType
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// get or set the Country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// get or set the State
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// get or set the PrimaryUser
        /// </summary>
        public bool PrimaryUser { get; set; }

        public string ProfilePicture { get; set; }

        public string About { get; set; }

        public string Gender { get; set; }

        public bool Active { get; set; }

        public string CreatedOn { get; set; }

        public string Address { get; set; }

        public string Fax { get; set; }

        public string Location { get; set; }

    }
}
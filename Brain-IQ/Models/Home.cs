using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace BrainIQ.Models
{
    public class Home
    {

        #region Property

        ///// <summary>
        ///// get or set the UserId
        ///// </summary>
        //public int UserId { get; set; }
        public int UID { get; set; }

        /// <summary>
        /// get or set the UserName
        /// </summary>
        [Required(ErrorMessage = "User Name is required")]
        [Display(Name = "User Name")]
        [StringLength(50)]
        public string UserName { get; set; }

        /// <summary>
        /// get or set the Password
        /// </summary>
        [Required(ErrorMessage = "Password is Required")]
        [Display(Name = "Password")]
        [StringLength(50)]
        public string Password { get; set; }

        [Display(Name = "")]
        public string ErrorLabel { get; set; }

        ///// <summary>
        ///// get or set the Active
        ///// </summary>
        //public bool Active { get; set; }

        ///// <summary>
        ///// get or set the RememberMe
        ///// </summary>
        //public bool RememberMe { get; set; }

        /// <summary>
        /// get or set the UserType
        /// </summary>
        public int UserType { get; set; }

        public string EmailId { get; set; }

        public int SchoolID { get; set; }

        public string Location { get; set; }

        public int StandardId { get; set; }

        public DateTime expire_on { get; set; }

        #endregion


    }
}
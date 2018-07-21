using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brain_IQ.Models
{
    public class UserType
    {
        public string UserTypeId { get; set; }
        public string Type { get; set; }

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
    }
}
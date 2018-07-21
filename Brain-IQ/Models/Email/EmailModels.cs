using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brain_IQ.Models.Email
{
    public class EmailModels
    {

        #region "Property"

        public int ID { get; set; }

        public int SchoolID { get; set; }

        public int MessageBoxID { get; set; }

        public int SentBy { get; set; }

        public string SentByName { get; set; }

        public int SentTo { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public int MoveTo { get; set; }

        public string FolderName { get; set; }

        public bool IsFavorite { get; set; }

        public DateTime SendDate { get; set; }

        public string SendTime { get; set; }

        public bool ReadContent { get; set; }

        public bool Deleted { get; set; }

        #endregion

        #region "Property - Invitations"

        public int InvitationID { get; set; }

        public string InvitationTitle { get; set; }

        public string InvitationImage { get; set; }

        public string Description { get; set; }

        public int PostedBy { get; set; }

        public DateTime PostedOn { get; set; }

        public string PostedTime { get; set; }

        public int Privacy { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePicture { get; set; }

        #endregion
    }
}
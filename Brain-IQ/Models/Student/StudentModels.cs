using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brain_IQ.Models.Student
{
    public class StudentModels
    {
        #region "Property"

        public int _LOCALID { get; set; }

        public int SchoolID { get; set; }

        public int ExamId { get; set; }

        public int UsertType { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePicture { get; set; }

        public string PhoneNo { get; set; }

        public string ExamName { get; set; }

        public string Description { get; set; }

        public int SubjectId { get; set; }

        public string Subject { get; set; }

        public string SubjectDescription { get; set; }

        public int ChapterId { get; set; }

        public string ChapterName { get; set; }

        public string ChapterDescription { get; set; }

        public int LoginUserID { get; set; }

        public bool Active { get; set; }

        public string Email { get; set; }

        public string PostelCode { get; set; }

        public string DOB { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public bool ChapterCovered { get; set; }

        public string Address { get; set; }

        public string VoterList { get; set; }

        public string MarkedOn { get; set; }

        public string MarkedTime { get; set; }

        #endregion
    }
}
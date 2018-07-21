using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brain_IQ.Models.Attendance
{
    public class AttendanceModels
    {

        #region "Property"

        public int ID { get; set; }

        public int UID { get; set; }

        public int SchoolID { get; set; }

        public int StandardID { get; set; }

        public int StduentID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePicture { get; set; }

        public bool MarkAttendance { get; set; }

        public DateTime AttedendanceDate { get; set; }

        public string Reason { get; set; }

        public bool GotPermission { get; set; }

        public int UserType { get; set; }

        public string Subject { get; set; }

        public string Chapter { get; set; }

        public int Marks { get; set; }

        public string TotalTime { get; set; }

        public string TimeTaken { get; set; }

        public string ExamDate { get; set; }

        public string AttedenceMarkedBy { get; set; }
        
        public string State { get; set; }

        public string Country { get; set; }

        public string DateOfBirth { get; set; }

        public string PostalCode { get; set; }

        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public string Address { get; set; }

        public bool IsLeader { get; set; }

        public bool DisplayLeaderSection { get; set; }

        public int NominattedUserID { get; set; }

        public string NominattedUsername { get; set; }

        #endregion

    }
}
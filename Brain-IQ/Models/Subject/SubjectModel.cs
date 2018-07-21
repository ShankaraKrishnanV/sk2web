using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brain_IQ.Models.Subject
{
    public class SubjectModel
    {

        #region "Property"

        /// <summary>
        /// get or set the UserId
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// get or set the SubjectId
        /// </summary>
        public int SubjectId { get; set; }

        /// <summary>
        /// get or set the StandardId
        /// </summary>
        public int StandardId { get; set; }

        public string StandardName { get; set; }

        /// <summary>
        /// get or set the Subject
        /// </summary>
        public string Subject { get; set; }
        
        /// <summary>
        /// get or set the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// get or set the ChapterId
        /// </summary>
        public int ChapterId { get; set; }

        /// <summary>
        /// get or set the Chapter
        /// </summary>
        public string Chapter { get; set; }

        /// <summary>
        /// get or set the Active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// get or set the MarkedChapterId
        /// </summary>
        public int MarkedChapterId { get; set; }

        /// <summary>
        /// get or set the MarkedSubjectId
        /// </summary>
        public int MarkedSubjectId { get; set; }

        /// <summary>
        /// get or set the Marks
        /// </summary>
        public int Marks { get; set; }

        /// <summary>
        /// get or set the SubjectDescription
        /// </summary>
        public string SubjectDescription { get; set; }

        /// <summary>
        /// get or set the ChapterName
        /// </summary>
        public string ChapterName { get; set; }

        /// <summary>
        /// get or set the ChapterDescription
        /// </summary>
        public string ChapterDescription { get; set; }

        /// <summary>
        /// get or set the Date
        /// </summary>
        public string Date { get; set; }

        public string TimeStart { get; set; }

        public string TimeFinished { get; set; }

        public string LoginUserID { get; set; }
        
        public int ExamID { get; set; }

        public string ExamName { get; set; }

        public int NoOfQuestions { get; set; }

        public string ExamTime { get; set; }

        public int TotalQuestions { get; set; }

        public int ExamId { get; set; }

        #endregion  

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brain_IQ.Models.Exam
{
    public class ChapterExamModels
    {
        #region "Property"

        /// <summary>
        /// get or set the UserId
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// get or set the QuestionId
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// get or set the ChapterId
        /// </summary>
        public int ChapterId { get; set; }

        /// <summary>
        /// get or set the Question
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string QuestionImage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string QuestionDescription { get; set; }

        /// <summary>
        /// get or set the OptionA
        /// </summary>
        public string OptionA { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OptionAImage { get; set; }

        /// <summary>
        /// get or set the OptionB
        /// </summary>
        public string OptionB { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OptionBImage { get; set; }

        /// <summary>
        /// get or set the OptionC
        /// </summary>
        public string OptionC { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OptionCImage { get; set; }

        /// <summary>
        /// get or set the OptionD
        /// </summary>
        public string OptionD { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OptionDImage { get; set; }

        /// <summary>
        /// get or set the Answer
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FillAnswer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AnswerDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SubjectId { get; set; }

        /// <summary>
        /// get or set the Marks
        /// </summary>
        public int Marks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TimeStart { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TimeFinished { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int StandardId { get; set; }

        public string Format { get; set; }

        #endregion
    }
}
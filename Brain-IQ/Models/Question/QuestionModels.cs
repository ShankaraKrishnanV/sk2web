using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Brain_IQ.Models.Question
{
    public class QuestionModels
    {

        #region "Property"

        public int id { get; set; }

        public int QuestionId { get; set; }

        public int SchoolID { get; set; }

        public int AnsweredQuestionId { get; set; }

        public int StandardId { get; set; }

        public int SubjectId { get; set; }

        public int ChapterId { get; set; }

        public string Question { get; set; }

        public string Question_Image { get; set; }

        public string Question_Image_Description { get; set; }

        public string OptionA { get; set; }

        public string OptionA_Image { get; set; }

        public string OptionB { get; set; }

        public string OptionB_Image { get; set; }

        public string OptionC { get; set; }

        public string OptionC_Image { get; set; }

        public string OptionD { get; set; }

        public string OptionD_Image { get; set; }

        public string Answer { get; set; }

        public bool Active { get; set; }

        public string QuestionHidden { get; set; }

        public string OptionAHidden { get; set; }

        public string OptionBHidden { get; set; }

        public string OptionCHidden { get; set; }

        public string OptionDHidden { get; set; }

        public string Subject { get; set; }

        public string ChapterName { get; set; }

        public string AnswerDescription { get; set; }

        public int UserId { get; set; }

        public int TotalCount { get; set; }

        public string AnswerSelected { get; set; }

        public string TimeStart { get; set; }

        public string TimeFinished { get; set; }

        public int Format { get; set; }

        public string FillAnswer { get; set; }

        public string ChapterIDList { get; set; }

        public string ExamTime { get; set; }

        public string NoofQuestions { get; set; }

        public string ExamName { get; set; }

        public int ExamID { get; set; }


        #endregion


    }
}
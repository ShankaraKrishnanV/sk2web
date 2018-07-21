using Brain_IQ.Models.Exam;
using Brain_IQ.Models.Question;
using Brain_IQ.Models.Student;
using Brain_IQ.Models.Subject;
using Brain_IQ.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Brain_IQ.Controllers
{
    public class SubjectsController : Controller
    {

        public AppKey appKey = new AppKey();

        public ActionResult Redirects(int ExamId)
        {
            return RedirectToAction("Index", "Subjects", new { ExamId = ExamId });
        }

        // GET: Subject
        public ActionResult Index(int ExamId)
        {
            try
            {
                List<SubjectModel> listsubject = new List<SubjectModel>();
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                httpClient.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.GetAsync("subject/GetSubject?ExamId=" + ExamId).Result;
                if (response.IsSuccessStatusCode)
                    listsubject = JsonConvert.DeserializeObject<List<SubjectModel>>(response.Content.ReadAsStringAsync().Result);
                ViewBag.getSubject = listsubject;
                return View();

            }
            catch (Exception ex)
            {
                string error = ex.ToString().Trim();
                return RedirectToAction("logins", "logins");
            }

        }

        public ActionResult Exams()
        {
            try
            {
                List<SubjectModel> listStandardList = new List<SubjectModel>();
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                httpClient.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.GetAsync("Student/GetExamLookup").Result;
                if (response.IsSuccessStatusCode)
                    listStandardList = JsonConvert.DeserializeObject<List<SubjectModel>>(response.Content.ReadAsStringAsync().Result);
                ViewBag.getChapterList = listStandardList;
                return View();
            }
            catch (Exception ex)
            {
                string error = ex.ToString().Trim();
                return RedirectToAction("logins", "logins");
            }
        }

        /// <summary>
        /// Get Chapter list based on subject
        /// </summary>
        /// <param name="SubjectId"></param>
        /// <returns></returns>
        public ActionResult subjectlist(int subjectId)
        {
            try
            {
                int SID = Convert.ToInt32(Session["ExamID"]);
                int STD_ID = Convert.ToInt32(Session["StandardID"]);
                int UserId = Convert.ToInt32(Session["UserId"]);
                List<SubjectModel> listsubject = new List<SubjectModel>();
                List<SubjectModel> listAnswered = new List<SubjectModel>();
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                httpClient.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.GetAsync("subject/GetChapter?SubjectId=" + subjectId + "&UserId=" + UserId).Result;
                if (response.IsSuccessStatusCode)
                    listsubject = JsonConvert.DeserializeObject<List<SubjectModel>>(response.Content.ReadAsStringAsync().Result);
                ViewBag.getChapter = listsubject;
                Session["exam_SubjectID"] = subjectId;
                return View("Chapter");
            }
            catch (Exception ex)
            {
                string error = ex.ToString().Trim();
                return RedirectToAction("logins", "logins");
            }

        }

        /// <summary>
        /// Get Chapter list based on subject
        /// </summary>
        /// <param name="SubjectId"></param>
        /// <returns></returns>
        public ActionResult exam(int examId, string STime)
        {
            this.Session["ExamId"] = examId;
            Session["exam_ChapterID"] = examId;
            this.Session["ExamTime"] = STime;
            return View("Questions");
        }

        /// <summary>
        /// get questions 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetQuestions()
        {
            try
            {
                List<ChapterExamModels> questionList = new List<ChapterExamModels>();
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                httpClient.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.GetAsync("chapterExam/GetChapterExams?ExamId=" + Convert.ToInt32(Session["ExamId"])).Result;
                if (response.IsSuccessStatusCode)
                    questionList = JsonConvert.DeserializeObject<List<ChapterExamModels>>(response.Content.ReadAsStringAsync().Result);
                ViewBag.getQuestions = questionList;
                return Json(questionList, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                string error = ex.ToString().Trim();
                return RedirectToAction("logins", "logins");
            }

        }

        /// <summary>
        /// get questions 
        /// </summary>
        /// <returns></returns>
        public ActionResult Update(List<QuestionModels> questionModels)
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            int ChapterId = Convert.ToInt32(Session["ChapterId"]);
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(appKey.GetapiURL());
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = httpClient.PostAsJsonAsync("chapterExam/SaveResult", questionModels).Result;
            return Json(response.IsSuccessStatusCode, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Goto Result page
        /// </summary>
        /// <returns></returns>
        public ActionResult Result()
        {
            return View();
        }

        /// <summary>
        /// Goto Question review page
        /// </summary>
        /// <returns></returns>
        public ActionResult review()
        {
            try
            {
                List<QuestionModels> listsubject = new List<QuestionModels>();
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                httpClient.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                //var response = httpClient.GetAsync("Questions/GetAnswerToReview?UserId=" + Convert.ToInt32(Session["UserId"])).Result;
                var response = httpClient.GetAsync("Questions/GetAnswerToReview?UserId=" + Convert.ToInt32(Session["UserId"]) + "&ChapterId=" + Session["exam_ChapterID"]).Result;
                if (response.IsSuccessStatusCode)
                    listsubject = JsonConvert.DeserializeObject<List<QuestionModels>>(response.Content.ReadAsStringAsync().Result);
                ViewBag.getSubject = listsubject;
                return View();

            }
            catch (Exception ex)
            {
                string error = ex.ToString().Trim();
                return RedirectToAction("logins", "logins");
            }

        }

        public ActionResult reviews(int chapterId)
        {
            this.Session["ChapterId"] = chapterId;
            try
            {
                List<QuestionModels> listsubject = new List<QuestionModels>();
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                httpClient.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                //var response = httpClient.GetAsync("Questions/GetAnswerToReview?UserId=" + Convert.ToInt32(Session["UserId"])).Result;
                var response = httpClient.GetAsync("Questions/GetAnswerToReview?UserId=" + Convert.ToInt32(Session["UserId"]) + "&ChapterId=" + Session["ChapterId"]).Result;
                if (response.IsSuccessStatusCode)
                    listsubject = JsonConvert.DeserializeObject<List<QuestionModels>>(response.Content.ReadAsStringAsync().Result);
                ViewBag.getSubject = listsubject;
                return View("Review");

            }
            catch (Exception ex)
            {
                string error = ex.ToString().Trim();
                return RedirectToAction("logins", "logins");
            }
        }

        public ActionResult viewsubjects()
        {
            return View();
        }

        public ActionResult viewsubjectsList()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["ExamID"])))
            {
                StudentModels qModel = new StudentModels();
                int SchoolID = Convert.ToInt32(Session["ExamID"]);
                try
                {
                    List<StudentModels> listSubjectTree = new List<StudentModels>();
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                    httpClient.DefaultRequestHeaders.Accept.Add(
                       new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = httpClient.GetAsync("Student/subjectTree?SchoolID=" + SchoolID).Result;
                    if (response.IsSuccessStatusCode)
                        listSubjectTree = JsonConvert.DeserializeObject<List<StudentModels>>(response.Content.ReadAsStringAsync().Result);
                    ViewBag.getSubjectTree = listSubjectTree;
                    return Json(listSubjectTree, JsonRequestBehavior.AllowGet);
                    //return View();
                }
                catch (Exception ex)
                {
                    string error = ex.ToString().Trim();
                    return RedirectToAction("logins", "logins");
                }
            }
            else
            {
                return RedirectToAction("logins", "logins");
            }
        }
    }
}
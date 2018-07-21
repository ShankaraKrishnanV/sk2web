using Brain_IQ.Models.Exam;
using Brain_IQ.Models.Question;
using Brain_IQ.Models.Subject;
using Brain_IQ.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Brain_IQ.Controllers.Question
{

    public class QuestionController : Controller
    {
        public AppKey appKey = new AppKey();

        #region "PAGE - Question"

        /// <summary>
        /// Add new question
        /// </summary>
        /// <returns></returns>
        public ActionResult AddQuestion(string Hide)
        {
            Session["EnableView"] = Hide;
            return View();
        }

        #endregion

        #region "GET - Section List"

        /// <summary>
        /// Show list of Subjects
        /// </summary>
        /// <returns></returns>
        public ActionResult StandardsList()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["ExamID"])))
            {
                int SchoolID = Convert.ToInt32(Session["ExamID"]);
                try
                {
                    List<SubjectModel> listStandardList = new List<SubjectModel>();
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                    httpClient.DefaultRequestHeaders.Accept.Add(
                       new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = httpClient.GetAsync("Student/GetStandarsLookup?SchoolID=" + SchoolID).Result;
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
            else
            {
                return RedirectToAction("logins", "logins");
            }

        }

        public ActionResult GetAjaxStandardsList()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["ExamID"])))
            {
                int SchoolID = Convert.ToInt32(Session["ExamID"]);
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
                    return Json(listStandardList, JsonRequestBehavior.AllowGet);
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

        #endregion

        #region "GET - Subject List"

        /// <summary>
        /// Show list of Subjects
        /// </summary>
        /// <returns></returns>
        public ActionResult SubjectList(int SubjectId)
        {
            try
            {
                List<SubjectModel> listsubject = new List<SubjectModel>();
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                httpClient.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.GetAsync("Student/GetSubjectLookup?SubjectId=" + Convert.ToInt32(SubjectId) + "&SchoolID=" + Convert.ToString(Session["ExamID"])).Result;
                if (response.IsSuccessStatusCode)
                    listsubject = JsonConvert.DeserializeObject<List<SubjectModel>>(response.Content.ReadAsStringAsync().Result);
                ViewBag.getSubject = listsubject;
                this.Session["subjectList"] = listsubject;
                return View();

            }
            catch (Exception ex)
            {
                string error = ex.ToString().Trim();
                return RedirectToAction("logins", "logins");
            }

        }

        public ActionResult GetAjaxSubjectList(int SubjectId)
        {
            try
            {
                this.Session["StandardId"] = SubjectId;
                List<SubjectModel> listsubject = new List<SubjectModel>();
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                httpClient.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.GetAsync("Student/GetSubjectLookup?SubjectId=" + Convert.ToInt32(SubjectId) + "&SchoolID=" + Convert.ToString(Session["ExamID"])).Result;
                if (response.IsSuccessStatusCode)
                    listsubject = JsonConvert.DeserializeObject<List<SubjectModel>>(response.Content.ReadAsStringAsync().Result);
                ViewBag.getSubject = listsubject;
                return Json(listsubject, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                string error = ex.ToString().Trim();
                return RedirectToAction("logins", "logins");
            }

        }

        #endregion

        #region "GET - Chapter List"

        /// <summary>
        /// Show list of Subjects
        /// </summary>
        /// <returns></returns>
        public ActionResult ChapterList(int SubjectId)
        {
            Session["SubjectId"] = SubjectId;
            try
            {
                List<SubjectModel> listChapter = new List<SubjectModel>();
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                httpClient.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.GetAsync("Student/GetChapterLookup?SubjectId=" + Convert.ToInt32(SubjectId) + "&Params=0&SchoolID=" + Convert.ToString(Session["ExamID"]) + "").Result;
                if (response.IsSuccessStatusCode)
                    listChapter = JsonConvert.DeserializeObject<List<SubjectModel>>(response.Content.ReadAsStringAsync().Result);
                ViewBag.getChapter = listChapter;
                return View();

            }
            catch (Exception ex)
            {
                string error = ex.ToString().Trim();
                return RedirectToAction("logins", "logins");
            }

        }

        public ActionResult GetAjaxChapterList(int SubjectId)
        {
            try
            {
                this.Session["SubjectId"] = SubjectId;
                List<SubjectModel> listChapter = new List<SubjectModel>();
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                httpClient.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.GetAsync("Student/GetChapterLookup?SubjectId=" + Convert.ToInt32(SubjectId) + "&Params=0&SchoolID=" + Convert.ToString(Session["ExamID"])).Result;
                if (response.IsSuccessStatusCode)
                    listChapter = JsonConvert.DeserializeObject<List<SubjectModel>>(response.Content.ReadAsStringAsync().Result);
                ViewBag.getChapter = listChapter;
                return Json(listChapter, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                string error = ex.ToString().Trim();
                return RedirectToAction("logins", "logins");
            }

        }

        /// <summary>
        /// getting the chapter list, which having exam question
        /// Coz, if the chapter has the question, only then we can generate the question
        /// </summary>
        /// <param name="SubjectId"></param>
        /// <returns></returns>
        public ActionResult GetQuestionBasedChapterList(int SubjectId)
        {
            int SID = Convert.ToInt32(Session["ExamID"]);
            int STDID = Convert.ToInt32(Session["StandardId"]);
            if (!string.IsNullOrEmpty(Convert.ToString(SID)) && SID != 0)
            {
                try
                {
                    this.Session["SubjectId"] = SubjectId;
                    List<ChapterExamModels> listChapter = new List<ChapterExamModels>();
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                    httpClient.DefaultRequestHeaders.Accept.Add(
                       new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = httpClient.GetAsync("chapterExam/ChapterList?SchoolID=" + SID + "&StandardID=" + STDID + "&SubjectID=" + Convert.ToInt32(SubjectId)).Result;
                    if (response.IsSuccessStatusCode)
                        listChapter = JsonConvert.DeserializeObject<List<ChapterExamModels>>(response.Content.ReadAsStringAsync().Result);
                    ViewBag.getChapter = listChapter;
                    return Json(listChapter, JsonRequestBehavior.AllowGet);

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


        #endregion

        #region "ASSIGN - Store Chapter List"

        public ActionResult StoreChapter(int ChapterId)
        {
            this.Session["ChapterId"] = ChapterId;
            if (ChapterId > 0)
                return Json(ChapterId, JsonRequestBehavior.AllowGet);
            else
                return RedirectToAction("logins", "logins");
        }


        #endregion

        #region "SAVE QUESTIONS"

        /// <summary>
        /// SAVE QUESTIONS
        /// </summary>
        /// <param name="questionModels"></param>
        /// <returns></returns>
        public ActionResult CreateQuestion(QuestionModels questionModels)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
            {
                int UserId = Convert.ToInt32(Session["UserId"]);
                int ChapterId = Convert.ToInt32(Session["ChapterId"]);
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(appKey.GetapiURL());
                client.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent content = new StringContent(questionModels.ToString(), Encoding.UTF8, "application/json");
                var response = client.PostAsJsonAsync("Questions/CreateQuestions", questionModels).Result;
                if (response.IsSuccessStatusCode)
                    return Json(response.IsSuccessStatusCode, JsonRequestBehavior.AllowGet);

                else
                    return RedirectToAction("logins", "logins");
            }
            else
            {
                return RedirectToAction("logins", "logins");
            }
        }

        #endregion

        #region "View Questions"

        public ActionResult ViewQuestions()
        {
            try
            {
                List<QuestionModels> listQuestions = new List<QuestionModels>();
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.GetAsync("Questions/View?ExamId=" + Convert.ToInt32(Session["StandardId"]) + "&SubjectId=" + Convert.ToInt32(Session["SubjectId"]) + "&ChapterId=" + Convert.ToInt32(Session["ChapterId"])).Result;
                if (response.IsSuccessStatusCode)
                    listQuestions = JsonConvert.DeserializeObject<List<QuestionModels>>(response.Content.ReadAsStringAsync().Result);
                if (listQuestions.Count > 0)
                {
                    ViewBag.DispStandard = listQuestions[0].ExamName;
                    ViewBag.DispSubject = listQuestions[0].Subject;
                    ViewBag.DispChapterName = listQuestions[0].ChapterName;
                    ViewBag.getQuestions = listQuestions;
                    ViewBag.Check = 1;
                }
                else
                {
                    ViewBag.Check = 0;
                }
                return View();
            }
            catch (Exception ex)
            {
                string error = ex.ToString().Trim();
                return RedirectToAction("logins", "logins");
            }
        }

        #endregion

        #region "Get chapterlist based on standard"
        /// <summary>
        /// Show list of Subjects
        /// </summary>
        /// <returns></returns>
        public ActionResult createquestionpaper()
        {
            int SID = Convert.ToInt32(Session["ExamID"]);
            if (SID != 0)
                return View();
            else
                return RedirectToAction("logins", "logins");

        }

        #endregion

        /// <summary>
        /// Move the questions from temporary table to staging table
        /// </summary>
        /// <param name="Questions"></param>
        /// <returns></returns>
        public ActionResult GenerateQuestions(QuestionModels Questions)
        {
            int SID = Convert.ToInt32(Session["ExamID"]);
            if (SID != 0)
            {
                Questions.SchoolID = SID;
                Questions.UserId = Convert.ToInt32(Session["UserId"]);

                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                httpClient.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent content = new StringContent(Questions.ToString(), Encoding.UTF8, "application/json");

                var response = httpClient.PostAsJsonAsync("Questions/GenerateQuestions", Questions).Result;

               
                if (response.IsSuccessStatusCode)
                {
                    return Json(response.IsSuccessStatusCode, JsonRequestBehavior.AllowGet);
                }
                else
                {
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
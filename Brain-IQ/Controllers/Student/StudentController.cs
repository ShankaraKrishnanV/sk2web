using Brain_IQ.Models;
using Brain_IQ.Models.Attendance;
using Brain_IQ.Models.Question;
using Brain_IQ.Models.Student;
using Brain_IQ.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Brain_IQ.Controllers.Student
{
    public class StudentController : Controller
    {

        public AppKey appKey = new AppKey();

        #region "Dashboard"

        // GET: Student
        /// <summary>
        /// Student Dashboard page
        /// </summary>
        /// <returns></returns>
        public ActionResult Dashboard()
        {
            return View();
        }

        #endregion

        #region "Profile"

        /// <summary>
        /// Profile page
        /// </summary>
        /// <returns></returns>
        public ActionResult Profile()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["ExamID"])))
            {
                int SchoolID = Convert.ToInt32(Session["ExamID"]);
                try
                {
                    List<Registration> listUserList = new List<Registration>();
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                    httpClient.DefaultRequestHeaders.Accept.Add(
                       new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = httpClient.GetAsync("Register/GetLogin?UserName=" + this.Session["LoginUserName"] + "&Password=" + this.Session["Password"]).Result;
                    if (response.IsSuccessStatusCode)
                        listUserList = JsonConvert.DeserializeObject<List<Registration>>(response.Content.ReadAsStringAsync().Result);
                    ViewBag.getUserList = listUserList;
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

        #endregion

        #region "MarkAsComplete"

        /// <summary>
        /// Mark as complete for the chapter
        /// </summary>
        /// <param name="ChapterID"></param>
        /// <param name="StatusIS"></param>
        /// <returns></returns>
        public ActionResult MarkAsComplete(int ChapterID, int StatusIS)
        {
            try
            {
                List<QuestionModels> listsubject = new List<QuestionModels>();
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                httpClient.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.GetAsync("chapterExam/MarkChapter?ChapterID=" + ChapterID + "&StatusIS=" + StatusIS + "&UserID=" + Convert.ToInt32(Session["UserId"])).Result;
                if (response.IsSuccessStatusCode)
                    return Json(true, JsonRequestBehavior.AllowGet);
                else
                    return Json(false, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                string error = ex.ToString().Trim();
                return RedirectToAction("logins", "logins");
            }

        }

        #endregion

        #region "Vote"

        /// <summary>
        /// Select leader
        /// </summary>
        /// <returns></returns>
        public ActionResult Vote()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["ExamID"])))
            {
                int SchoolID = Convert.ToInt32(Session["ExamID"]);
                int StdID = Convert.ToInt32(Session["Session_StandardID"]);
                int UserID = Convert.ToInt32(Session["UserId"]);
                try
                {
                    List<AttendanceModels> listLeaderList = new List<AttendanceModels>();
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                    httpClient.DefaultRequestHeaders.Accept.Add(
                       new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = httpClient.GetAsync("attendance/students?SchoolID=" + SchoolID + "&StdID=" + StdID + "&Type=1&UserID=" + UserID).Result;
                    if (response.IsSuccessStatusCode)
                        listLeaderList = JsonConvert.DeserializeObject<List<AttendanceModels>>(response.Content.ReadAsStringAsync().Result);
                    ViewBag.getLeaderList = listLeaderList;
                    ViewBag.DisplayLeaderSection = (listLeaderList[0]).DisplayLeaderSection;
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

        #endregion

        #region "Get eliminated student list"

        public ActionResult EliminateStudents(string UserIDList)
        {
            bool Status = false;
            if (!string.IsNullOrEmpty(Convert.ToString(Session["ExamID"])))
            {
                int SchoolID = Convert.ToInt32(Session["ExamID"]);
                int StandardID = Convert.ToInt32(Session["StandardID"]);
                try
                {
                    List<StudentModels> listVoterList = new List<StudentModels>();
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                    httpClient.DefaultRequestHeaders.Accept.Add(
                       new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = httpClient.GetAsync("Student/ElimatedUserID?SchoolID=" + SchoolID + "&stdID=" + StandardID + "&UserIDList=" + UserIDList + "&CreatedBy=" + Convert.ToInt32(Session["UserId"])).Result;
                    if (response.IsSuccessStatusCode)
                        Status = true;
                    else
                        Status = false;

                    return Json(Status, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    string error = ex.ToString().Trim();
                    return Json(error, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return RedirectToAction("logins", "logins");
            }
        }

        #endregion

        #region "Insert for --> Enable/Disable Voting Section"

        public ActionResult EnableVotingSection(string IsEnable)
        {
            bool Status = false;
            if (!string.IsNullOrEmpty(Convert.ToString(Session["ExamID"])))
            {
                int SchoolID = Convert.ToInt32(Session["ExamID"]);
                int StandardID = Convert.ToInt32(Session["StandardID"]);
                try
                {
                    List<StudentModels> listVoterList = new List<StudentModels>();
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                    httpClient.DefaultRequestHeaders.Accept.Add(
                       new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = httpClient.GetAsync("Student/EnableVoteSection?SchoolID=" + SchoolID + "&stdID=" + StandardID + "&IsEnable=" + IsEnable + "&CreatedBy=" + Convert.ToInt32(Session["UserId"])).Result;
                    if (response.IsSuccessStatusCode)
                        Status = true;
                    else
                        Status = false;
                    return Json(Status, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    string error = ex.ToString().Trim();
                    return Json(error, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return RedirectToAction("logins", "logins");
            }
        }

        #endregion

        #region "Insert votes by students"

        public ActionResult InsertVote(int NominattedVoterID)
        {
            bool Status = false;
            if (!string.IsNullOrEmpty(Convert.ToString(Session["ExamID"])))
            {
                int SchoolID = Convert.ToInt32(Session["ExamID"]);
                int StandardID = Convert.ToInt32(Session["StandardID"]);
                int LoginUser = Convert.ToInt32(Session["UserId"]);
                if (SchoolID != 0 && StandardID != 0 && LoginUser != 0 && NominattedVoterID != 0)
                {
                    try
                    {
                        List<StudentModels> listVoterList = new List<StudentModels>();
                        HttpClient httpClient = new HttpClient();
                        httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                        httpClient.DefaultRequestHeaders.Accept.Add(
                           new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = httpClient.GetAsync("Student/VoteStudent?SchoolID=" + SchoolID + "&stdID=" + StandardID + "&StudentID=" + LoginUser + "&NominaterID=" + NominattedVoterID).Result;
                        if (response.IsSuccessStatusCode)
                            Status = true;
                        else
                            Status = false;
                        return Json(Status, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        string error = ex.ToString().Trim();
                        return Json(error, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("Invalid data", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return RedirectToAction("logins", "logins");
            }
        }

        #endregion

    }
}
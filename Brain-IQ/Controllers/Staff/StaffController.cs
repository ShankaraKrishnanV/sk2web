using Brain_IQ.Models;
using Brain_IQ.Models.Attendance;
using Brain_IQ.Models.Email;
using Brain_IQ.Models.Subject;
using Brain_IQ.Settings;
using BrainIQ.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Brain_IQ.Controllers.Staff
{
    public class StaffController : Controller
    {

        public AppKey appKey = new AppKey();

        // GET: Staff
        public ActionResult Dashboard()
        {
            return View();
        }

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
                    {
                        listUserList = JsonConvert.DeserializeObject<List<Registration>>(response.Content.ReadAsStringAsync().Result);
                    }
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

        //public ActionResult SendMail()
        //{
        //    return View();
        //}
        public ActionResult SendMailList(string Type)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["ExamID"])))
            {
                int SchoolID = Convert.ToInt32(Session["ExamID"]);
                try
                {
                    List<EmailModels> listEmailList = new List<EmailModels>();
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                    httpClient.DefaultRequestHeaders.Accept.Add(
                       new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = httpClient.GetAsync("emailnotifications/getemails?SchoolID=" + this.Session["ExamID"] + "&UID=" + this.Session["UserId"] + "&Type=" + Type).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        listEmailList = JsonConvert.DeserializeObject<List<EmailModels>>(response.Content.ReadAsStringAsync().Result);
                        return Json(listEmailList, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(0, JsonRequestBehavior.AllowGet);
                    }
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

        /// <summary>
        /// save email list
        /// </summary>
        /// <param name="Subject"></param>
        /// <param name="Message"></param>
        /// <param name="TO_list_ID"></param>
        /// <param name="CC_list_ID"></param>
        /// <returns></returns>
        public ActionResult SaveEmailList(string Subject, string Message, string TO_list_ID, string CC_list_ID)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["ExamID"])))
            {
                int SID = Convert.ToInt32(Session["ExamID"]);
                int UID = Convert.ToInt32(Session["UserId"]);
                if (string.IsNullOrEmpty(Convert.ToString(Message)))
                    Message = string.Empty;
                if (string.IsNullOrEmpty(Convert.ToString(CC_list_ID)))
                    CC_list_ID = string.Empty;
                try
                {
                    List<EmailModels> listEmailList = new List<EmailModels>();
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                    httpClient.DefaultRequestHeaders.Accept.Add(
                       new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = httpClient.GetAsync("emailnotifications/insertMail?SID=" + SID + "&UID=" + UID + "&Subject=" + Subject + "&Message=" + Message + "&TO_list_ID=" + TO_list_ID + "&CC_list_ID=" + CC_list_ID).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return Json("true", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("false", JsonRequestBehavior.AllowGet);
                    }
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

        public ActionResult standard()
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
                    ViewBag.getStandardList = listStandardList;
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

        public ActionResult leader(int stdid)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["ExamID"])))
            {
                int SchoolID = Convert.ToInt32(Session["ExamID"]);
                int UserID = Convert.ToInt32(Session["UserId"]);
                Session["StandardID"] = stdid;
                try
                {
                    List<AttendanceModels> listStudentList = new List<AttendanceModels>();
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                    httpClient.DefaultRequestHeaders.Accept.Add(
                       new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = httpClient.GetAsync("attendance/students?SchoolID=" + SchoolID + "&StdID=" + stdid + "&Type=0&UserID=" + UserID).Result;
                    if (response.IsSuccessStatusCode)
                        listStudentList = JsonConvert.DeserializeObject<List<AttendanceModels>>(response.Content.ReadAsStringAsync().Result);
                    ViewBag.getStudentList = listStudentList;
                    ViewBag.DisplayLeaderSection = (listStudentList[0]).DisplayLeaderSection;
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

        /// <summary>
        /// Generate questions
        /// </summary>
        /// <returns></returns>
        public ActionResult generate()
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
                    ViewBag.getStandardList = listStandardList;
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

    }
}
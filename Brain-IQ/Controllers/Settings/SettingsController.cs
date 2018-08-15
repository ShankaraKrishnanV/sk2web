using Brain_IQ.Models.Attendance;
using Brain_IQ.Models.Student;
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

namespace Brain_IQ.Controllers.Settings
{
    public class SettingsController : Controller
    {

        public AppKey appKey = new AppKey();

        public ActionResult Home()
        {
            return View();
        }

        //DeleteAjaxStandrad
        public ActionResult DeleteAwarness(int ID)
        {
            try
            {
                List<SubjectModel> listsubject = new List<SubjectModel>();
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.GetAsync("settings/deleteAwarness?ID=" + ID).Result;
                if (response.IsSuccessStatusCode)
                    return Json("true", JsonRequestBehavior.AllowGet);
                else
                    return Json("false", JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                string error = ex.ToString().Trim();
                return RedirectToAction("logins", "logins");
            }
        }

        //InserAjaxStandrad
        public ActionResult SaveAwarness(string ID, string Title, string Description, string Active)
        {
            try
            {
                List<SettingsModels> lst = new List<SettingsModels>();
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                httpClient.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.GetAsync("settings/insertAwarness?ID=" + ID + "&Title=" + Title + "&Description=" + Description + "&Active=" + Active).Result;
                if (response.IsSuccessStatusCode)
                    return Json("true", JsonRequestBehavior.AllowGet);
                else
                    return Json("false", JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                string error = ex.ToString().Trim();
                return RedirectToAction("logins", "logins");
            }
        }

        public ActionResult InsertAwarness()
        {
            try
            {
                ViewBag.awarnessList = this.GetAwarnessList();
                return View();
            }
            catch (Exception ex)
            {
                string error = ex.ToString().Trim();
                return RedirectToAction("logins", "logins");
            }
        }
        private List<SettingsModels> GetAwarnessList()
        {
            List<SettingsModels> awarnessList = new List<SettingsModels>();
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(appKey.GetapiURL());
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = httpClient.GetAsync("settings/GetAwarnessList").Result;
            if (response.IsSuccessStatusCode)
                awarnessList = JsonConvert.DeserializeObject<List<SettingsModels>>(response.Content.ReadAsStringAsync().Result);
            return awarnessList;
        }

        public ActionResult Awarness()
        {
            try
            {
                ViewBag.awarnessList = this.GetAwarnessList();
                return View();

            }
            catch (Exception ex)
            {
                string error = ex.ToString().Trim();
                return RedirectToAction("logins", "logins");
            }
        }

        /// <summary>
        /// Get standarad list
        /// </summary>
        /// <returns></returns>
        public ActionResult Standard()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["ExamID"])))
            {
                int SchoolID = Convert.ToInt32(Session["ExamID"]);
                try
                {
                    List<SubjectModel> listExamList = new List<SubjectModel>();
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                    httpClient.DefaultRequestHeaders.Accept.Add(
                       new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = httpClient.GetAsync("Student/GetExamLookup").Result;
                    if (response.IsSuccessStatusCode)
                        listExamList = JsonConvert.DeserializeObject<List<SubjectModel>>(response.Content.ReadAsStringAsync().Result);
                    ViewBag.getStandardList = listExamList;
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


        public ActionResult selectstd()
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

        //Get subject list
        public ActionResult Subject(int ExamID)
        {
            Session["Exam_ID"] = ExamID;
            try
            {
                List<SubjectModel> listsubject = new List<SubjectModel>();
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                httpClient.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.GetAsync("subject/GetSubjectSTD?ExamID=" + Convert.ToInt32(ExamID)).Result;
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

        public ActionResult Chapter()
        {
            return View();
        }

        public ActionResult Student()
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

        public ActionResult studentlist(int stdid)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["ExamID"])))
            {
                int UserID = Convert.ToInt32(Session["UserId"]);
                Session["StandardID"] = stdid;
                try
                {
                    List<AttendanceModels> listExamList = new List<AttendanceModels>();
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                    httpClient.DefaultRequestHeaders.Accept.Add(
                       new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = httpClient.GetAsync("attendance/students?StdID=" + stdid + "&Type=0&UserID=" + UserID).Result;
                    if (response.IsSuccessStatusCode)
                        listExamList = JsonConvert.DeserializeObject<List<AttendanceModels>>(response.Content.ReadAsStringAsync().Result);
                    ViewBag.getStudentList = listExamList;
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

        //InserAjaxStandrad
        public ActionResult InsertStandard(int ExamID, string Name, string Desc)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["ExamID"])))
            {
                int UserID = Convert.ToInt32(Session["UserId"]);
                try
                {
                    List<SubjectModel> listsubject = new List<SubjectModel>();
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                    httpClient.DefaultRequestHeaders.Accept.Add(
                       new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = httpClient.GetAsync("Student/insertStd?ExamID=" + ExamID + "&Name=" + Name + "&Desc=" + Desc + "&UserID=" + UserID + "").Result;
                    if (response.IsSuccessStatusCode)
                        return Json("true", JsonRequestBehavior.AllowGet);
                    else
                        return Json("false", JsonRequestBehavior.AllowGet);

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


        //InserAjaxStandrad
        public ActionResult InsertSubject(int SubjectId, string Name, string Desc)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["ExamID"])))
            {
                int SchoolID = Convert.ToInt32(Session["ExamID"]);
                int UserID = Convert.ToInt32(Session["UserId"]);
                try
                {
                    List<SubjectModel> listsubject = new List<SubjectModel>();
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                    httpClient.DefaultRequestHeaders.Accept.Add(
                       new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = httpClient.GetAsync("Student/insertSubject?ExamID=" + Convert.ToInt32(Session["Exam_ID"]) + "&SubjectId=" + SubjectId + "&Name=" + Name + "&Desc=" + Desc + "&UserID=" + UserID + "").Result;
                    if (response.IsSuccessStatusCode)
                        return Json("true", JsonRequestBehavior.AllowGet);
                    else
                        return Json("false", JsonRequestBehavior.AllowGet);

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

        //InserAjaxStandrad
        public ActionResult InsertChapter(int ChapterId, string Name, string Desc)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["ExamID"])))
            {
                int SchoolID = Convert.ToInt32(Session["ExamID"]);
                int UserID = Convert.ToInt32(Session["UserId"]);
                try
                {
                    List<SubjectModel> listsubject = new List<SubjectModel>();
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                    httpClient.DefaultRequestHeaders.Accept.Add(
                       new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = httpClient.GetAsync("Student/insertChapter?ChapterId=" + ChapterId + "&SubjectId=" + Convert.ToInt32(Session["SubjectId"]) + "&Name=" + Name + "&Desc=" + Desc + "&UserID=" + UserID + "").Result;
                    if (response.IsSuccessStatusCode)
                        return Json("true", JsonRequestBehavior.AllowGet);
                    else
                        return Json("false", JsonRequestBehavior.AllowGet);

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

        //InserAjaxStandrad
        public ActionResult InsertStudent(StudentModels studentModels)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["ExamID"])))
            {
                studentModels.SchoolID = Convert.ToInt32(Session["ExamID"]);
                studentModels.LoginUserID = Convert.ToInt32(Session["UserId"]);
                //studentModels.StandardId = Convert.ToInt32(Session["StandardID"]);
                try
                {
                    List<SubjectModel> listsubject = new List<SubjectModel>();
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                    httpClient.DefaultRequestHeaders.Accept.Add(
                       new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(studentModels.ToString(), Encoding.UTF8, "application/json");

                    var response = httpClient.PostAsJsonAsync("Student/insertStudent", studentModels).Result;
                    if (response.IsSuccessStatusCode)
                        return Json("true", JsonRequestBehavior.AllowGet);
                    else
                        return Json("false", JsonRequestBehavior.AllowGet);

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


        //DeleteAjaxStandrad
        public ActionResult DeleteStandard(int StdID)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["ExamID"])))
            {
                int SchoolID = Convert.ToInt32(Session["ExamID"]);
                int UserID = Convert.ToInt32(Session["UserId"]);
                try
                {
                    List<SubjectModel> listsubject = new List<SubjectModel>();
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                    httpClient.DefaultRequestHeaders.Accept.Add(
                       new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = httpClient.GetAsync("Student/deleteStd?SchoolID=" + SchoolID + "&stdID=" + StdID + "").Result;
                    if (response.IsSuccessStatusCode)
                        return Json("true", JsonRequestBehavior.AllowGet);
                    else
                        return Json("false", JsonRequestBehavior.AllowGet);

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
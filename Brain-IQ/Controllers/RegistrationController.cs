using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Brain_IQ.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using System.Text;
using Newtonsoft.Json;
using Brain_IQ.Settings;

namespace Brain_IQ.Controllers
{
    public class RegistrationController : Controller
    {
        public AppKey appKey = new AppKey();

        #region "View"

        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region "Commanded"

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //public ActionResult Registration()
        //{
        //    Registration reg = new Registration();
        //    reg.DOB = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        //    TempData["notice"] = "";
        //    ViewBag.UserTypeList = GetUserType();
        //    return View(reg);
        //}

        #endregion

        #region "Registration"

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Registration(Registration registration)
        {

            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(appKey.GetapiURL());
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsync("/Registration/SaveRegistration", new StringContent(new JavaScriptSerializer().Serialize(registration), Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    bool status = JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);
                    if (status)
                        TempData["notice"] = "Successfully registered";
                    else
                        TempData["notice"] = "Failed to registered";
                }
            }
            ViewBag.UserTypeList = GetUserType();
            return View();
        }

        #endregion

        #region "UpdateAboutYourself"

        public ActionResult UpdateAboutYourself(string About)
        {
            bool Status = false;
            if (!string.IsNullOrEmpty(Convert.ToString(Session["ExamID"])))
            {
                int SchoolID = Convert.ToInt32(Session["ExamID"]);
                int UID = Convert.ToInt32(Session["UserId"]);
                int UType = Convert.ToInt32(Session["UserType"]);
                try
                {
                    List<Registration> listUserList = new List<Registration>();
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                    httpClient.DefaultRequestHeaders.Accept.Add(
                       new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = httpClient.GetAsync("Register/updateInfo?UID=" + UID + "&SchoolID=" + SchoolID + "&About=" + About + "&UType=" + UType).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        listUserList = JsonConvert.DeserializeObject<List<Registration>>(response.Content.ReadAsStringAsync().Result);
                        Status = true;
                    }
                    else
                        Status = false;
                    return Json(Status, JsonRequestBehavior.AllowGet);
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

        #region "UpdateBasicInfo"

        public ActionResult UpdateBasicInfo(Registration registrationModels)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["ExamID"])))
            {
                registrationModels.SchoolID = Convert.ToInt32(Session["ExamID"]);
                registrationModels.UID = Convert.ToInt32(Session["UserId"]);
                registrationModels.UserType = Convert.ToInt32(Session["UserType"]);
                registrationModels.About = string.Empty;
                registrationModels.PhoneNumber = string.Empty;
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.PostAsync("Register/UpdateBasicInfo", new StringContent(new JavaScriptSerializer().Serialize(registrationModels), Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    return Json("true", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("false", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return RedirectToAction("logins", "logins");
            }
        }

        #endregion

        #region "UpdateContactInfo"

        public ActionResult UpdateContactInfo(string ContactNo, string Fax)
        {
            bool Status = false;
            if (!string.IsNullOrEmpty(Convert.ToString(Session["ExamID"])))
            {
                int SchoolID = Convert.ToInt32(Session["ExamID"]);
                int UID = Convert.ToInt32(Session["UserId"]);
                int UType = Convert.ToInt32(Session["UserType"]);
                try
                {
                    List<Registration> listUserList = new List<Registration>();
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                    httpClient.DefaultRequestHeaders.Accept.Add(
                       new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = httpClient.GetAsync("Register/UpdateContactInfo?UID=" + UID + "&SchoolID=" + SchoolID + "&ContactNo=" + ContactNo + "&UType=" + UType + "&Fax=" + Fax).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        listUserList = JsonConvert.DeserializeObject<List<Registration>>(response.Content.ReadAsStringAsync().Result);
                        Status = true;
                    }
                    else
                        Status = false;
                    return Json(Status, JsonRequestBehavior.AllowGet);
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

        #region "GetUserType"

        private List<UserType> GetUserType()
        {
            List<UserType> userTypeList = new List<UserType>();
            HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync("http://localhost:53937/api/Register/GetLogin?UserName=admin&Password=admin").Result;
            if (response.IsSuccessStatusCode)
            {
                userTypeList = JsonConvert.DeserializeObject<List<UserType>>(response.Content.ReadAsStringAsync().Result);
            }
            return userTypeList;
        }

        #endregion

        #region SearchEmail

        public ActionResult SearchEmail(string SearchText, string std, int UserType)
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
                    var response = httpClient.GetAsync("Register/searchEmail?SID=" + SchoolID + "&SearchText=" + SearchText+ "&std="+ std + "&UserType="+ UserType).Result;
                    if (response.IsSuccessStatusCode)
                        listUserList = JsonConvert.DeserializeObject<List<Registration>>(response.Content.ReadAsStringAsync().Result);
                    return Json(listUserList, JsonRequestBehavior.AllowGet);
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


        public JsonResult AccountDetails()
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
                    return Json(listUserList, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    string error = ex.ToString().Trim();
                    return Json("false", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }

        }

    }

}
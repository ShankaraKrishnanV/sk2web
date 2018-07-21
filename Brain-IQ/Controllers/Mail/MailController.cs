using Brain_IQ.Models.Email;
using Brain_IQ.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Brain_IQ.Controllers.Mail
{
    public class MailController : Controller
    {

        public AppKey appKey = new AppKey();

        #region "Mail Page"

        // GET: Mail
        public ActionResult SendMail(string Id)
        {
            this.Session["OpenMail"] = Id;
            return View();
        }

        #endregion

        #region "View Invitations Page"

        public ActionResult Invitations()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["ExamID"])))
            {
                int SchoolID = Convert.ToInt32(Session["ExamID"]);
                int UType = Convert.ToInt32(Session["UserType"]);
                try
                {
                    List<EmailModels> listQuestions = new List<EmailModels>();
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = httpClient.GetAsync("emailnotifications/GetInvitations?SID=" + SchoolID + "&UType=" + UType).Result;
                    if (response.IsSuccessStatusCode)
                        listQuestions = JsonConvert.DeserializeObject<List<EmailModels>>(response.Content.ReadAsStringAsync().Result);
                    ViewBag.listStatus = listQuestions;
                    return View();
                }
                catch (Exception ex)
                {
                    string error = ex.ToString().Trim();
                    return View();
                }
            }
            else
            {
                return RedirectToAction("logins", "logins");
            }
        }

        #endregion

        #region "Go to Save Invitations Page"

        public ActionResult PostInvitations()
        {
            return View();
        }

        #endregion

        #region "Save Invitations"

        public ActionResult SaveInvitations(string ITitle, string IImage, string Description, string Privacy)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["ExamID"])))
            {
                int SchoolID = Convert.ToInt32(Session["ExamID"]);
                int UID = Convert.ToInt32(Session["UserId"]);
                try
                {
                    List<EmailModels> listEmailList = new List<EmailModels>();
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                    httpClient.DefaultRequestHeaders.Accept.Add(
                       new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = httpClient.GetAsync("emailnotifications/insertInvitation?SID=" + SchoolID + "&UID=" + UID + "&ITitle=" + ITitle + "&IImage=" + IImage + "&Description=" + Description + "&Privacy=" + Convert.ToInt32(Privacy)).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.ToString().Trim();
                    return Json(false, JsonRequestBehavior.AllowGet);
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
using Brain_IQ.Models;
using Brain_IQ.Models.Question;
using Brain_IQ.Settings;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Brain_IQ.Controllers.Question
{
    public class FileUploadController : Controller
    {

        public AppKey appKey = new AppKey();

        /// <summary>
        /// Upload questions Image
        /// </summary>
        /// <param name="files"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string UploadQuestionImage(IEnumerable files, QuestionModels model)
        {
            string Hidden = model.QuestionHidden;
            string _PassObject = UploadImage(files, model, Hidden);
            return _PassObject;
        }

        /// <summary>
        /// Upload option A Image
        /// </summary>
        /// <param name="files"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string UploadOptionAImage(IEnumerable files, QuestionModels model)
        {
            string Hidden = model.OptionAHidden;
            string _PassObject = UploadImage(files, model, Hidden);
            return _PassObject;
        }

        /// <summary>
        /// Upload option B Image
        /// </summary>
        /// <param name="files"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string UploadOptionBImage(IEnumerable files, QuestionModels model)
        {
            string Hidden = model.OptionBHidden;
            string _PassObject = UploadImage(files, model, Hidden);
            return _PassObject;
        }

        /// <summary>
        /// Upload option C Image
        /// </summary>
        /// <param name="files"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string UploadOptionCImage(IEnumerable files, QuestionModels model)
        {
            string Hidden = model.OptionCHidden;
            string _PassObject = UploadImage(files, model, Hidden);
            return _PassObject;
        }

        /// <summary>
        /// Upload option D Image
        /// </summary>
        /// <param name="files"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string UploadOptionDImage(IEnumerable files, QuestionModels model)
        {
            string Hidden = model.OptionDHidden;
            string _PassObject = UploadImage(files, model, Hidden);
            return _PassObject;
        }

        /// <summary>
        /// Upload status
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string UploadStatusImage(IEnumerable files)
        {
            QuestionModels model = new QuestionModels();
            string Hidden = "Status";
            string _PassObject = UploadImage(files, model, Hidden);
            return _PassObject;
        }

        /// <summary>
        /// Upload status
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string ProfileUpload(IEnumerable files)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(files)))
            {
                QuestionModels model = new QuestionModels();
                string Hidden = "profile";
                string _PassObject = UploadImage(files, model, Hidden);
                return _PassObject;
            }
            else
            {
                return "empty";
            }
        }

        public string UploadImage(IEnumerable files, QuestionModels model, string Hidden)
        {
            string path = string.Empty;
            string pic = string.Empty;
            string ext = string.Empty;
            string dataIs = string.Empty;
            int SID = Convert.ToInt32(Session["ExamID"]);
            int UID = Convert.ToInt32(Session["UserId"]);
            int UserType = Convert.ToInt32(Session["UserType"]);
            string _Date = DateTime.Now.Millisecond.ToString().Trim();

            try
            {
                foreach (HttpPostedFileBase file in files)
                {

                    if (Hidden.Equals("Status"))
                    {
                        // Verify that the user selected a file
                        if (file != null && file.ContentLength > 0)
                        {

                            pic = Path.GetFileName(file.FileName);
                            ext = Path.GetExtension(file.FileName);
                            path = Path.Combine(Server.MapPath("/images/Status/"), pic + ext);

                            // file is uploaded
                            file.SaveAs(path);
                        }
                        dataIs = Hidden + "|" + "/images/Status/" + pic + ext;
                    }
                    else if (Hidden.Equals("profile"))
                    {
                        // Verify that the user selected a file
                        if (file != null && file.ContentLength > 0)
                        {

                            pic = Path.GetFileName(SID + "_" + UID);
                            ext = Path.GetExtension(file.FileName);
                            path = Path.Combine(Server.MapPath("/images/UserProfile/"), pic + ext);

                            if (System.IO.File.Exists(path))
                            {
                                System.IO.File.Delete(path);
                            }
                            // file is uploaded
                            file.SaveAs(path);
                            string Location = ConfigurationManager.AppSettings["Application_URL"].ToString().Trim() + "/images/UserProfile/" + pic + ext;
                            try
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(Session["ExamID"])))
                                {
                                    int SchoolID = Convert.ToInt32(Session["ExamID"]);
                                    try
                                    {
                                        List<Registration> listEmailList = new List<Registration>();
                                        HttpClient httpClient = new HttpClient();
                                        httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                                        httpClient.DefaultRequestHeaders.Accept.Add(
                                           new MediaTypeWithQualityHeaderValue("application/json"));
                                        var response = httpClient.GetAsync("Register/UpdateProfilePicture?SID=" + SID + "&UID=" + UID + "&UserType=" + UserType + "&Location=" + Location).Result;
                                        if (response.IsSuccessStatusCode)
                                            dataIs = Hidden + "|" + "/images/UserProfile/" + pic + ext;
                                        else
                                            dataIs = "false";

                                    }
                                    catch (Exception ex)
                                    {
                                        string error = ex.ToString().Trim();
                                        dataIs = "false";
                                    }
                                }
                                else
                                {
                                    dataIs = "false";
                                }
                            }
                            catch (Exception ex)
                            {
                                dataIs = ex.ToString().Trim();
                            }

                        }
                        dataIs = Hidden + "|" + "/images/UserProfile/" + pic + ext;
                    }
                    else
                    {
                        // Verify that the user selected a file
                        if (file != null && file.ContentLength > 0)
                        {
                            string StandardId = Session["StandardId"].ToString().Trim();
                            string SubjectId = Session["SubjectId"].ToString().Trim();
                            string ChapterId = Session["ChapterId"].ToString().Trim();
                            pic = Path.GetFileName(StandardId + "_" + SubjectId + "_" + ChapterId + "_" + Hidden + "_" + _Date);
                            ext = Path.GetExtension(file.FileName);
                            path = Path.Combine(Server.MapPath("/images/profile/"), pic + ext);

                            // file is uploaded
                            file.SaveAs(path);
                        }
                        dataIs = Hidden + "|" + "/images/profile/" + pic + ext;
                    }
                }

                return dataIs;
            }
            catch (Exception ex)
            {
                return ex.ToString().Trim();
            }
        }

        /// <summary>
        /// get questions 
        /// </summary>
        /// <returns></returns>
        public ActionResult Update(int Result)
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            int ChapterId = Convert.ToInt32(Session["ChapterId"]);

            List<QuestionModels> questionList = new List<QuestionModels>();
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(appKey.GetapiURL());
            httpClient.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            var response = httpClient.GetAsync("Questions/UploadImage?UserId=" + Convert.ToInt32(UserId) + "&ChapterId=" + ChapterId + "&Marks=" + Result + "").Result;
            if (response.IsSuccessStatusCode)
                return Json(response.IsSuccessStatusCode, JsonRequestBehavior.AllowGet);

            else
                return RedirectToAction("logins", "logins");
        }
    }

}
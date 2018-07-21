using Brain_IQ.Settings;
using BrainIQ.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Brain_IQ.Controllers
{
    public class LoginController : Controller
    {

        public AppKey appKey = new AppKey();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        public ActionResult GetLogin(Home home)
        {
            List<Home> listHome = new List<Home>();
            if (ModelState.IsValid)
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(appKey.GetapiURL());
                httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.GetAsync("Register/GetLogin?UserName=" + home.UserName + "&Password=" + home.Password).Result;
                if (response.IsSuccessStatusCode)
                    listHome = JsonConvert.DeserializeObject<List<Home>>(response.Content.ReadAsStringAsync().Result);
                foreach (var item in listHome)
                {
                    this.Session["UserId"] = item.UID;
                    this.Session["UserType"] = item.UserType;
                    this.Session["LoginUserName"] = item.UserName;
                    this.Session["Password"] = item.Password;
                    this.Session["ExamID"] = item.SchoolID;
                    this.Session["StandardID"] = item.StandardId;
                    if (!string.IsNullOrEmpty(Convert.ToString(item.Location)))
                        TempData["ProfilePicture"] = item.Location;
                    else
                        TempData["ProfilePicture"] = string.Empty;

                    if (!string.IsNullOrEmpty(Convert.ToString(item.EmailId)))
                        this.Session["LoginEmailID"] = item.EmailId;
                    else
                        this.Session["LoginEmailID"] = string.Empty;

                    this.Session["Session_StandardID"] = item.StandardId;
                }

                if (listHome.Count > 0)
                {
                    
                    if (Convert.ToInt32(Session["UserType"]) == 1)
                    {
                        //1 --> Admin
                        return RedirectToAction("dashboard", "staff");
                    } 
                    else if (Convert.ToInt32(Session["UserType"]) == 2)
                    {
                        // 2 --> Staff
                        return RedirectToAction("dashboard", "staff");
                    } 
                    else if (Convert.ToInt32(Session["UserType"]) == 3)
                    {
                        // 3 --> Paid User
                        return RedirectToAction("dashboard", "student");
                    }
                    else
                    {
                        // 4 --> Free User
                        return RedirectToAction("dashboard", "student");
                    }
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
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

namespace Brain_IQ.Controllers
{
    public class LoginsController : Controller
    {

        public AppKey appKey = new AppKey();

        // GET: Logins
        public ActionResult Logins()
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
                var response = httpClient.GetAsync("Register/GetLogin?UserName=" + home.UserName + "&Password=" + home.Password + "").Result;
                if (response.IsSuccessStatusCode)
                    listHome = JsonConvert.DeserializeObject<List<Home>>(response.Content.ReadAsStringAsync().Result);
                foreach (var item in listHome)
                {
                    this.Session["UserId"] = item.UID;
                }

                if (listHome.Count > 0)
                {
                    //1 --> Admin
                    if (Convert.ToInt32(Session["UserType"]) == 1)
                    {
                        return RedirectToAction("staff", "dashboard");
                    } // 2 --> Staff
                    else if (Convert.ToInt32(Session["UserType"]) == 2)
                    {
                        return RedirectToAction("staff", "dashboard");
                    } // 3 --> Student
                    else
                    {
                        return RedirectToAction("student", "dashboard");
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
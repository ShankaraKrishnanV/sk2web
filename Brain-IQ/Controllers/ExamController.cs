using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Brain_IQ.Controllers
{
    public class examController : Controller
    {
        // GET: Exam
        public ActionResult available()
        {
            return View();
        }
    }
}
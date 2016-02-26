using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELearning.Controllers
{
    public class MainPageController : Controller
    {
        // GET: MainPage
        [HttpGet]
        public ActionResult Main()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Note()
        {
            return Redirect("mailto:marcinszydlak93@wp.pl");
        }
        [HttpGet]
        public ActionResult About()
        {
            return View();
        }
    }
}
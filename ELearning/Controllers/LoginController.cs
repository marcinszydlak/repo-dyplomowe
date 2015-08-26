using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataServices;
using DataServices.ViewModels;

namespace ELearning.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        [HttpGet]
        public ActionResult Index()
     {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string Login, string Password)
        {
            UserServices service = new UserServices();
            UserLoginModel model = service.GetLoggedUser(Login, Password);
            if (model != null)
            {
                return RedirectToAction("Look", model);
            }
            else
            {
                ViewBag.Message = "Nie istnieje użytkownik o podanych danych logowania, spróbuj ponownie";
                return View();
            }
        }
        public ActionResult Look(UserLoginModel usm)
        {
            return View(usm);
        }

    }
}

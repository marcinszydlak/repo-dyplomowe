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

        #region Logowanie
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
                Session["zalogowany"] = model;
                LoggedUserModel user = new LoggedUserModel();
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Login = model.Login;
                HttpCookie cookie = new HttpCookie("LoggedUser");
                cookie.Values.Add("Name",model.Name);
                cookie.Values.Add("Surname",model.Surname);
                cookie.Values.Add("Login",model.Login);
                cookie.Expires = DateTime.Now.AddDays(1.0);
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index","Student", user);
            }
            else
            {
                ViewBag.Message = "Nie istnieje użytkownik o podanych danych logowania, spróbuj ponownie";
                return View();
            }
            
        }
        #endregion

        #region Wylogowanie
        public ActionResult Logout(UserLoginModel usm)
        {
            usm = null;
            Session.Remove("zalogowany");
            HttpCookie cookie = Response.Cookies.Get("LoggedUser");
            cookie.Expires = DateTime.Now.AddYears(-1);
            return RedirectToAction("Index");
        }
        #endregion

    }
}

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
            UserServices service = new UserServices();

            if (Request.Cookies.Get("LoggedUser") != null)
            {

                UserLoginModel model = service.GetLoggedUser(Request.Cookies.Get("LoggedUser").Values.Get("Login"));
                if(Session["zalogowany"]!= null)
                    if (service.ExistsStudentInDatabase(Request.Cookies.Get("LoggedUser").Values.Get("Login"), Request.Cookies.Get("LoggedUser").Values.Get("Name"), Request.Cookies.Get("LoggedUser").Values.Get("Surname")))
                    {
                        return RedirectToAction("Index", "Student", model);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Teacher", model);
                    }
            }
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

                if (service.ExistsStudentInDatabase(user.Login, user.Name, user.Surname))
                {
                    cookie.Values.Add("Role", "S");
                    cookie.Expires = DateTime.Now.AddDays(1.0);
                    Response.Cookies.Add(cookie);
                    return RedirectToAction("Index", "Student", user);
                }
                else
                {
                    cookie.Values.Add("Role", "T");
                    cookie.Expires = DateTime.Now.AddDays(1.0);
                    Response.Cookies.Add(cookie);
                    return RedirectToAction("Index", "Teacher", user);
                }
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

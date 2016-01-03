using DataServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataServices;

namespace ELearning.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student

        [HttpGet]
        public ActionResult Index(UserLoginModel model)
        {

            LoggedUserModel user = new LoggedUserModel();
            UserServices services = new UserServices();
            if(Session["zalogowany"] != null && Request.Cookies["LoggedUser"] !=null && model.Login == null && model.Password == null)
            {
                user.Login = Request.Cookies["LoggedUser"].Values.Get("Login");
                user.Name = Request.Cookies["LoggedUser"].Values.Get("Name");
                user.Surname = Request.Cookies["LoggedUser"].Values.Get("Surname");
            }
            else
            {
                user.Login = model.Login;
                user.Name = model.Name;
                user.Surname = model.Surname;
            }
            return View(user);
        }
        [HttpGet]
        public ActionResult EditStudent()
        {
            HttpCookie loggedStudent = Request.Cookies["LoggedUser"];
            StudentModel student = new StudentModel();
            UserServices services = new UserServices();

            
            student = services.GetStudent(loggedStudent.Values.Get("Name"),loggedStudent.Values.Get("Surname"),loggedStudent.Values.Get("Login"));
            student.Oddziały = services.GetClasses();
            return View(student);
        }
        [HttpPost]
        public ActionResult EditStudent(StudentModel model)
        {
            HttpCookie loggedStudent = Request.Cookies["LoggedUser"];
            UserServices services = new UserServices();
            StudentModel student = services.GetStudent(loggedStudent.Values.Get("Name"), loggedStudent.Values.Get("Surname"), loggedStudent.Values.Get("Login"));
            services.ChangePassword(student.Login, student.Name, student.Surname, model.Password);

            UserLoginModel logged = new UserLoginModel();
            logged.Login = model.Login;
            logged.Name = model.Name;
            logged.Surname = model.Surname;
            return RedirectToAction("Index",logged);
        }

    }
}
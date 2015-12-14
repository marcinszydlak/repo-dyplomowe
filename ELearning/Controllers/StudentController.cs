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
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Login = model.Login;
            
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
            UserServices services = new UserServices();
            services.UpdateStudent(model);
            UserLoginModel logged = new UserLoginModel();
            logged.Login = model.Login;
            logged.Name = model.Name;
            logged.Surname = model.Surname;
            return RedirectToAction("Index",logged);
        }

    }
}
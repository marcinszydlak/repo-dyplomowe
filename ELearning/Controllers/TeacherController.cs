using DataServices;
using DataServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ELearning.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        [HttpGet]
        public ActionResult Index(UserLoginModel model)
        {
            LoggedUserModel user = new LoggedUserModel();
            UserServices services = new UserServices();
            if (Session["zalogowany"] != null && Request.Cookies["LoggedUser"] != null && model.Login == null && model.Password == null)
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
        public ActionResult EditTeacher()
        {
            HttpCookie loggedTeacher = Request.Cookies["LoggedUser"];
            TeacherModel teacher = new TeacherModel();
            UserServices services = new UserServices();


            teacher = services.GetTeacher(loggedTeacher.Values.Get("Login"));
            return View(teacher);
        }
        [HttpPost]
        public ActionResult EditTeacher(TeacherModel model)
        {
            HttpCookie loggedTeacher = Request.Cookies["LoggedUser"];
            UserServices services = new UserServices();
            TeacherModel teacher = services.GetTeacher(loggedTeacher.Values.Get("Login"));
            services.ChangePassword(teacher.Login, teacher.Imię, teacher.Nazwisko, model.Hasło);

            UserLoginModel logged = new UserLoginModel();
            logged.Login = model.Login;
            logged.Name = model.Imię;
            logged.Surname = model.Nazwisko;
            return RedirectToAction("Index", logged);
        }
        [HttpGet]
        public ActionResult CreateCourse()
        {
            CourseModel model = new CourseModel();
            CourseServices ServiceForCourses = new CourseServices();
            UserServices ServicesForUsers = new UserServices();
            model.Subjects = ServiceForCourses.GetSubjects();
            model.Classes = ServicesForUsers.GetClasses();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateCourse(CourseModel newCourse)
        {
            CourseServices CourseServ = new CourseServices();
            UserServices UserServ = new UserServices();
            HttpCookie loggedTeacher = Request.Cookies["LoggedUser"];
            TeacherModel teacher = UserServ.GetTeacher(loggedTeacher.Values.Get("Login"));
            newCourse.TeacherId = teacher.IdNauczyciela;
            CourseServ.NewCourse(newCourse);
            return RedirectToAction("Index", "Teacher");
        }
        [HttpGet]
        public ActionResult ManageCourses()
        {
            List<CourseModel> courses = new List<CourseModel>();
            CourseServices CourseServ = new CourseServices();
            UserServices UserServ = new UserServices();
            HttpCookie loggedTeacher = Request.Cookies["LoggedUser"];
            TeacherModel teacher = UserServ.GetTeacher(loggedTeacher.Values.Get("Login"));
            courses = CourseServ.GetCourses(teacher.IdNauczyciela);
            return View(courses);
        }


    }
}
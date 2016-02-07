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

            
            student = services.GetStudent(loggedStudent.Values.Get("Login"));
            student.Oddziały = services.GetClasses();
            return View(student);
        }
        [HttpPost]
        public ActionResult EditStudent(StudentModel model)
        {
            HttpCookie loggedStudent = Request.Cookies["LoggedUser"];
            UserServices services = new UserServices();
            StudentModel student = services.GetStudent(loggedStudent.Values.Get("Login"));
            services.ChangePassword(student.Login, student.Name, student.Surname, model.Password);

            UserLoginModel logged = new UserLoginModel();
            logged.Login = model.Login;
            logged.Name = model.Name;
            logged.Surname = model.Surname;
            return RedirectToAction("Index",logged);
        }
        [HttpGet]
        public ActionResult ManageCourses()
        {
            CourseServices cs = new CourseServices();
            UserServices us = new UserServices();
            List<CourseModel> courses = new List<CourseModel>();
            HttpCookie loggedStudent = Request.Cookies["LoggedUser"];
            StudentModel student = us.GetStudent(loggedStudent.Values.Get("Login"));
            courses = cs.GetCourses().Where(x => x.ClassId == student.IdKlasy).ToList();
            return View(courses);
        }
        [HttpGet]
        public ActionResult TaskList(int CourseId)
        {
            CourseServices cs = new CourseServices();
            List<TaskModel> tasks = cs.GetTasks(CourseId);
            return View(tasks);
        }
        [HttpGet]
        public ActionResult InsertSolution(int TaskId)
        {
            HttpCookie cookie = new HttpCookie("Solution");
            cookie.Values.Add("TaskId", TaskId.ToString());
            cookie.Expires = DateTime.Now.AddMinutes(2);
            Response.Cookies.Add(cookie);
            return View();
        }
        [HttpPost]
        public ActionResult InsertSolution(SolutionModel model,HttpPostedFileBase upload)
        {
            CourseServices cs = new CourseServices();
            UserServices us = new UserServices();
            HttpCookie loggedStudent = Request.Cookies["LoggedUser"];
            model.Student = us.GetStudent(loggedStudent.Values.Get("Login"));
            model.StudentId = model.Student.IdUcznia;
            model.FileName = upload.FileName;
            using (var reader = new System.IO.BinaryReader(upload.InputStream))
            {
                model.Solution = reader.ReadBytes(upload.ContentLength);
            }
            cs.NewSolution(model);

            return RedirectToAction("ManageCourses","Student");
        }
    }
}